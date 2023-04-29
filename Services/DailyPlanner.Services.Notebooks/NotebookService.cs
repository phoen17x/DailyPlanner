using AutoMapper;
using DailyPlanner.Common.Exceptions;
using DailyPlanner.Common.Validator;
using DailyPlanner.Context;
using DailyPlanner.Context.Entities;
using DailyPlanner.Services.Cache;
using DailyPlanner.Services.Notebooks.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyPlanner.Services.Notebooks;

/// <summary>
/// Service that provides CRUD operations for <see cref="Notebook"/> entities
/// </summary>
public class NotebookService : INotebookService
{
    private readonly IDbContextFactory<DailyPlannerContext> contextFactory;
    private readonly IMapper mapper;
    private readonly ICacheService cacheService;
    private readonly IModelValidator<AddNotebookModel> addNotebookModelValidator;
    private readonly IModelValidator<UpdateNotebookModel> updateNotebookModelValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="NotebookService"/> class.
    /// </summary>
    /// <param name="contextFactory">Factory that provides access to the <see cref="DailyPlannerContext"/>.</param>
    /// <param name="mapper">Object mapper that maps entities to models and vice versa.</param>
    /// <param name="cacheService">Service to cache data.</param>
    /// <param name="addNotebookModelValidator">Validator for <see cref="AddNotebookModel"/>.</param>
    /// <param name="updateNotebookModelValidator">Validator for <see cref="UpdateNotebookModel"/>.</param>
    public NotebookService(
        IDbContextFactory<DailyPlannerContext> contextFactory,
        IMapper mapper,
        ICacheService cacheService,
        IModelValidator<AddNotebookModel> addNotebookModelValidator,
        IModelValidator<UpdateNotebookModel> updateNotebookModelValidator)
    {
        this.contextFactory = contextFactory;
        this.mapper = mapper;
        this.cacheService = cacheService;
        this.addNotebookModelValidator = addNotebookModelValidator;
        this.updateNotebookModelValidator = updateNotebookModelValidator;
    }
    

    public async Task<IEnumerable<NotebookModel>> GetNotebooks(Guid userId)
    {
        var cacheKey = $"dailyplanner:notebooks-{userId}";
        var cachedData = await cacheService.Get<IEnumerable<NotebookModel>>(cacheKey);
        if (cachedData is not null) return cachedData;

        await using var context = await contextFactory.CreateDbContextAsync();
        var notebooks = context.Notebooks.Where(notebook => notebook.UserId == userId);

        var data = (await notebooks.ToListAsync()).Select(mapper.Map<NotebookModel>);
        await cacheService.Put(cacheKey, data);

        return data;
    }

    public async Task<NotebookModel> GetNotebook(Guid userId, int notebookId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();

        var notebook = await context.Notebooks
            .Where(notebook => notebook.UserId == userId)
            .FirstOrDefaultAsync(notebook => notebook.Id.Equals(notebookId));

        return mapper.Map<NotebookModel>(notebook);
    }

    public async Task<NotebookModel> AddNotebook(AddNotebookModel model)
    {
        addNotebookModelValidator.Check(model);

        await using var context = await contextFactory.CreateDbContextAsync();

        var notebook = mapper.Map<Notebook>(model);
        await context.Notebooks.AddAsync(notebook);
        await context.SaveChangesAsync();

        await cacheService.Delete($"dailyplanner:notebooks-{model.UserId}");

        return mapper.Map<NotebookModel>(notebook);
    }

    public async Task UpdateNotebook(int notebookId, UpdateNotebookModel model)
    {
        updateNotebookModelValidator.Check(model);

        await using var context = await contextFactory.CreateDbContextAsync();
        var notebook = await context.Notebooks
            .Where(notebook => notebook.UserId == model.UserId)
            .FirstOrDefaultAsync(notebook => notebook.Id.Equals(notebookId));
        ProcessException.ThrowIfNull(notebook, $"The notebook with id {notebookId} was not found.");

        notebook = mapper.Map(model, notebook);
        context.Notebooks.Update(notebook!);

        await cacheService.Delete($"dailyplanner:notebooks-{model.UserId}");

        await context.SaveChangesAsync();
    }

    public async Task DeleteNotebook(Guid userId, int notebookId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();

        var notebook = await context.Notebooks
            .Where(notebook => notebook.UserId == userId)
            .FirstOrDefaultAsync(notebook => notebook.Id.Equals(notebookId));
        ProcessException.ThrowIfNull(notebook, $"The notebook with id {notebookId} was not found.");

        context.Remove(notebook!);

        await cacheService.Delete($"dailyplanner:notebooks-{userId}");
        await cacheService.Delete($"dailyplanner:todotasks-{userId}");

        await context.SaveChangesAsync();
    }
}