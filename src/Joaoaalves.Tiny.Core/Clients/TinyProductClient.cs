using System.Text.Json;
using Joaoaalves.Tiny.Abstractions.DTOs.Requests.Products;
using Joaoaalves.Tiny.Abstractions.Enums;
using Joaoaalves.Tiny.Core.DTOs.Common;
using Joaoaalves.Tiny.Core.DTOs.Products;
using Joaoaalves.Tiny.Core.Http;

namespace Joaoaalves.Tiny.Core.Clients;

/// <summary>
/// Typed client for the Tiny product endpoints.
/// Handles URL parameter construction and JSON serialization of request payloads.
/// </summary>
internal sealed class TinyProductClient
{
    private readonly TinyHttpClient _http;

    public TinyProductClient(TinyHttpClient http) => _http = http;

    /// <summary>Calls <c>produto.obter.php</c> with the given product ID.</summary>
    internal Task<TinyGetProductResponse> GetByIdAsync(long id, CancellationToken ct)
        => _http.PostAsync<TinyGetProductResponse>(
            "produto.obter.php",
            [new("id", id.ToString())],
            ct);

    /// <summary>Calls <c>produtos.pesquisa.php</c> with the given search filters.</summary>
    internal Task<TinySearchProductsResponse> SearchAsync(SearchProductsRequest request, CancellationToken ct)
    {
        var parameters = new List<KeyValuePair<string, string?>>();

        if (!string.IsNullOrEmpty(request.Query))
            parameters.Add(new("pesquisa", request.Query));

        if (request.Status.HasValue)
            parameters.Add(new("situacao", MapStatusToApi(request.Status.Value)));

        parameters.Add(new("pagina", request.Page.ToString()));

        return _http.PostAsync<TinySearchProductsResponse>("produtos.pesquisa.php", parameters, ct);
    }

    /// <summary>Calls <c>produto.incluir.php</c> with a batch of products to create.</summary>
    internal Task<TinyUpsertResponse> CreateAsync(IEnumerable<UpsertProductData> products, CancellationToken ct)
        => _http.PostAsync<TinyUpsertResponse>(
            "produto.incluir.php",
            [new("produto", SerializeUpsertRequest(products))],
            ct);

    /// <summary>Calls <c>produto.alterar.php</c> with a batch of products to update.</summary>
    internal Task<TinyUpsertResponse> UpdateAsync(IEnumerable<UpsertProductData> products, CancellationToken ct)
        => _http.PostAsync<TinyUpsertResponse>(
            "produto.alterar.php",
            [new("produto", SerializeUpsertRequest(products))],
            ct);

    private static string SerializeUpsertRequest(IEnumerable<UpsertProductData> products)
    {
        var request = new TinyUpsertProductRequestJson
        {
            Products = products.Select(p => new TinyUpsertProductItemJson
            {
                Product = new TinyUpsertProductDataJson
                {
                    Sequence = p.Sequence,
                    Id = p.Id?.ToString(),
                    Sku = p.Sku,
                    Name = p.Name,
                    Unit = p.Unit,
                    Price = p.Price,
                    PromotionalPrice = p.PromotionalPrice,
                    Ncm = p.Ncm,
                    Origin = p.Origin,
                    Gtin = p.Gtin,
                    PackagingGtin = p.PackagingGtin,
                    Location = p.Location,
                    NetWeight = p.NetWeight,
                    GrossWeight = p.GrossWeight,
                    MinimumStock = p.MinimumStock,
                    MaximumStock = p.MaximumStock,
                    SupplierId = p.SupplierId,
                    SupplierCode = p.SupplierCode,
                    SupplierProductCode = p.SupplierProductCode,
                    UnitsPerBox = p.UnitsPerBox,
                    CostPrice = p.CostPrice,
                    Status = MapStatusToApi(p.Status),
                    Type = MapTypeToApi(p.Type),
                    IpiClass = p.IpiClass,
                    FixedIpiValue = p.FixedIpiValue,
                    ServiceListCode = p.ServiceListCode,
                    AdditionalDescription = p.AdditionalDescription,
                    Notes = p.Notes,
                    Warranty = p.Warranty,
                    Cest = p.Cest,
                    PreparationDays = p.PreparationDays,
                    Brand = p.Brand,
                    PackagingType = p.PackagingType.HasValue ? (int)p.PackagingType.Value : null,
                    PackagingHeight = p.PackagingHeight,
                    PackagingWidth = p.PackagingWidth,
                    PackagingLength = p.PackagingLength,
                    PackagingDiameter = p.PackagingDiameter,
                    Category = p.Category,
                    ProductClass = p.ProductClass.HasValue ? MapProductClassToApi(p.ProductClass.Value) : null
                }
            }).ToList()
        };

        return JsonSerializer.Serialize(request, TinyHttpClient.RequestJsonOptions);
    }

    private static string MapStatusToApi(ProductStatus status) => status switch
    {
        ProductStatus.Inactive => "I",
        ProductStatus.Deleted => "E",
        _ => "A"
    };

    private static string MapTypeToApi(ProductType type) =>
        type == ProductType.Service ? "S" : "P";

    private static string MapProductClassToApi(ProductClass productClass) => productClass switch
    {
        ProductClass.Kit => "K",
        ProductClass.WithVariations => "V",
        ProductClass.Manufactured => "F",
        ProductClass.RawMaterial => "M",
        _ => "S"
    };
}
