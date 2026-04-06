namespace Joaoaalves.Tiny.Abstractions.Entities.Stock;

/// <summary>
/// The current stock position of a product across all warehouses.
/// </summary>
public sealed class ProductStock
{
    /// <summary>The Tiny internal ID of the product.</summary>
    public long Id { get; init; }

    /// <summary>The display name of the product.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>The seller's SKU code for the product.</summary>
    public string? Sku { get; init; }

    /// <summary>The unit of measure (e.g. "UN", "KG").</summary>
    public string? Unit { get; init; }

    /// <summary>Total available stock across all non-excluded warehouses.</summary>
    public decimal Balance { get; init; }

    /// <summary>
    /// Stock reserved for pending orders.
    /// Only returned when the "Reserva de Estoques" extension is active on the account.
    /// </summary>
    public decimal? ReservedBalance { get; init; }

    /// <summary>Per-warehouse breakdown of the stock balance.</summary>
    public IReadOnlyList<StockWarehouse> Warehouses { get; init; } = [];
}
