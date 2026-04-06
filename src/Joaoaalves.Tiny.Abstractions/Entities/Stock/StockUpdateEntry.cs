using Joaoaalves.Tiny.Abstractions.Enums;

namespace Joaoaalves.Tiny.Abstractions.Entities.Stock;

/// <summary>
/// A product whose stock was updated recently, returned by the stock-updates queue endpoint.
/// Requires the "API para estoque em tempo real" extension to be active on the Tiny account.
/// </summary>
public sealed class StockUpdateEntry
{
    /// <summary>The Tiny internal ID of the product.</summary>
    public long Id { get; init; }

    /// <summary>The display name of the product.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>The seller's SKU code for the product.</summary>
    public string? Sku { get; init; }

    /// <summary>The unit of measure (e.g. "UN", "KG").</summary>
    public string? Unit { get; init; }

    /// <summary>Whether this product is standalone, a parent, or a variation.</summary>
    public VariationType VariationType { get; init; }

    /// <summary>Physical location of the product in the warehouse.</summary>
    public string? Location { get; init; }

    /// <summary>When the stock balance was last changed.</summary>
    public DateTime? UpdatedAt { get; init; }

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
