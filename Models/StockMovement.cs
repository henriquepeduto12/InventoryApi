using System.ComponentModel.DataAnnotations;
using Inventory.Api.Enums;

namespace Inventory.Api.Models;

public class StockMovement
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required] public Guid ProductId { get; set; }
    public Product Product { get; set; } = default!;

    [Required] public MovementType Type { get; set; }
    [Range(1,int.MaxValue)] public int Quantity { get; set; }
    public DateTime MovedAt { get; set; } = DateTime.UtcNow;

    // Para perecíveis (validados na regra)
    public string? BatchCode { get; set; }
    public DateOnly? ExpirationDate { get; set; }
}