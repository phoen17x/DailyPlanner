using AutoMapper;
using DailyPlanner.Common.Exceptions;
using DailyPlanner.Common.Validator;
using DailyPlanner.Context;
using DailyPlanner.Context.Entities;
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
    private readonly IModelValidator<AddTodoTaskModel> addTodoTaskModelValidator;
    private readonly IModelValidator<UpdateTodoTaskModel> updateTodoTaskModelValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="TodoTaskService"/> class.
    /// </summary>
    /// <param name="contextFactory">Factory that provides access to the <see cref="DailyPlannerContext"/>.</param>
    /// <param name="mapper">Object mapper that maps entities to models and vice versa.</param>
    /// <param name="addTodoTaskModelValidator">Validator for <see cref="AddTodoTaskModel"/>.</param>
    public TodoTaskService(
        IDbContextFactory<DailyPlannerContext> contextFactory,
        IMapper mapper,
        IModelValidator<AddTodoTaskModel> addTodoTaskModelValidator,
        IModelValidator<UpdateTodoTaskModel> updateTodoTaskModelValidator)
    {
        this.contextFactory = contextFactory;
        this.mapper = mapper;
        this.addTodoTaskModelValidator = addTodoTaskModelValidator;
        this.updateTodoTaskModelValidator = updateTodoTaskModelValidator;
    }

    public async Task<IEnumerable<TodoTaskModel>> GetTodoTasks(int notebookId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();

        var todoTasks = context.TodoTasks.Where(task => task.NotebookId == notebookId);

        return (await todoTasks.ToListAsync()).Select(mapper.Map<TodoTaskModel>);
    }

    public async Task<TodoTaskModel> AddTodoTask(AddTodoTaskModel model)
    {
        addTodoTaskModelValidator.Check(model);

        await using var context = await contextFactory.CreateDbContextAsync();

        var todoTask = mapper.Map<TodoTask>(model);
        await context.TodoTasks.AddAsync(todoTask);
        await context.SaveChangesAsync();

        return mapper.Map<TodoTaskModel>(todoTask);
    }

    public async Task UpdateTodoTask(int todoTaskId, UpdateTodoTaskModel model)
    {
        updateTodoTaskModelValidator.Check(model);

        await using var context = await contextFactory.CreateDbContextAsync();

        var todoTask = await context.TodoTasks.FirstOrDefaultAsync(todoTask => todoTask.Id.Equals(todoTaskId));
        ProcessException.ThrowIfNull(todoTask, $"The notebook with id {todoTaskId} was not found");

        todoTask = mapper.Map(model, todoTask);
        context.TodoTasks.Update(todoTask!);
        await context.SaveChangesAsync();
    }

    public async Task DeleteTodoTask(int todoTaskId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();

        var todoTask = await context.TodoTasks.FirstOrDefaultAsync(todoTask => todoTask.Id.Equals(todoTaskId));
        ProcessException.ThrowIfNull(todoTask, $"The notebook with id {todoTaskId} was not found");

        context.Remove(todoTask!);
        await context.SaveChangesAsync();
    }
}