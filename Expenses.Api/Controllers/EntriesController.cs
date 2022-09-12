using Expenses.Core.Dtos;
using Expenses.Core.Interfaces.BusinessLayer;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.Api.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class EntriesController : ControllerBase
{
    private readonly ILogger<PeriodsController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public EntriesController(
        ILogger<PeriodsController> logger
        , IUnitOfWork unitOfWork
    )
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    
    // [HttpGet("{id}")]
    // public async Task<CategoryDto> Get(string id)
    // {
    //     CategoryDto item;

    //     item = await _unitOfWork.Category.GetAsync(id);

    //     return item;
    // }

    [HttpPost]
    public async Task<IActionResult> Post(EntryDtoIn item)
    {
        string id;

        id = await _unitOfWork.Entry.AddAsync(item);

        return Created($"Api/Entries/{id}", new { Id = id });
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
