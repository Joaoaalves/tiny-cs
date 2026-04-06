using Joaoaalves.Tiny.Abstractions.Enums;

namespace Joaoaalves.Tiny.Abstractions.DTOs.Requests.Products;

/// <summary>
/// Filters and pagination options for the product search endpoint
/// (<c>produtos.pesquisa.php</c>).
/// At least one filter field must be provided.
/// </summary>
public sealed class SearchProductsRequest
{
    /// <summary>
    /// Free-text search term matched against the product name or SKU.
    /// Corresponds to the <c>pesquisa</c> query parameter.
    /// </summary>
    public string? Query { get; init; }

    /// <summary>
    /// Filter by product lifecycle status.
    /// When null, Tiny returns both active and inactive products.
    /// Corresponds to the <c>situacao</c> query parameter.
    /// </summary>
    public ProductStatus? Status { get; init; }

    /// <summary>
    /// Page number to retrieve (1-based). Defaults to 1.
    /// Each page contains up to 100 records.
    /// Corresponds to the <c>pagina</c> query parameter.
    /// </summary>
    public int Page { get; init; } = 1;
}
