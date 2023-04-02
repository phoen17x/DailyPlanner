using AutoMapper;
using DailyPlanner.Common.Exceptions;
using DailyPlanner.Common.Validator;
using DailyPlanner.Context;
using DailyPlanner.Context.Entities;
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
    private readonly IModelValidator<AddNotebookModel> addNotebookModelValidator;
    private readonly IModelValidator<UpdateNotebookModel> updateNotebookModelValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="NotebookService"/> class.
    /// </summary>
    /// <param name="contextFactory">Factory that provides access to the <see cref="DailyPlannerContext"/>.</param>
    /// <param name="mapper">Object mapper that maps entities to models and vice versa.</param>
    /// <param name="addNotebookModelValidator">Validator for <see cref="AddNotebookModel"/>.</param>
    /// <param name="updateNotebookModelValidator">Validator for <see cref="UpdateNotebookModel"/>.</param>
    public NotebookService(
        IDbContextFactory<DailyPlannerContext> contextFactory,
        IMapper mapper,
        IModelValidator<AddNotebookModel> addNotebookModelValidator,
        IModelValidator<UpdateNotebookModel> updateNotebookModelValidator)
    {
        this.contextFactory = contextFactory;
        this.mapper = mapper;
        this.addNotebookModelValidator = addNotebookModelValidator;
        this.updateNotebookModelValidator = updateNotebookModelValidator;
    }
    

    public async Task<IEnumerable<NotebookModel>> GetNotebooks(Guid userId, int offset = 0, int limit = 10)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var notebooks = context.Notebooks.AsQueryable();
        notebooks = notebooks
            .Where(notebook => notebook.UserId == userId)
            .Skip(Math.Max(offset, 0))
            .Take(Math.Max(0, Math.Min(limit, 1000)));

        var data = (await notebooks.ToListAsync()).Select(mapper.Map<NotebookModel>);

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
        await context.SaveChangesAsync();
    }
}