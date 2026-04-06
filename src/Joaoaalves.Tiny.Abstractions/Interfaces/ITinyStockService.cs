using Joaoaalves.Tiny.Abstractions.DTOs.Requests.Stock;
using Joaoaalves.Tiny.Abstractions.Entities.Common;
using Joaoaalves.Tiny.Abstractions.Entities.Stock;

namespace Joaoaalves.Tiny.Abstractions.Interfaces;

/// <summary>
/// Provides access to the Tiny stock endpoints (API V2).
/// </summary>
public interface ITinyStockService
{
    /// <summary>
    /// Retrieves the current stock position of a product across all warehouses.
    /// Calls <c>produto.obter.estoque.php</c>.
    /// </summary>
    /// <param name="productId">The Tiny internal product ID.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>The stock position, or null if the product was not found.</returns>
    Task<ProductStock?> GetByProductIdAsync(long productId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Posts a stock movement (entry, exit, or balance) for a product.
    /// Calls <c>produto.atualizarestoque.php</c>.
    /// </summary>
    /// <param name="data">The stock movement details.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>The result of the operation, including the new balance.</returns>
    Task<UpsertResult> UpdateAsync(UpdateStockData data, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves products whose stock changed since the given timestamp, consuming them
    /// from the real-time stock update queue.
    /// Calls <c>produtos.atualizacoes.estoque.php</c>.
    /// Requires the "API para estoque em tempo real" extension on the account.
    /// </summary>
    /// <param name="request">Timestamp filter and page number.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>A page of products with updated stock.</returns>
    Task<PagedResult<StockUpdateEntry>> ListUpdatesAsync(
        ListStockUpdatesRequest request,
        CancellationToken cancellationToken = default);
}
