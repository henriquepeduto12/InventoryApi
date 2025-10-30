using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Inventory.Api.Enums;

namespace Inventory.Api.Models;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required] public string Sku { get; set; } = default!;
    [Required] public string Name { get; set; } = default!;
    [Required] public Category Category { get; set; }
    [Range(0,double.MaxValue)] public decimal UnitPrice { get; set; }
    [Range(0,int.MaxValue)] public int MinQuantity { get; set; }
    [Range(0,int.MaxValue)] public int CurrentQuantity { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}