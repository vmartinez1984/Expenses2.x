using Expenses.Core.Dtos;
using Expenses.Core.Interfaces.BusinessLayer;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.Api.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class SubcategoriesController : ControllerBase
{
    private readonly ILogger<PeriodsController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public SubcategoriesController(
        ILogger<PeriodsController> logger
        , IUnitOfWork unitOfWork
    )
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IEnumerable<SubcategoryDto>> Get()
    {
        List<SubcategoryDto> list;

        list = await _unitOfWork.Subcategory.GetAsync();

        return list;
    }

    [HttpGet("{id}")]
    public async Task<SubcategoryDto> Get(string id)
    {
        SubcategoryDto item;

        item = await _unitOfWork.Subcategory.GetAsync(id);

        return item;
    }

    [HttpPost]
    public async Task<IActionResult> Post(SubcategoryDtoIn item)
    {
        string id;

        id = await _unitOfWork.Subcategory.AddAsync(item);

        return Created($"Api/Subcategories/{id}", new { Id = id });
    }


    [HttpPatch("{id}")]
    public async Task<IActionResult> Put(string id, JsonPatchDocument<SubcategoryDtoIn> json)
    {
        SubcategoryDto item;

        item = await _unitOfWork.Subcategory.GetAsync(id);
        json.ApplyTo(item, ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await _unitOfWork.Subcategory.UpdateAsync(id, item);

        return Accepted($"Api/Subcategories/{id}", new { Id = id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Patch(string id, SubcategoryDtoIn item)
    {

        await _unitOfWork.Subcategory.UpdateAsync(id, item);

        return Accepted($"Api/Subcategories/{id}", new { Id = id });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {

        await _unitOfWork.Subcategory.DeleteAsync(id);

        return NoContent();
    }
}
