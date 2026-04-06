namespace Joaoaalves.Tiny.Abstractions.Entities.Products;

/// <summary>
/// Represents a component product and its quantity within a kit product.
/// </summary>
public sealed class ProductKitItem
{
    /// <summary>The Tiny ID of the component product.</summary>
    public long ProductId { get; init; }

    /// <summary>How many units of the component are included in one kit.</summary>
    public decimal Quantity { get; init; }
}
