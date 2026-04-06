namespace Joaoaalves.Tiny.Abstractions.Entities.Orders;

/// <summary>
/// A line item within an order, representing one product and its quantity.
/// </summary>
public sealed class OrderItem
{
    /// <summary>The SKU code of the product on this line.</summary>
    public string? Sku { get; init; }

    /// <summary>The product name or description shown on the order.</summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>The unit of measure for this line (e.g. "UN", "KG").</summary>
    public string? Unit { get; init; }

    /// <summary>Number of units ordered.</summary>
    public decimal Quantity { get; init; }

    /// <summary>Price per unit, excluding any freight or discounts.</summary>
    public decimal UnitPrice { get; init; }
}
