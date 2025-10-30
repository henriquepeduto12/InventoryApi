using Inventory.Api.Dtos;
using Inventory.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers;

[ApiController]
[Route("api/movements")]
public class MovementsController(StockMovementService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Move(MovementDto dto)
        => Ok(await service.RegisterAsync(dto.Sku, dto.Type, dto.Quantity, dto.BatchCode, dto.ExpirationDate));
}
