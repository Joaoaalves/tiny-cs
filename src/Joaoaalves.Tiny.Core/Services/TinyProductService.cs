using Joaoaalves.Tiny.Abstractions.DTOs.Requests.Products;
using Joaoaalves.Tiny.Abstractions.Entities.Common;
using Joaoaalves.Tiny.Abstractions.Entities.Products;
using Joaoaalves.Tiny.Abstractions.Interfaces;
using Joaoaalves.Tiny.Core.Clients;
using Joaoaalves.Tiny.Core.Mappers;

namespace Joaoaalves.Tiny.Core.Services;

/// <summary>
/// Implements <see cref="ITinyProductService"/> using <see cref="TinyProductClient"/>.
/// Maps raw API responses to domain entities via <see cref="ProductMapper"/>.
/// </summary>
internal sealed class TinyProductService : ITinyProductService
{
    private readonly ITinyProductClient _client;

    public TinyProductService(ITinyProductClient client) => _client = client;

    /// <inheritdoc />
    public async Task<Product?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var response = await _client.GetByIdAsync(id, cancellationToken);
        return response.Product is null ? null : ProductMapper.ToEntity(response.Product);
    }

    /// <inheritdoc />
    public async Task<PagedResult<ProductSummary>> SearchAsync(
        SearchProductsRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await _client.SearchAsync(request, cancellationToken);

        var items = response.Products?
            .Where(p => p.Product is not null)
            .Select(p => ProductMapper.ToSummary(p.Product!))
            .ToList() ?? [];

        return new PagedResult<ProductSummary>
        {
            Page = ProductMapper.ParseNullableInt(response.Page) ?? 1,
            TotalPages = ProductMapper.ParseNullableInt(response.TotalPages) ?? 1,
            Items = items
        };
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<UpsertResult>> CreateAsync(
        IEnumerable<UpsertProductData> products,
        CancellationToken cancellationToken = default)
    {
        var response = await _client.CreateAsync(products, cancellationToken);
        return MapUpsertResults(response.Records);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<UpsertResult>> UpdateAsync(
        IEnumerable<UpsertProductData> products,
        CancellationToken cancellationToken = default)
    {
        var response = await _client.UpdateAsync(products, cancellationToken);
        return MapUpsertResults(response.Records);
    }

    private static IReadOnlyList<UpsertResult> MapUpsertResults(
        IEnumerable<DTOs.Common.TinyUpsertRegistroListItem>? records)
    {
        if (records is null) return [];

        return records
            .Where(r => r.Record is not null)
            .Select(r =>
            {
                var rec = r.Record!;
                var errors = rec.Errors?
                    .Select(e => e.Error ?? string.Empty)
                    .Where(e => e.Length > 0)
                    .ToList() ?? [];

                var variationIds = rec.Variations?
                    .Where(v => v.Variation is not null)
                    .Select(v => ProductMapper.ParseLong(v.Variation!.Id))
                    .Where(id => id != 0)
                    .ToList() ?? [];

                return new UpsertResult
                {
                    Sequence = ProductMapper.ParseNullableInt(rec.Sequence) ?? 0,
                    Success = string.Equals(rec.Status, "OK", StringComparison.OrdinalIgnoreCase),
                    Id = ProductMapper.ParseNullableLong(rec.Id),
                    Errors = errors,
                    VariationIds = variationIds
                };
            })
            .ToList();
    }
}
