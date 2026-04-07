using Joaoaalves.Tiny.Abstractions.DTOs.Requests.Orders;
using Joaoaalves.Tiny.Core.DTOs.Orders;

namespace Joaoaalves.Tiny.Core.Clients;

internal interface ITinyOrderClient
{
    Task<TinyGetOrderResponse> GetByIdAsync(long id, CancellationToken ct);
    Task<TinySearchOrdersResponse> SearchAsync(SearchOrdersRequest request, CancellationToken ct);
}
