using Joaoaalves.Tiny.Abstractions.DTOs.Requests.Stock;
using Joaoaalves.Tiny.Abstractions.Entities.Common;
using Joaoaalves.Tiny.Abstractions.Entities.Stock;
using Joaoaalves.Tiny.Abstractions.Interfaces;
using Joaoaalves.Tiny.Core.Clients;
using Joaoaalves.Tiny.Core.Mappers;

namespace Joaoaalves.Tiny.Core.Services;

/// <summary>
/// Implements <see cref="ITinyStockService"/> using <see cref="TinyStockClient"/>.
/// Maps raw API responses to domain entities via <see cref="StockMapper"/>.
/// </summary>
internal sealed class TinyStockService : ITinyStockService
{
    private readonly ITinyStockClient _client;

    public TinyStockService(ITinyStockClient client) => _client = client;

    /// <inheritdoc />
    public async Task<ProductStock?> GetByProductIdAsync(long productId, CancellationToken cancellationToken = default)
    {
        var response = await _client.GetByProductIdAsync(productId, cancellationToken);
        return response.Product is null ? null : StockMapper.ToEntity(response.Product);
    }

    /// <inheritdoc />
    public async Task<UpsertResult> UpdateAsync(UpdateStockData data, CancellationToken cancellationToken = default)
    {
        var response = await _client.UpdateAsync(data, cancellationToken);
        var record = response.Registros?.Record;

        if (record is null)
            return new UpsertResult { Success = false, Errors = ["No record returned from API."] };

        var errors = record.Errors?
            .Select(e => e.Error ?? string.Empty)
            .Where(e => e.Length > 0)
            .ToList() ?? [];

        return new UpsertResult
        {
            Sequence = ProductMapper.ParseNullableInt(record.Sequence) ?? 1,
            Success = string.Equals(record.Status, "OK", StringComparison.OrdinalIgnoreCase),
            Id = ProductMapper.ParseNullableLong(record.Id),
            Errors = errors
        };
    }

    /// <inheritdoc />
    public async Task<PagedResult<StockUpdateEntry>> ListUpdatesAsync(
        ListStockUpdatesRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await _client.ListUpdatesAsync(request, cancellationToken);

        var items = response.Products?
            .Where(p => p.Product is not null)
            .Select(p => StockMapper.ToUpdateEntry(p.Product!))
            .ToList() ?? [];

        return new PagedResult<StockUpdateEntry>
        {
            Page = ProductMapper.ParseNullableInt(response.Page) ?? 1,
            TotalPages = ProductMapper.ParseNullableInt(response.TotalPages) ?? 1,
            Items = items
        };
    }
}
