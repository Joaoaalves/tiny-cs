# Joaoaalves.Tiny

C# client library for the **Tiny ERP API V2** (Brazilian ERP).

> This library targets **API V2**, which uses token-based authentication (`?token=…`).
> It does not support API V3, which requires OAuth.

## Packages

| Package | Description |
|---|---|
| `Joaoaalves.Tiny` | HTTP clients and service implementations |
| `Joaoaalves.Tiny.Abstractions` | Entities, interfaces, DTOs, and enums |

## Quick start

```bash
dotnet add package Joaoaalves.Tiny
```

```csharp
using Microsoft.Extensions.DependencyInjection;
using Joaoaalves.Tiny.Core.Extensions;
using Joaoaalves.Tiny.Abstractions.Interfaces;

var services = new ServiceCollection();
services.AddTiny("your-api-token");
var provider = services.BuildServiceProvider();

var products = provider.GetRequiredService<ITinyProductService>();
var product  = await products.GetByIdAsync(12345);
Console.WriteLine(product?.Name);
```

## What's covered

| Resource | Read | Write |
|---|---|---|
| Products | `GetByIdAsync`, `SearchAsync` | `CreateAsync`, `UpdateAsync` |
| Orders | `GetByIdAsync`, `SearchAsync` | — |
| Stock | `GetByProductIdAsync`, `ListUpdatesAsync` | `UpdateAsync` |

## Navigation

- [Getting Started](getting-started.md)
- [Architecture](architecture.md)
- [Abstractions](abstractions/README.md)
- [Core](core/README.md)
- [API Reference](api-reference.md)
- [Contributing](contributing.md)
- [Changelog](changelog.md)
