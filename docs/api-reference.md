# API Reference

## ITinyProductService

### GetByIdAsync

```csharp
Task<Product?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
```

Returns the full product for the given Tiny product ID, or `null` if not found.

### SearchAsync

```csharp
Task<PagedResult<ProductSummary>> SearchAsync(
    SearchProductsRequest request,
    CancellationToken cancellationToken = default)
```

Returns a paged list of product summaries matching the given filters.

| Parameter | Type | Description |
|---|---|---|
| `Query` | `string?` | Free-text search across name and SKU |
| `Status` | `ProductStatus?` | Filter by status (Active, Inactive, Deleted) |
| `Page` | `int` | Page number, 1-based (default: 1) |

### CreateAsync

```csharp
Task<IReadOnlyList<UpsertResult>> CreateAsync(
    IEnumerable<UpsertProductData> products,
    CancellationToken cancellationToken = default)
```

Creates one or more products in a single batch call. Returns one `UpsertResult` per input item, in the same order. Check `UpsertResult.Success` before using `Id`.

### UpdateAsync

```csharp
Task<IReadOnlyList<UpsertResult>> UpdateAsync(
    IEnumerable<UpsertProductData> products,
    CancellationToken cancellationToken = default)
```

Updates one or more existing products. `UpsertProductData.Id` must be set.

---

## ITinyOrderService

### GetByIdAsync

```csharp
Task<Order?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
```

Returns the full order for the given Tiny order ID, including customer, items, installments, markers, and integrated payments. Returns `null` if not found.

### SearchAsync

```csharp
Task<PagedResult<OrderSummary>> SearchAsync(
    SearchOrdersRequest request,
    CancellationToken cancellationToken = default)
```

Returns a paged list of order summaries. All filter fields are optional.

| Parameter | Type | Description |
|---|---|---|
| `Number` | `string?` | Exact order number |
| `CustomerName` | `string?` | Partial customer name |
| `CustomerTaxId` | `string?` | CPF or CNPJ |
| `StartDate` | `DateOnly?` | Order date range start |
| `EndDate` | `DateOnly?` | Order date range end |
| `UpdatedSince` | `DateTime?` | Last update timestamp |
| `Status` | `string?` | Tiny status label (e.g. `"Aberto"`) |
| `EcommerceNumber` | `string?` | Marketplace order number |
| `SellerId` | `long?` | Seller ID |
| `SellerName` | `string?` | Partial seller name |
| `Marker` | `string?` | Marker label |
| `OccurrenceStartDate` | `DateOnly?` | Occurrence date range start |
| `OccurrenceEndDate` | `DateOnly?` | Occurrence date range end |
| `Page` | `int` | Page number, 1-based (default: 1) |
| `SortOrder` | `SortOrder?` | Ascending or Descending |

> Searching without any filter on accounts with large order volumes may trigger a server-side error from Tiny. Always provide at least one filter, such as a date range.

---

## ITinyStockService

### GetByProductIdAsync

```csharp
Task<ProductStock?> GetByProductIdAsync(
    long productId,
    CancellationToken cancellationToken = default)
```

Returns current stock position for the given product, including per-warehouse breakdown. Returns `null` if not found.

### UpdateAsync

```csharp
Task<UpsertResult> UpdateAsync(
    UpdateStockData data,
    CancellationToken cancellationToken = default)
```

Records a stock movement (entry, exit, or balance adjustment).

| Parameter | Type | Required | Description |
|---|---|---|---|
| `ProductId` | `long` | Yes | Tiny product ID |
| `Type` | `StockUpdateType` | Yes | Entry, Exit, or Balance |
| `Quantity` | `decimal` | Yes | Movement quantity |
| `Date` | `DateTime?` | No | Movement date (defaults to now) |
| `UnitPrice` | `decimal?` | No | Unit cost for this movement |
| `Notes` | `string?` | No | Free-text observation |
| `WarehouseName` | `string?` | No | Target warehouse name |

### ListUpdatesAsync

```csharp
Task<PagedResult<StockUpdateEntry>> ListUpdatesAsync(
    ListStockUpdatesRequest request,
    CancellationToken cancellationToken = default)
```

Returns products whose stock changed since the given timestamp. Requires the **"API para estoque em tempo real"** extension to be enabled on the Tiny account — the endpoint returns `404` without it.

| Parameter | Type | Required | Description |
|---|---|---|---|
| `UpdatedSince` | `DateTime` | Yes | Earliest change timestamp |
| `Page` | `int` | No | Page number, 1-based (default: 1) |

---

## Common return types

### PagedResult\<T\>

| Property | Type | Description |
|---|---|---|
| `Page` | `int` | Current page number |
| `TotalPages` | `int` | Total number of pages |
| `Items` | `IReadOnlyList<T>` | Items on this page |

### UpsertResult

| Property | Type | Description |
|---|---|---|
| `Sequence` | `int` | Position in the input batch |
| `Success` | `bool` | Whether the operation succeeded |
| `Id` | `long?` | ID of the created or updated record |
| `Errors` | `IReadOnlyList<string>` | Error messages if `Success` is false |
| `VariationIds` | `IReadOnlyList<long>` | IDs of created variations (products only) |

---

## Exceptions

### TinyApiException

Thrown when the Tiny API returns a business error (envelope `status != "OK"`).

```csharp
catch (TinyApiException ex)
{
    Console.WriteLine(ex.Message);      // Human-readable error
    Console.WriteLine(ex.ErrorCode);    // Tiny error code string
    Console.WriteLine(ex.Errors);       // List<string> with individual messages
}
```

HTTP-level failures (network errors, 4xx/5xx) surface as `HttpRequestException` from the standard .NET library.
