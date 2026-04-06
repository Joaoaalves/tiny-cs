# Getting Started

## Installation

```bash
dotnet add package Joaoaalves.Tiny
```

If you only need the contracts (for your own implementations):

```bash
dotnet add package Joaoaalves.Tiny.Abstractions
```

## Configuration

Register the library with the built-in DI container by calling `AddTiny` with your API token:

```csharp
services.AddTiny("your-api-token-here");
```

You can obtain an API token from the Tiny ERP panel under **Configurações → Integrações → API**.

## Authentication

Tiny API V2 authenticates requests by appending a `token` query parameter to every call.
The library handles this automatically once you configure the token via `AddTiny`.

## First Request

```csharp
var productService = provider.GetRequiredService<ITinyProductService>();
var response = await productService.GetByIdAsync(12345);
Console.WriteLine(response.Product.Name);
```
