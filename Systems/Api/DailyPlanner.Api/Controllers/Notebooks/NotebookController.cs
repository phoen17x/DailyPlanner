using AutoMapper;
using DailyPlanner.Api.Controllers.Notebooks.Models;
using DailyPlanner.Common.Responses;
using DailyPlanner.Services.Notebooks;
using DailyPlanner.Services.Notebooks.Models;
using Microsoft.AspNetCore.Mvc;

namespace DailyPlanner.Api.Controllers.Notebooks;

/// <summary>
/// API endpoints for managing notebooks.
/// </summary>
[ProducesResponseType(typeof(ErrorResponse), 400)]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/notebooks")]
[ApiController]
[ApiVersion("1.0")]
public class NotebookController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly INotebookService notebookService;

    /// <summary>
    /// Creates a new instance of the <see cref="NotebookController"/> class.
    /// </summary>
    /// <param name="mapper">The <see cref="IMapper"/> instance used for mapping.</param>
    /// <param name="notebookService">The <see cref="INotebookService"/> instance used for notebook operations.</param>
    public NotebookController(IMapper mapper, INotebookService notebookService)
    {
        this.mapper = mapper;
        this.notebookService = notebookService;
    }

    /// <summary>
    /// Gets a list of notebooks.
    /// </summary>
    /// <param name="offset">The offset to start from.</param>
    /// <param name="limit">The maximum number of notebooks to return.</param>
    /// <returns>A collection of <see cref="NotebookResponse"/> objects.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<NotebookResponse>), 200)]
    public async Task<IEnumerable<NotebookResponse>> GetNotebooks(
        [FromQuery] int offset = 0,
        [FromQuery] int limit = 10)
    {
        var notebooks = await notebookService.GetNotebooks(offset, limit);
        return mapper.Map<IEnumerable<NotebookResponse>>(notebooks);
    }

    /// <summary>
    /// Gets a single notebook by id.
    /// </summary>
    /// <param name="notebookId">The id of the notebook to get.</param>
    /// <returns>A <see cref="NotebookResponse"/> object.</returns>
    [HttpGet("{notebookId}")]
    [ProducesResponseType(typeof(NotebookResponse), 200)]
    public async Task<NotebookResponse> GetNotebook([FromRoute] int notebookId)
    {
        var notebook = await notebookService.GetNotebook(notebookId);
        return mapper.Map<NotebookResponse>(notebook);
    }

    /// <summary>
    /// Adds a new notebook.
    /// </summary>
    /// <param name="request">The <see cref="AddNotebookRequest"/> object containing the notebook details.</param>
    /// <returns>A <see cref="NotebookResponse"/> object.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(NotebookResponse), 200)]
    public async Task<NotebookResponse> AddNotebook([FromBody] AddNotebookRequest request)
    {
        var model = mapper.Map<AddNotebookModel>(request);
        var notebook = await notebookService.AddNotebook(model);
        return mapper.Map<NotebookResponse>(notebook);
    }

    /// <summary>
    /// Updates an existing notebook by id.
    /// </summary>
    /// <param name="notebookId">The id of the notebook to update.</param>
    /// <param name="request">The <see cref="UpdateNotebookRequest"/> object containing the updated notebook details.</param>
    /// <returns>An <see cref="IActionResult"/> indicating success or failure.</returns>
    [HttpPut("{notebookId}")]
    [ProducesResponseType(typeof(IActionResult), 200)]
    public async Task<IActionResult> UpdateNotebook(
        [FromRoute] int notebookId, 
        [FromBody] UpdateNotebookRequest request)
    {
        var model = mapper.Map<UpdateNotebookModel>(request);
        await notebookService.UpdateNotebook(notebookId, model);
        return Ok();
    }

    /// <summary>
    /// Deletes notebook by id.
    /// </summary>
    /// <param name="notebookId">The id of the notebook to delete.</param>
    /// <returns>An <see cref="IActionResult"/> indicating success or failure.</returns>
    [HttpDelete("{notebookId}")]
    [ProducesResponseType(typeof(IActionResult), 200)]
    public async Task<IActionResult> DeleteNotebook([FromRoute] int notebookId)
    {
        await notebookService.DeleteNotebook(notebookId);
        return Ok();
    }
}