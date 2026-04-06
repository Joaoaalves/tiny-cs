using Joaoaalves.Tiny.Abstractions.DTOs.Requests.Orders;
using Joaoaalves.Tiny.Abstractions.Entities.Common;
using Joaoaalves.Tiny.Abstractions.Entities.Orders;

namespace Joaoaalves.Tiny.Abstractions.Interfaces;

/// <summary>
/// Provides access to the Tiny order endpoints (API V2).
/// </summary>
public interface ITinyOrderService
{
    /// <summary>
    /// Retrieves the full details of a single order by its Tiny ID.
    /// Calls <c>pedido.obter.php</c>.
    /// </summary>
    /// <param name="id">The Tiny internal order ID.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>The order, or null if not found.</returns>
    Task<Order?> GetByIdAsync(long id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches for orders matching the given filters.
    /// Calls <c>pedidos.pesquisa.php</c>.
    /// </summary>
    /// <param name="request">Filters and page number for the search.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>A page of matching order summaries.</returns>
    Task<PagedResult<OrderSummary>> SearchAsync(
        SearchOrdersRequest request,
        CancellationToken cancellationToken = default);
}
