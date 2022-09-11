using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniLojaVirtual.ProductApi.Dtos;
using MiniLojaVirtual.ProductApi.Roles;
using MiniLojaVirtual.ProductApi.Services;

namespace MiniLojaVirtual.ProductApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> Get()
    {
        var categoriesDto = await _categoryService.GetCategories();
        if (categoriesDto == null)
        {
            return NotFound("Categories not found");
        }

        return Ok(categoriesDto);
    }

    [HttpGet("products")]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoriesProducts()
    {
        var categoriesDto = await _categoryService.GetCategoriesProducts();
        if (categoriesDto == null)
        {
            return NotFound("Categories not found");
        }

        return Ok(categoriesDto);
    }

    [HttpGet("{id:int}", Name = "GetCategory")]
    public async Task<ActionResult<CategoryDto>> Get(int id)
    {
        var categoryDto = await _categoryService.GetCategoryById(id);
        if (categoryDto == null)
        {
            return NotFound("Category not found");
        }

        return Ok(categoryDto);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CategoryDto categoryDto)
    {
        if (categoryDto == null)
            return BadRequest("Invalid Data");

        await _categoryService.AddCategory(categoryDto);

        return new CreatedAtRouteResult("GetCategory", new { id = categoryDto.CategoryId },
            categoryDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] CategoryDto categoryDto)
    {
        if (id != categoryDto.CategoryId)
            return BadRequest();

        if (categoryDto == null)
            return BadRequest();

        await _categoryService.UpdateCategory(categoryDto);
        return Ok(categoryDto);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = Role.Admin)]
    public async Task<ActionResult<CategoryDto>> Delete(int id)
    {
        var categoryDto = await _categoryService.GetCategoryById(id);
        if (categoryDto == null)
        {
            return NotFound("Category not found");
        }

        await _categoryService.RemoveCategory(id);
        return Ok(categoryDto);
    }
}
