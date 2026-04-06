# Architecture

## Overview

The library is split into two packages that follow a clean separation of contracts and implementation.

```
Joaoaalves.Tiny.Abstractions   (contracts — no dependencies)
         ↑
Joaoaalves.Tiny.Core           (implementation — depends on Abstractions)
```

## Joaoaalves.Tiny.Abstractions

Contains all domain types that define the shape of the Tiny API:

- **Entities** — plain C# classes mapping to Tiny domain objects (Product, Order, Contact, etc.)
- **Interfaces** — service contracts (`ITinyProductService`, `ITinyOrderService`, etc.)
- **DTOs** — request and response objects for each endpoint
- **Enums** — Tiny status codes and type enumerations

This package has no dependencies. Consumers who want to build their own implementations can depend solely on Abstractions.

## Joaoaalves.Tiny.Core

Contains the concrete implementation:

- **Http/** — `TinyHttpClient` wraps `HttpClient`, injects the API token, unwraps the Tiny response envelope, and maps API errors to `TinyApiException`
- **Clients/** — one typed client per Tiny resource, mapping endpoints to method calls
- **Services/** — high-level orchestration implementing the Abstractions interfaces
- **Extensions/** — `IServiceCollection` extension for easy DI registration

## HTTP Contract

All Tiny API V2 requests are HTTP POST with `Content-Type: application/x-www-form-urlencoded`.
The `format=json` parameter is always included.
Authentication is via `?token={apiToken}` appended to the URL.

Response envelope shape:

```json
{
  "retorno": {
    "status": "OK",
    "codigo_status": 200,
    "codigo_mensagem": "..."
  }
}
```
