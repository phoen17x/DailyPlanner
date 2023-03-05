using AutoMapper;
using DailyPlanner.Api.Controllers.TodoTasks.Models;
using DailyPlanner.Common.Responses;
using DailyPlanner.Services.TodoTasks;
using DailyPlanner.Services.TodoTasks.Models;
using Microsoft.AspNetCore.Mvc;

namespace DailyPlanner.Api.Controllers.TodoTasks;

/// <summary>
/// API endpoints for managing todotasks.
/// </summary>
[ProducesResponseType(typeof(ErrorResponse), 400)]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/todotasks")]
[ApiController]
[ApiVersion("1.0")]
public class TodoTaskController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly ITodoTaskService todoTaskService;

    /// <summary>
    /// Creates a new instance of the <see cref="TodoTaskController"/> class.
    /// </summary>
    /// <param name="mapper">The <see cref="IMapper"/> instance used for mapping.</param>
    /// <param name="todoTaskService">The <see cref="ITodoTaskService"/> instance used for task operations.</param>
    public TodoTaskController(IMapper mapper, ITodoTaskService todoTaskService)
    {
        this.mapper = mapper;
        this.todoTaskService = todoTaskService;
    }

    /// <summary>
    /// Gets a list of todotasks.
    /// </summary>
    /// <param name="notebookId">ID of the notebook to get todotasks from.</param>
    /// <returns>A collection of <see cref="TodoTaskResponse"/> objects.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TodoTaskResponse>), 200)]
    public async Task<IEnumerable<TodoTaskResponse>> GetTodoTasks([FromQuery] int notebookId)
    {
        var todoTasks = await todoTaskService.GetTodoTasks(notebookId);
        return mapper.Map<IEnumerable<TodoTaskResponse>>(todoTasks);
    }

    /// <summary>
    /// Adds a new todotask.
    /// </summary>
    /// <param name="request">The <see cref="AddTodoTaskRequest"/> object containing the todotask details.</param>
    /// <returns>A <see cref="TodoTaskResponse"/> object.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(TodoTaskResponse), 200)]
    public async Task<TodoTaskResponse> AddTodoTask([FromBody] AddTodoTaskRequest request)
    {
        var model = mapper.Map<AddTodoTaskModel>(request);
        var todoTask = await todoTaskService.AddTodoTask(model);
        return mapper.Map<TodoTaskResponse>(todoTask);
    }

    /// <summary>
    /// Updates an existing todotask by id.
    /// </summary>
    /// <param name="todoTaskId">The id of the todotask to update.</param>
    /// <param name="request">The <see cref="UpdateTodoTaskRequest"/> object containing the updated todotask details.</param>
    /// <returns>An <see cref="IActionResult"/> indicating success or failure.</returns>
    [HttpPut("{todoTaskId}")]
    [ProducesResponseType(typeof(IActionResult), 200)]
    public async Task<IActionResult> UpdateTodoTask(
        [FromRoute] int todoTaskId,
        [FromBody] UpdateTodoTaskRequest request)
    {
        var model = mapper.Map<UpdateTodoTaskModel>(request);
        await todoTaskService.UpdateTodoTask(todoTaskId, model);
        return Ok();
    }

    /// <summary>
    /// Deletes todotask by id.
    /// </summary>
    /// <param name="todoTaskId">The id of the todotask to delete.</param>
    /// <returns>An <see cref="IActionResult"/> indicating success or failure.</returns>
    [HttpDelete("{todoTaskId}")]
    [ProducesResponseType(typeof(IActionResult), 200)]
    public async Task<IActionResult> DeleteNotebook([FromRoute] int todoTaskId)
    {
        await todoTaskService.DeleteTodoTask(todoTaskId);
        return Ok();
    }
}