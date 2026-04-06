namespace Joaoaalves.Tiny.Abstractions.Entities.Stock;

/// <summary>
/// The stock balance of a product within a specific warehouse (depósito) in Tiny.
/// </summary>
public sealed class StockWarehouse
{
    /// <summary>The name of the warehouse or storage location.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// When true, the balance in this warehouse is excluded from the product's total stock count.
    /// Corresponds to the <c>desconsiderar</c> field in the API.
    /// </summary>
    public bool Exclude { get; init; }

    /// <summary>The current available stock balance in this warehouse.</summary>
    public decimal Balance { get; init; }

    /// <summary>The alias of the company that owns this warehouse, for multi-company accounts.</summary>
    public string? Company { get; init; }
}
