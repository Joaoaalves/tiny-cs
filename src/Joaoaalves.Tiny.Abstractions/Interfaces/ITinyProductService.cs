using Joaoaalves.Tiny.Abstractions.DTOs.Requests.Products;
using Joaoaalves.Tiny.Abstractions.Entities.Common;
using Joaoaalves.Tiny.Abstractions.Entities.Products;

namespace Joaoaalves.Tiny.Abstractions.Interfaces;

/// <summary>
/// Provides access to the Tiny product endpoints (API V2).
/// </summary>
public interface ITinyProductService
{
    /// <summary>
    /// Retrieves the full details of a single product by its Tiny ID.
    /// Calls <c>produto.obter.php</c>.
    /// </summary>
    /// <param name="id">The Tiny internal product ID.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>The product, or null if not found.</returns>
    Task<Product?> GetByIdAsync(long id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches for products matching the given filters.
    /// Calls <c>produtos.pesquisa.php</c>.
    /// </summary>
    /// <param name="request">Filters and page number for the search.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>A page of matching product summaries.</returns>
    Task<PagedResult<ProductSummary>> SearchAsync(
        SearchProductsRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates one or more products in a single batch request.
    /// Calls <c>produto.incluir.php</c>.
    /// </summary>
    /// <param name="products">The products to create. Each must have a unique <c>Sequence</c>.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>One result per product in the batch, in sequence order.</returns>
    Task<IReadOnlyList<UpsertResult>> CreateAsync(
        IEnumerable<UpsertProductData> products,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates one or more existing products in a single batch request.
    /// Calls <c>produto.alterar.php</c>.
    /// </summary>
    /// <param name="products">The products to update. Each must have a unique <c>Sequence</c>.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>One result per product in the batch, in sequence order.</returns>
    Task<IReadOnlyList<UpsertResult>> UpdateAsync(
        IEnumerable<UpsertProductData> products,
        CancellationToken cancellationToken = default);
}
