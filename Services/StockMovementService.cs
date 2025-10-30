using Inventory.Api.Data;
using Inventory.Api.Enums;
using Inventory.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Api.Services;

public class StockMovementService(AppDbContext db)
{
    public async Task<StockMovement> RegisterAsync(
        string sku, MovementType type, int qty, string? batch, DateOnly? exp)
    {
        if (qty <= 0) throw new ArgumentException("Quantidade deve ser positiva");

        var product = await db.Products.FirstOrDefaultAsync(p => p.Sku == sku)
            ?? throw new InvalidOperationException("Produto não encontrado");

        if (product.Category == Category.PERECIVEL)
        {
            if (string.IsNullOrWhiteSpace(batch)) throw new ArgumentException("Lote é obrigatório para perecíveis");
            if (exp is null) throw new ArgumentException("Data de validade é obrigatória para perecíveis");
            if (DateOnly.FromDateTime(DateTime.UtcNow) > exp)
                throw new InvalidOperationException("Movimentação não permitida: lote vencido");
        }
        else { batch = null; exp = null; }

        int delta = type == MovementType.ENTRADA ? qty : -qty;
        int newQty = product.CurrentQuantity + delta;
        if (newQty < 0) throw new InvalidOperationException("Saldo insuficiente para saída");

        product.CurrentQuantity = newQty;

        var m = new StockMovement {
            ProductId = product.Id, Type = type, Quantity = qty,
            BatchCode = batch, ExpirationDate = exp
        };

        db.StockMovements.Add(m);
        await db.SaveChangesAsync();
        return m;
    }

    public Task<List<StockMovement>> ExpiringIn7DaysAsync()
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var in7 = today.AddDays(7);
        return db.StockMovements
            .Include(m => m.Product)
            .Where(m => m.Product.Category == Category.PERECIVEL
                     && m.ExpirationDate >= today && m.ExpirationDate <= in7)
            .ToListAsync();
    }
}
