using Joaoaalves.Tiny.Abstractions.DTOs.Requests.Orders;
using Joaoaalves.Tiny.Abstractions.Entities.Common;
using Joaoaalves.Tiny.Abstractions.Entities.Orders;
using Joaoaalves.Tiny.Abstractions.Interfaces;
using Joaoaalves.Tiny.Core.Clients;
using Joaoaalves.Tiny.Core.Mappers;

namespace Joaoaalves.Tiny.Core.Services;

/// <summary>
/// Implements <see cref="ITinyOrderService"/> using <see cref="TinyOrderClient"/>.
/// Maps raw API responses to domain entities via <see cref="OrderMapper"/>.
/// </summary>
internal sealed class TinyOrderService : ITinyOrderService
{
    private readonly TinyOrderClient _client;

    public TinyOrderService(TinyOrderClient client) => _client = client;

    /// <inheritdoc />
    public async Task<Order?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var response = await _client.GetByIdAsync(id, cancellationToken);
        return response.Order is null ? null : OrderMapper.ToEntity(response.Order);
    }

    /// <inheritdoc />
    public async Task<PagedResult<OrderSummary>> SearchAsync(
        SearchOrdersRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await _client.SearchAsync(request, cancellationToken);

        var items = response.Orders?
            .Where(o => o.Order is not null)
            .Select(o => OrderMapper.ToSummary(o.Order!))
            .ToList() ?? [];

        return new PagedResult<OrderSummary>
        {
            Page = ProductMapper.ParseNullableInt(response.Page) ?? 1,
            TotalPages = ProductMapper.ParseNullableInt(response.TotalPages) ?? 1,
            Items = items
        };
    }
}
