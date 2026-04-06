# Joaoaalves.Tiny

C# client library for the **Tiny ERP API V2** (Brazilian ERP).

> This library targets **API V2**, which uses token-based authentication.  
> It does not support API V3 (OAuth).

## Packages

| Package | Description |
|---|---|
| `Joaoaalves.Tiny` | Core HTTP client and service implementations |
| `Joaoaalves.Tiny.Abstractions` | Entities, interfaces, enums and DTOs |

## Quick Start

```csharp
using Microsoft.Extensions.DependencyInjection;
using Joaoaalves.Tiny.Core.Extensions;

var services = new ServiceCollection();
services.AddTiny("your-api-token");

var provider = services.BuildServiceProvider();
var productService = provider.GetRequiredService<ITinyProductService>();
```

## Navigation

- [Getting Started](getting-started.md)
- [Architecture](architecture.md)
- [Abstractions](abstractions/README.md)
- [Core](core/README.md)
- [API Reference](api-reference.md)
