using Inventory.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers;

[ApiController]
[Route("api/reports")]
public class ReportsController(ReportService service) : ControllerBase
{
    [HttpGet("total-value")]
    public async Task<IActionResult> Total() => Ok(new { total = await service.TotalStockValueAsync() });

    [HttpGet("expiring-7-days")]
    public Task<object> Expiring() => service.ExpiringIn7DaysAsync().ContinueWith(t => (object)t.Result);

    [HttpGet("below-minimum")]
    public Task<object> BelowMin() => service.BelowMinimumAsync().ContinueWith(t => (object)t.Result);
}
