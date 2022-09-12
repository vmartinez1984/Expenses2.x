using System.ComponentModel.DataAnnotations;
using Expenses.Core.Dtos;
using Expenses.Core.Interfaces.BusinessLayer;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.Api.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ILogger<CategoriesController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CategoriesController(
        ILogger<CategoriesController> logger
        , IUnitOfWork unitOfWork
    )
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Get list of categories
    /// </summary>
    /// <returns></returns>
    /// <returns>List of categories</returns>
    /// <response code="200">List of categories</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Produces("application/json")]
    public async Task<IEnumerable<CategoryDto>> Get()
    {
        List<CategoryDto> list;

        list = await _unitOfWork.Category.GetAsync();

        return list;
    }

    /// <summary>
    /// Get list of subcategories from category
    /// </summary>
    /// <param name="categoryId">categoryId 24 characteres</param>
    /// <returns>List of subcategories</returns>
    /// <response code="200">Subcategories</response>
    [HttpGet("/Api/Categories/{categoryId}/Subcategories")]
    [ProducesResponseType(typeof(List<SubcategoryDto>),StatusCodes.Status200OK)]
    [Produces("application/json")]
    public async Task<IActionResult> GetByCategory(
        [MaxLength(24)]
        [MinLength(24)]
        string categoryId
    )
    {
        CategoryDto item;

        item = await _unitOfWork.Category.GetAsync(categoryId);
        if (item is null)
            return NotFound(new { Message = "https://http.cat/404" });

        List<SubcategoryDto> list;

        list = await _unitOfWork.Subcategory.GetByCategoryIdAsync(categoryId);

        return Ok(list);
    }

    /// <summary>
    /// Get category by id
    /// </summary>
    /// <param name="id">categoryId</param>
    /// <returns>Category</returns>
    /// <response code="200">Category</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [Produces("application/json")]
    public async Task<IActionResult> Get(string id)
    {
        CategoryDto item;

        item = await _unitOfWork.Category.GetAsync(id);
        if (item is null)
            return NotFound(new { Message = "https://http.cat/404" });

        return Ok(item);
    }

    /// <summary>
    /// Add category
    /// </summary>
    /// <returns>Id</returns>
    /// <response code="201">Category created</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Produces("application/json")]
    public async Task<IActionResult> Post(CategoryDtoIn item)
    {
        string id;

        id = await _unitOfWork.Category.AddAsync(item);

        return Created($"Api/Categories/{id}", new { Id = id });
    }

    /// <summary>
    /// Update category
    /// </summary>
    /// <returns>Id</returns>
    /// <response code="202">Category updated</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [Produces("application/json")]
    public async Task<IActionResult> Put(string id, CategoryDtoIn item)
    {
        CategoryDto category;

        category = await _unitOfWork.Category.GetAsync(id);
        if (category is null)
            return NotFound(new { Message = "https://http.cat/404" });

        await _unitOfWork.Category.UpdateAsync(id, item);

        return Accepted($"Api/Categories/{id}", new { Id = id });
    }

    /// <summary>
    /// Delete category
    /// </summary>
    /// <returns>Id</returns>
    /// <response code="204">Category updated</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var item = _unitOfWork.Category.GetAsync(id);
        if (item is null)
            return NotFound(new { Message = "https://http.cat/404" });
        await _unitOfWork.Category.DeleteAsync(id);

        return NoContent();
    }
}
