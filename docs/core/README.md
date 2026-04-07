# Joaoaalves.Tiny.Core

The implementation package. It depends on `Joaoaalves.Tiny.Abstractions` and provides everything needed to call the Tiny API V2.

## Contents

| Folder | What's inside |
|---|---|
| [`Http/`](http-client.md) | `TinyHttpClient`, `TinyOptions`, `TinyApiException`, `FlexibleStringConverter` |
| [`Clients/`](clients.md) | `TinyProductClient`, `TinyOrderClient`, `TinyStockClient` |
| `Mappers/` | `ProductMapper`, `OrderMapper`, `StockMapper` — translate wire DTOs to domain entities |
| [`Services/`](services.md) | `TinyProductService`, `TinyOrderService`, `TinyStockService` |
| [`Extensions/`](di.md) | `TinyServiceCollectionExtensions.AddTiny(…)` |

## Dependencies

| Package | Version |
|---|---|
| `Microsoft.Extensions.Http` | 10.0.x |
| `Microsoft.Extensions.DependencyInjection.Abstractions` | 10.0.x |

## Internal visibility

The `Clients/`, `Http/`, `Mappers/`, and `Services/` types are `internal`. They are exposed to the test project via `InternalsVisibleTo("Joaoaalves.Tiny.Core.Tests")` and to Moq's dynamic proxy via `InternalsVisibleTo("DynamicProxyGenAssembly2")`.
