using Joaoaalves.Tiny.Abstractions.Enums;

namespace Joaoaalves.Tiny.Abstractions.Entities.Products;

/// <summary>
/// A lightweight projection of a product returned by the search endpoint.
/// Use <see cref="Product"/> to access the full detail set.
/// </summary>
public sealed class ProductSummary
{
    /// <summary>The Tiny internal ID of the product.</summary>
    public long Id { get; init; }

    /// <summary>The display name of the product.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>The seller's SKU code for the product.</summary>
    public string? Sku { get; init; }

    /// <summary>The regular sale price.</summary>
    public decimal Price { get; init; }

    /// <summary>The promotional sale price, when active.</summary>
    public decimal PromotionalPrice { get; init; }

    /// <summary>The direct cost price of the product.</summary>
    public decimal? CostPrice { get; init; }

    /// <summary>The weighted average cost price calculated by Tiny.</summary>
    public decimal? AverageCostPrice { get; init; }

    /// <summary>The unit of measure (e.g. "UN", "PC", "KG").</summary>
    public string? Unit { get; init; }

    /// <summary>The GTIN/EAN barcode of the product.</summary>
    public string? Gtin { get; init; }

    /// <summary>Whether this product is standalone, a parent, or a variation.</summary>
    public VariationType VariationType { get; init; }

    /// <summary>Physical location of the product in the warehouse.</summary>
    public string? Location { get; init; }

    /// <summary>The current lifecycle status of the product.</summary>
    public ProductStatus? Status { get; init; }

    /// <summary>When the product record was first created in Tiny.</summary>
    public DateTime? CreatedAt { get; init; }
}
