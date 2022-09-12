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


    [HttpGet("{id}")]
    public async Task<IActionResult> Get([MaxLength(24)][MinLength(24)]string id)
    {
        ExpenseDto item;

        item = await _unitOfWork.Expense.GetAsync(id);
        if (item is null)
            return NotFound();

        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Post(ExpenseDtoIn item)
    {
        string id;

        id = await _unitOfWork.Expense.AddAsync(item);

        return Created($"Api/Expenses/{id}", new { Id = id });
    }


    // [HttpPut("{id}")]
    // public async Task<IActionResult> Put(string id, CategoryDtoIn item)
    // {

    //     await _unitOfWork.Category.UpdateAsync(id, item);

    //     return Created("/", new { Id = id });
    // }

    // [HttpDelete("{id}")]
    // public async Task<IActionResult> Delete(string id)
    // {

    //     await _unitOfWork.Category.DeleteAsync(id);

    //     return NoContent();
    // }
}
