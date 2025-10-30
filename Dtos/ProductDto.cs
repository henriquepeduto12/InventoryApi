using Inventory.Api.Enums;

namespace Inventory.Api.Dtos;

public record ProductDto(
    string Sku, string Name, Category Category, decimal UnitPrice,
    int MinQuantity, int CurrentQuantity
);