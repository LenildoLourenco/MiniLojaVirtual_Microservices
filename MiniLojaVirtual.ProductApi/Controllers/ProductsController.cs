using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniLojaVirtual.ProductApi.Dtos;
using MiniLojaVirtual.ProductApi.Roles;
using MiniLojaVirtual.ProductApi.Services;

namespace MiniLojaVirtual.ProductApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
    {
        var productsDto = await _productService.GetProducts();
        if (productsDto == null)
        {
            return NotFound("Products not found");
        }
        return Ok(productsDto);
    }

    [HttpGet("{id}", Name = "GetProduct")]
    public async Task<ActionResult<ProductDto>> Get(int id)
    {
        var productDto = await _productService.GetProductById(id);
        if (productDto == null)
        {
            return NotFound("Product not found");
        }
        return Ok(productDto);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProductDto productDto)
    {
        if (productDto == null)
            return BadRequest("Invalid Data");

        await _productService.AddProduct(productDto);

        return new CreatedAtRouteResult("GetProduct",
            new { id = productDto.Id }, productDto);
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody] ProductDto productDto)
    {
        if (productDto == null)
            return BadRequest("Invalid data");

        await _productService.UpdateProduct(productDto);
        return Ok(productDto);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = Role.Admin)]
    public async Task<ActionResult<ProductDto>> Delete(int id)
    {
        var productDto = await _productService.GetProductById(id);

        if (productDto == null)
        {
            return NotFound("Product not found");
        }

        await _productService.RemoveProduct(id);
        return Ok(productDto);
    }
}
