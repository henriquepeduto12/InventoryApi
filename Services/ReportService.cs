using Inventory.Api.Services;

namespace Inventory.Api.Services;

public class ReportService(ProductService products, StockMovementService movements)
{
    public Task<decimal> TotalStockValueAsync() => products.TotalStockValueAsync();
    public Task<List<Inventory.Api.Models.Product>> BelowMinimumAsync() => products.BelowMinAsync();
    public Task<List<Inventory.Api.Models.StockMovement>> ExpiringIn7DaysAsync() => movements.ExpiringIn7DaysAsync();
}
