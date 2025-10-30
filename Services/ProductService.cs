using Inventory.Api.Data;
using Inventory.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Api.Services;

public class ProductService(AppDbContext db)
{
    public async Task<Product> CreateAsync(Product p)
    {
        if (await db.Products.AnyAsync(x => x.Sku == p.Sku))
            throw new InvalidOperationException("SKU já cadastrado");
        if (p.UnitPrice < 0) throw new ArgumentException("Preço não pode ser negativo");
        if (p.MinQuantity < 0) throw new ArgumentException("Quantidade mínima inválida");
        p.CurrentQuantity = Math.Max(p.CurrentQuantity, 0);
        db.Products.Add(p);
        await db.SaveChangesAsync();
        return p;
    }

    public Task<Product?> GetBySkuAsync(string sku) =>
        db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Sku == sku);

    public Task<List<Product>> BelowMinAsync() =>
        db.Products.Where(p => p.CurrentQuantity < p.MinQuantity).ToListAsync();

    public async Task<decimal> TotalStockValueAsync() =>
        await db.Products.SumAsync(p => p.UnitPrice * p.CurrentQuantity);
}
