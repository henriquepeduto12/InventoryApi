using Inventory.Api.Dtos;
using Inventory.Api.Enums;
using Inventory.Api.Models;
using Inventory.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController(ProductService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Product>> Create(ProductDto dto)
    {
        var p = new Product {
            Sku = dto.Sku, Name = dto.Name, Category = dto.Category,
            UnitPrice = dto.UnitPrice, MinQuantity = dto.MinQuantity,
            CurrentQuantity = dto.CurrentQuantity
        };
        return Ok(await service.CreateAsync(p));
    }

    [HttpGet("{sku}")]
    public async Task<ActionResult<Product>> GetBySku(string sku)
        => (await service.GetBySkuAsync(sku)) is { } p ? Ok(p) : NotFound();

    [HttpGet("below-min")]
    public Task<List<Product>> BelowMin() => service.BelowMinAsync();
}
