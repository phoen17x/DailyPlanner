﻿using System.Security.Claims;
using AutoMapper;
using DailyPlanner.Api.Controllers.Notebooks.Models;
using DailyPlanner.Common.Responses;
using DailyPlanner.Common.Security;
using DailyPlanner.Services.Notebooks;
using DailyPlanner.Services.Notebooks.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailyPlanner.Api.Controllers.Notebooks;

/// <summary>
/// API endpoints for managing notebooks.
/// </summary>
[ProducesResponseType(typeof(ErrorResponse), 400)]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/notebooks")]
[Authorize]
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
    /// <returns>A collection of <see cref="NotebookResponse"/> objects.</returns>
    [HttpGet]
    [Authorize(AppScopes.PlannerAccess)]
    [ProducesResponseType(typeof(IEnumerable<NotebookResponse>), 200)]
    public async Task<IEnumerable<NotebookResponse>> GetNotebooks()
    {
        var userId = Guid.Parse((ReadOnlySpan<char>)User.FindFirstValue(ClaimTypes.NameIdentifier));
        var notebooks = await notebookService.GetNotebooks(userId);
        return mapper.Map<IEnumerable<NotebookResponse>>(notebooks);
    }

    /// <summary>
    /// Gets a single notebook by id.
    /// </summary>
    /// <param name="notebookId">The id of the notebook to get.</param>
    /// <returns>A <see cref="NotebookResponse"/> object.</returns>
    [HttpGet("{notebookId}")]
    [Authorize(AppScopes.PlannerAccess)]
    [ProducesResponseType(typeof(NotebookResponse), 200)]
    public async Task<NotebookResponse> GetNotebook([FromRoute] int notebookId)
    {
        var userId = Guid.Parse((ReadOnlySpan<char>)User.FindFirstValue(ClaimTypes.NameIdentifier));
        var notebook = await notebookService.GetNotebook(userId, notebookId);
        return mapper.Map<NotebookResponse>(notebook);
    }

    /// <summary>
    /// Adds a new notebook.
    /// </summary>
    /// <param name="request">The <see cref="AddNotebookRequest"/> object containing the notebook details.</param>
    /// <returns>A <see cref="NotebookResponse"/> object.</returns>
    [HttpPost]
    [Authorize(AppScopes.PlannerAccess)]
    [ProducesResponseType(typeof(NotebookResponse), 200)]
    public async Task<NotebookResponse> AddNotebook([FromBody] AddNotebookRequest request)
    {
        var model = mapper.Map<AddNotebookModel>(request);
        model.UserId = Guid.Parse((ReadOnlySpan<char>)User.FindFirstValue(ClaimTypes.NameIdentifier));
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
    [Authorize(AppScopes.PlannerAccess)]
    [ProducesResponseType(typeof(IActionResult), 200)]
    public async Task<IActionResult> UpdateNotebook(
        [FromRoute] int notebookId, 
        [FromBody] UpdateNotebookRequest request)
    {
        var model = mapper.Map<UpdateNotebookModel>(request);
        model.UserId = Guid.Parse((ReadOnlySpan<char>)User.FindFirstValue(ClaimTypes.NameIdentifier));
        await notebookService.UpdateNotebook(notebookId, model);
        return Ok();
    }

    /// <summary>
    /// Deletes notebook by id.
    /// </summary>
    /// <param name="notebookId">The id of the notebook to delete.</param>
    /// <returns>An <see cref="IActionResult"/> indicating success or failure.</returns>
    [HttpDelete("{notebookId}")]
    [Authorize(AppScopes.PlannerAccess)]
    [ProducesResponseType(typeof(IActionResult), 200)]
    public async Task<IActionResult> DeleteNotebook([FromRoute] int notebookId)
    {
        var userId = Guid.Parse((ReadOnlySpan<char>)User.FindFirstValue(ClaimTypes.NameIdentifier));
        await notebookService.DeleteNotebook(userId, notebookId);
        return Ok();
    }
}