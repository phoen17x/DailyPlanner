using AutoMapper;
using DailyPlanner.Common.Exceptions;
using DailyPlanner.Common.Validator;
using DailyPlanner.Context;
using DailyPlanner.Context.Entities;
using DailyPlanner.Context.Entities.User;
using DailyPlanner.Services.Cache;
using DailyPlanner.Services.TodoTasks.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyPlanner.Services.TodoTasks;

/// <summary>
/// Service that provides CRUD operations for <see cref="TodoTask"/> entities
/// </summary>
public class TodoTaskService : ITodoTaskService
{
    private readonly IDbContextFactory<DailyPlannerContext> contextFactory;
    private readonly IMapper mapper;
    private readonly ICacheService cacheService;
    private readonly IModelValidator<AddTodoTaskModel> addTodoTaskModelValidator;
    private readonly IModelValidator<UpdateTodoTaskModel> updateTodoTaskModelValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="TodoTaskService"/> class.
    /// </summary>
    /// <param name="contextFactory">Factory that provides access to the <see cref="DailyPlannerContext"/>.</param>
    /// <param name="mapper">Object mapper that maps entities to models and vice versa.</param>
    /// <param name="cacheService">Service to cache data.</param>
    /// <param name="addTodoTaskModelValidator">Validator for <see cref="AddTodoTaskModel"/>.</param>
    /// <param name="updateTodoTaskModelValidator">Validator for <see cref="UpdateTodoTaskModel"/>.</param>
    public TodoTaskService(
        IDbContextFactory<DailyPlannerContext> contextFactory,
        IMapper mapper,
        ICacheService cacheService,
        IModelValidator<AddTodoTaskModel> addTodoTaskModelValidator,
        IModelValidator<UpdateTodoTaskModel> updateTodoTaskModelValidator)
    {
        this.contextFactory = contextFactory;
        this.mapper = mapper;
        this.cacheService = cacheService;
        this.addTodoTaskModelValidator = addTodoTaskModelValidator;
        this.updateTodoTaskModelValidator = updateTodoTaskModelValidator;
    }

    public async Task<IEnumerable<TodoTaskModel>> GetTodoTasks(Guid userId, int notebookId)
    {
        var cacheKey = $"dailyplanner:todotasks-{notebookId}-{userId}";
        var cachedData = await cacheService.Get<IEnumerable<TodoTaskModel>>(cacheKey);
        if (cachedData is not null) return cachedData;

        await using var context = await contextFactory.CreateDbContextAsync();
        var todoTasks = context.TodoTasks
            .Where(task => task.UserId == userId && task.NotebookId == notebookId);

        var data = (await todoTasks.ToListAsync()).Select(mapper.Map<TodoTaskModel>);
        await cacheService.Put(cacheKey, data);

        return data;
    }

    public async Task<TodoTaskModel> GetTodoTask(Guid userId, int todoTaskId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();

        var todoTask = await context.TodoTasks
            .Where(task => task.UserId == userId)
            .FirstOrDefaultAsync(task => task.Id.Equals(todoTaskId));

        return mapper.Map<TodoTaskModel>(todoTask);
    }

    public async Task<TodoTaskModel> AddTodoTask(AddTodoTaskModel model)
    {
        addTodoTaskModelValidator.Check(model);

        await using var context = await contextFactory.CreateDbContextAsync();

        var todoTask = mapper.Map<TodoTask>(model);
        await context.TodoTasks.AddAsync(todoTask);
        await context.SaveChangesAsync();

        var cacheKey = $"dailyplanner:todotasks-{model.NotebookId}-{model.UserId}";
        await cacheService.Delete(cacheKey);

        return mapper.Map<TodoTaskModel>(todoTask);
    }

    public async Task UpdateTodoTask(int todoTaskId, UpdateTodoTaskModel model)
    {
        updateTodoTaskModelValidator.Check(model);

        await using var context = await contextFactory.CreateDbContextAsync();
        var todoTask = await context.TodoTasks
            .Where(task => task.UserId == model.UserId)
            .FirstOrDefaultAsync(task => task.Id.Equals(todoTaskId));
        ProcessException.ThrowIfNull(todoTask, $"The task with id {todoTaskId} was not found.");

        todoTask = mapper.Map(model, todoTask);
        context.TodoTasks.Update(todoTask!);

        var cacheKey = $"dailyplanner:todotasks-{model.NotebookId}-{model.UserId}";
        await cacheService.Delete(cacheKey);

        await context.SaveChangesAsync();
    }

    public async Task DeleteTodoTask(Guid userId, int todoTaskId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();

        var todoTask = await context.TodoTasks
            .Where(task => task.UserId == userId)
            .FirstOrDefaultAsync(task => task.Id.Equals(todoTaskId));
        ProcessException.ThrowIfNull(todoTask, $"The task with id {todoTaskId} was not found.");

        context.Remove(todoTask!);

        var cacheKey = $"dailyplanner:todotasks-{todoTask?.NotebookId}-{userId}";
        await cacheService.Delete(cacheKey);

        await context.SaveChangesAsync();
    }
}