using Joaoaalves.Tiny.Abstractions.DTOs.Requests.Orders;
using Joaoaalves.Tiny.Abstractions.DTOs.Requests.Products;
using Joaoaalves.Tiny.Abstractions.DTOs.Requests.Stock;
using Joaoaalves.Tiny.Abstractions.Interfaces;
using Joaoaalves.Tiny.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Joaoaalves.Tiny.Core.Tests.Integration;

/// <summary>
/// Read-only integration tests against the live Tiny API V2.
///
/// Rules that must never be violated:
///   1. Only read endpoints may be called (pesquisa, obter, listar).
///      CreateAsync, UpdateAsync, and UpdateStockAsync are FORBIDDEN here.
///   2. No test may cause any side-effect in the Tiny account (no records
///      created, altered, or deleted, no stock movements, no invoices).
///   3. The test classes expose only the read-only service interfaces to make
///      this boundary enforceable by the compiler.
///
/// Run with:
///   $env:TINY_TOKEN="your_token"; dotnet test --filter "Category=Integration"
/// </summary>
[Trait("Category", "Integration")]
public class TinyIntegrationTests
{
    private readonly ITinyProductReadService _products;
    private readonly ITinyOrderReadService _orders;
    private readonly ITinyStockReadService _stock;

    public TinyIntegrationTests()
    {
        var token = Environment.GetEnvironmentVariable("TINY_TOKEN")
            ?? throw new InvalidOperationException(
                "Set TINY_TOKEN environment variable to a valid Tiny API token before running integration tests.");

        var provider = new ServiceCollection()
            .AddTiny(token)
            .BuildServiceProvider();

        _products = new ProductReadOnlyGateway(provider.GetRequiredService<ITinyProductService>());
        _orders   = new OrderReadOnlyGateway(provider.GetRequiredService<ITinyOrderService>());
        _stock    = new StockReadOnlyGateway(provider.GetRequiredService<ITinyStockService>());
    }

    [Fact]
    public async Task Products_SearchAsync_ReturnsAtLeastOnePage()
    {
        var result = await _products.SearchAsync(new SearchProductsRequest { Page = 1 });

        Assert.NotNull(result);
        Assert.True(result.TotalPages >= 1);
        Assert.True(result.Items.Count >= 0);
    }

    [Fact]
    public async Task Orders_SearchAsync_ReturnsAtLeastOnePage()
    {
        var result = await _orders.SearchAsync(new SearchOrdersRequest { Page = 1 });

        Assert.NotNull(result);
        Assert.True(result.TotalPages >= 1);
    }

    [Fact]
    public async Task Stock_ListUpdatesAsync_ReturnsResult()
    {
        var result = await _stock.ListUpdatesAsync(new ListStockUpdatesRequest
        {
            UpdatedSince = DateTime.Today.AddDays(-7),
            Page = 1
        });

        Assert.NotNull(result);
        Assert.True(result.TotalPages >= 0);
    }
}

// These narrow interfaces are intentionally limited to the read-only surface.
// Adding a write operation here requires a deliberate, visible change to this file.

internal interface ITinyProductReadService
{
    Task<Joaoaalves.Tiny.Abstractions.Entities.Common.PagedResult<Joaoaalves.Tiny.Abstractions.Entities.Products.ProductSummary>> SearchAsync(SearchProductsRequest request, CancellationToken ct = default);
    Task<Joaoaalves.Tiny.Abstractions.Entities.Products.Product?> GetByIdAsync(long id, CancellationToken ct = default);
}

internal interface ITinyOrderReadService
{
    Task<Joaoaalves.Tiny.Abstractions.Entities.Common.PagedResult<Joaoaalves.Tiny.Abstractions.Entities.Orders.OrderSummary>> SearchAsync(SearchOrdersRequest request, CancellationToken ct = default);
    Task<Joaoaalves.Tiny.Abstractions.Entities.Orders.Order?> GetByIdAsync(long id, CancellationToken ct = default);
}

internal interface ITinyStockReadService
{
    Task<Joaoaalves.Tiny.Abstractions.Entities.Common.PagedResult<Joaoaalves.Tiny.Abstractions.Entities.Stock.StockUpdateEntry>> ListUpdatesAsync(ListStockUpdatesRequest request, CancellationToken ct = default);
    Task<Joaoaalves.Tiny.Abstractions.Entities.Stock.ProductStock?> GetByProductIdAsync(long productId, CancellationToken ct = default);
}

internal sealed class ProductReadOnlyGateway(ITinyProductService inner) : ITinyProductReadService
{
    public Task<Joaoaalves.Tiny.Abstractions.Entities.Common.PagedResult<Joaoaalves.Tiny.Abstractions.Entities.Products.ProductSummary>> SearchAsync(SearchProductsRequest request, CancellationToken ct = default) => inner.SearchAsync(request, ct);
    public Task<Joaoaalves.Tiny.Abstractions.Entities.Products.Product?> GetByIdAsync(long id, CancellationToken ct = default) => inner.GetByIdAsync(id, ct);
}

internal sealed class OrderReadOnlyGateway(ITinyOrderService inner) : ITinyOrderReadService
{
    public Task<Joaoaalves.Tiny.Abstractions.Entities.Common.PagedResult<Joaoaalves.Tiny.Abstractions.Entities.Orders.OrderSummary>> SearchAsync(SearchOrdersRequest request, CancellationToken ct = default) => inner.SearchAsync(request, ct);
    public Task<Joaoaalves.Tiny.Abstractions.Entities.Orders.Order?> GetByIdAsync(long id, CancellationToken ct = default) => inner.GetByIdAsync(id, ct);
}

internal sealed class StockReadOnlyGateway(ITinyStockService inner) : ITinyStockReadService
{
    public Task<Joaoaalves.Tiny.Abstractions.Entities.Common.PagedResult<Joaoaalves.Tiny.Abstractions.Entities.Stock.StockUpdateEntry>> ListUpdatesAsync(ListStockUpdatesRequest request, CancellationToken ct = default) => inner.ListUpdatesAsync(request, ct);
    public Task<Joaoaalves.Tiny.Abstractions.Entities.Stock.ProductStock?> GetByProductIdAsync(long productId, CancellationToken ct = default) => inner.GetByProductIdAsync(productId, ct);
}
