# Services

Services implement the `ITinyXxxService` interfaces from `Abstractions` and are the layer consumers inject and call. They delegate HTTP work to the typed clients and translate the internal wire DTOs into domain entities using the mappers.

All three services are `internal sealed` and registered as `transient` by `AddTiny`.

## TinyProductService

Implements `ITinyProductService`.

| Method | What it does |
|---|---|
| `GetByIdAsync` | Calls `TinyProductClient.GetByIdAsync`, maps `TinyProductJson → Product` via `ProductMapper.ToEntity`. Returns `null` when the client response has no product. |
| `SearchAsync` | Calls `TinyProductClient.SearchAsync`, maps each `TinyProductSummaryJson → ProductSummary` via `ProductMapper.ToSummary`. Builds `PagedResult<ProductSummary>` from pagination fields. |
| `CreateAsync` | Calls `TinyProductClient.CreateAsync`, maps each `TinyUpsertRegistroJson → UpsertResult`. |
| `UpdateAsync` | Same as `CreateAsync` but delegates to `TinyProductClient.UpdateAsync`. |

## TinyOrderService

Implements `ITinyOrderService`.

| Method | What it does |
|---|---|
| `GetByIdAsync` | Calls `TinyOrderClient.GetByIdAsync`, maps `TinyOrderJson → Order` via `OrderMapper.ToEntity`. Returns `null` when the client response has no order. |
| `SearchAsync` | Calls `TinyOrderClient.SearchAsync`, maps each `TinyOrderSummaryJson → OrderSummary` via `OrderMapper.ToSummary`. |

## TinyStockService

Implements `ITinyStockService`.

| Method | What it does |
|---|---|
| `GetByProductIdAsync` | Calls `TinyStockClient.GetByProductIdAsync`, maps `TinyProductStockJson → ProductStock` via `StockMapper.ToEntity`. |
| `UpdateAsync` | Calls `TinyStockClient.UpdateAsync`, maps the single `TinyUpdateStockRegistroJson → UpsertResult`. Returns a failed `UpsertResult` when `Registros` is null. |
| `ListUpdatesAsync` | Calls `TinyStockClient.ListUpdatesAsync`, maps each `TinyStockUpdateEntryJson → StockUpdateEntry` via `StockMapper.ToUpdateEntry`. |

## Mappers

The three mapper classes (`ProductMapper`, `OrderMapper`, `StockMapper`) contain the translation logic between internal JSON DTOs and domain entities. They are `internal static` and not part of the public API.

Key parsing helpers in `ProductMapper`, reused by all mappers:

| Helper | Behaviour |
|---|---|
| `ParseLong(string?)` | Returns `0` on null or parse failure |
| `ParseNullableLong(string?)` | Returns `null` on null, empty, zero, or parse failure |
| `ParseDecimal(string?)` | Invariant culture; returns `0m` on failure |
| `ParseNullableDecimal(string?)` | Returns `null` on whitespace or parse failure |
| `ParseDate(string?)` | Tries `dd/MM/yyyy HH:mm:ss`, then `dd/MM/yyyy`; returns `null` on failure |
| `NullIfEmpty(string?)` | Returns `null` for null or whitespace strings |
