namespace Joaoaalves.Tiny.Abstractions.Entities.Products;

/// <summary>
/// A specific variation of a parent product, defined by a combination of grid attributes
/// such as size and colour.
/// </summary>
public sealed class ProductVariation
{
    /// <summary>The Tiny ID of this variation record.</summary>
    public long Id { get; init; }

    /// <summary>The SKU code assigned to this variation.</summary>
    public string? Sku { get; init; }

    /// <summary>The sale price for this specific variation.</summary>
    public decimal Price { get; init; }

    /// <summary>
    /// The grid attributes that distinguish this variation from others in the family.
    /// Keys are attribute names (e.g. "Tamanho", "Cor") and values are the selected options
    /// (e.g. "GG", "Branco").
    /// </summary>
    public IReadOnlyDictionary<string, string> Grid { get; init; } = new Dictionary<string, string>();
}
