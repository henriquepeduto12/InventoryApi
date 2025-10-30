using Inventory.Api.Enums;

namespace Inventory.Api.Dtos;

public record MovementDto(
    string Sku, MovementType Type, int Quantity,
    string? BatchCode, DateOnly? ExpirationDate
);