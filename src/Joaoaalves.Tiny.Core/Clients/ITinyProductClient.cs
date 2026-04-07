using Joaoaalves.Tiny.Abstractions.DTOs.Requests.Products;
using Joaoaalves.Tiny.Core.DTOs.Common;
using Joaoaalves.Tiny.Core.DTOs.Products;

namespace Joaoaalves.Tiny.Core.Clients;

internal interface ITinyProductClient
{
    Task<TinyGetProductResponse> GetByIdAsync(long id, CancellationToken ct);
    Task<TinySearchProductsResponse> SearchAsync(SearchProductsRequest request, CancellationToken ct);
    Task<TinyUpsertResponse> CreateAsync(IEnumerable<UpsertProductData> products, CancellationToken ct);
    Task<TinyUpsertResponse> UpdateAsync(IEnumerable<UpsertProductData> products, CancellationToken ct);
}
