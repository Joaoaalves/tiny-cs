using Joaoaalves.Tiny.Abstractions.DTOs.Requests.Stock;
using Joaoaalves.Tiny.Core.DTOs.Stock;

namespace Joaoaalves.Tiny.Core.Clients;

internal interface ITinyStockClient
{
    Task<TinyGetProductStockResponse> GetByProductIdAsync(long productId, CancellationToken ct);
    Task<TinyUpdateStockResponse> UpdateAsync(UpdateStockData data, CancellationToken ct);
    Task<TinyListStockUpdatesResponse> ListUpdatesAsync(ListStockUpdatesRequest request, CancellationToken ct);
}
