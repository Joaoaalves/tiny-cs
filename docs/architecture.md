# Architecture

## Package layout

The library is split into two packages with a strict dependency direction:

```
Joaoaalves.Tiny.Abstractions   (contracts — zero dependencies)
         ↑
Joaoaalves.Tiny.Core           (implementation — depends on Abstractions only)
```

`Abstractions` never references `Core`. This allows consumers to depend solely on `Abstractions` when building adapters, mocks, or alternative implementations.

## Joaoaalves.Tiny.Abstractions

Contains all public domain types:

| Folder | Contents |
|---|---|
| `Entities/` | Read-only C# records returned by services |
| `Interfaces/` | `ITinyProductService`, `ITinyOrderService`, `ITinyStockService` |
| `DTOs/Requests/` | Input objects passed to service methods |
| `Enums/` | Typed representations of Tiny API codes |

No NuGet dependencies. Target framework: `net10.0`.

## Joaoaalves.Tiny.Core

Contains the concrete implementation:

| Folder | Contents |
|---|---|
| `Http/` | `TinyHttpClient`, `TinyOptions`, `TinyApiException`, `FlexibleStringConverter` |
| `Clients/` | `TinyProductClient`, `TinyOrderClient`, `TinyStockClient` |
| `Services/` | `TinyProductService`, `TinyOrderService`, `TinyStockService` |
| `Mappers/` | `ProductMapper`, `OrderMapper`, `StockMapper` |
| `Extensions/` | `TinyServiceCollectionExtensions.AddTiny(…)` |

Dependencies: `Microsoft.Extensions.Http`, `Microsoft.Extensions.DependencyInjection.Abstractions`.

## Request flow

```
Consumer
  → ITinyXxxService          (Abstractions interface)
    → TinyXxxService         (orchestrates and maps)
      → TinyXxxClient        (builds parameters, calls HTTP)
        → TinyHttpClient     (injects token, unwraps envelope)
          → HttpClient       (provided by IHttpClientFactory)
```

## Tiny API V2 HTTP contract

Every call is an **HTTP POST**. No request body is ever sent — all parameters travel in the URL query string, even for write operations. This is an intentional (if unusual) design of Tiny API V2.

```
POST https://api.tiny.com.br/api2/produto.obter.php
     ?token=<apiToken>
     &formato=json
     &id=123456789
```

Response envelope:

```json
{
  "retorno": {
    "status_processamento": "3",
    "status": "OK",
    "produto": { ... }
  }
}
```

When `status` is not `"OK"`, `TinyHttpClient` throws `TinyApiException` containing the error details.

## JSON quirks

Tiny API V2 returns numeric fields inconsistently — the same field may arrive as a JSON string `"1"` or a JSON number `1` depending on the endpoint and account configuration. The library handles this transparently via `FlexibleStringConverter`, which accepts any scalar JSON token into a `string?` DTO field without requiring changes to the callers.

## Testing

Unit tests mock `HttpMessageHandler` directly so the real `HttpClient` code path is exercised. Services are tested by mocking the typed client interfaces (`ITinyProductClient`, etc.). Integration tests (gated by `Category=Integration`) run against the live API using a token from the `TINY_TOKEN` environment variable, and are strictly read-only.
