using System.ComponentModel.DataAnnotations;
using Expenses.Core.Dtos;
using Expenses.Core.Interfaces.BusinessLayer;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.Api.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class EntriesController : ControllerBase
{
    private readonly ILogger<EntriesController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public EntriesController(
        ILogger<EntriesController> logger
        , IUnitOfWork unitOfWork
    )
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Get entry by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Entry</returns>
    /// <response code="200">Entry</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EntryDto), StatusCodes.Status200OK)]
    [Produces("application/json")]
    public async Task<IActionResult> Get(
        [MaxLength(24)]
        [MinLength(24)]
        string id
    )
    {
        EntryDto item;

        item = await _unitOfWork.Entry.GetAsync(id);
        if (item is null)
            return NotFound(new { Message = "https://http.cat/404" });

        return Ok(item);
    }

    /// <summary>
    /// Add Entry
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    /// <response code="200">Entry created</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Produces("application/json")]
    public async Task<IActionResult> Post(EntryDtoIn item)
    {
        string id;

        id = await _unitOfWork.Entry.AddAsync(item);

        return Created($"Api/Entries/{id}", new { Id = id });
    }

    /// <summary>
    /// Update entry
    /// </summary>
    /// <returns></returns>
    /// <response code="202">entry updated</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [Produces("application/json")]
    public async Task<IActionResult> Put(
        [MaxLength(24)]
        [MinLength(24)]
        string id,
        EntryDtoIn item)
    {
        EntryDto itemSearch;

        itemSearch = await _unitOfWork.Entry.GetAsync(id);
        if (itemSearch is null)
            return NotFound(new { Message = "https://http.cat/404" });

        await _unitOfWork.Entry.UpdateAsync(id, item);

        return Accepted($"Api/Entries/{id}", new { Id = id });
    }

    /// <summary>
    /// Delete entry
    /// </summary>
    /// <param name="id">id 24 characteres</param>
    /// <returns></returns>
    /// <response code="204">entry deleted</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(
        [MaxLength(24)]
        [MinLength(24)]
        string id
    )
    {
        EntryDto itemSearch;

        itemSearch = await _unitOfWork.Entry.GetAsync(id);
        if (itemSearch is null)
            return NotFound(new { Message = "https://http.cat/404" });

        await _unitOfWork.Entry.DeleteAsync(id);

        return NoContent();
    }

    /// <summary>
    /// Update entry (falta probar que funciona)
    /// </summary>
    /// <returns></returns>
    /// <response code="202">entry updated</response>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [Produces("application/json")]
    public async Task<IActionResult> Patch([MaxLength(24)][MinLength(24)]string id, JsonPatchDocument<EntryDtoIn> json)
    {
        EntryDtoIn item;

        item = await _unitOfWork.Entry.GetAsync(id);
        json.ApplyTo(item, ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await _unitOfWork.Entry.UpdateAsync(id, item);

        return Accepted($"Api/Entries/{id}", new { Id = id });
    }
}
