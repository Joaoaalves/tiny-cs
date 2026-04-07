# Service Interfaces

All three service interfaces live in `Joaoaalves.Tiny.Abstractions.Interfaces`. Inject them via DI after registering with `services.AddTiny(token)`.

## ITinyProductService

```csharp
public interface ITinyProductService
{
    Task<Product?> GetByIdAsync(long id, CancellationToken cancellationToken = default);

    Task<PagedResult<ProductSummary>> SearchAsync(
        SearchProductsRequest request,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<UpsertResult>> CreateAsync(
        IEnumerable<UpsertProductData> products,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<UpsertResult>> UpdateAsync(
        IEnumerable<UpsertProductData> products,
        CancellationToken cancellationToken = default);
}
```

`GetByIdAsync` returns `null` when the product does not exist. `SearchAsync` pages through results. `CreateAsync` and `UpdateAsync` accept batches and return one `UpsertResult` per input item.

## ITinyOrderService

```csharp
public interface ITinyOrderService
{
    Task<Order?> GetByIdAsync(long id, CancellationToken cancellationToken = default);

    Task<PagedResult<OrderSummary>> SearchAsync(
        SearchOrdersRequest request,
        CancellationToken cancellationToken = default);
}
```

`GetByIdAsync` returns the full order including all nested objects. `SearchAsync` supports 13 filter parameters and pagination.

## ITinyStockService

```csharp
public interface ITinyStockService
{
    Task<ProductStock?> GetByProductIdAsync(
        long productId,
        CancellationToken cancellationToken = default);

    Task<UpsertResult> UpdateAsync(
        UpdateStockData data,
        CancellationToken cancellationToken = default);

    Task<PagedResult<StockUpdateEntry>> ListUpdatesAsync(
        ListStockUpdatesRequest request,
        CancellationToken cancellationToken = default);
}
```

`GetByProductIdAsync` returns the current stock position including per-warehouse breakdown. `UpdateAsync` records a movement (entry, exit, or balance set). `ListUpdatesAsync` polls the real-time stock queue and requires the **"API para estoque em tempo real"** extension on the Tiny account.
