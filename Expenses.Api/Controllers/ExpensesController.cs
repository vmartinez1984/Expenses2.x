using System.ComponentModel.DataAnnotations;
using Expenses.Core.Dtos;
using Expenses.Core.Interfaces.BusinessLayer;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.Api.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly ILogger<ExpensesController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ExpensesController(
        ILogger<ExpensesController> logger
        , IUnitOfWork unitOfWork
    )
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Get expense by id
    /// </summary>
    /// <param name="id">id</param>
    /// <returns>Expense</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(
        [MaxLength(24)]
        [MinLength(24)]
        string id
    )
    {
        ExpenseDto item;

        item = await _unitOfWork.Expense.GetAsync(id);
        if (item is null)
            return NotFound();

        return Ok(item);
    }

    /// <summary>
    /// Add a expense
    /// </summary>
    /// <param name="item">Expense</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post(ExpenseDtoIn item)
    {
        string id;

        id = await _unitOfWork.Expense.AddAsync(item);

        return Created($"Api/Expenses/{id}", new { Id = id });
    }

    /// <summary>
    /// Update expense
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="item">Expense</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Produces("application/json")]
    public async Task<IActionResult> Put(
        [MaxLength(24)]
        [MinLength(24)]string id, ExpenseDtoIn item)
    {
        await _unitOfWork.Expense.UpdateAsync(id, item);

        return Accepted($"Api/Expenses/{id}", new { Id = id });
    }

    /// <summary>
    /// delete a expense by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="204">Expense delete</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([MaxLength(24)]
        [MinLength(24)]string id)
    {
        ExpenseDto item = await  _unitOfWork.Expense.GetAsync(id);
        if (item is null)
            return NotFound(new { Message = "https://http.cat/404" });
        await _unitOfWork.Expense.DeleteAsync(id);

        return NoContent();
    }
}
