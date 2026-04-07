# Getting Started

## Installation

Install the main package, which includes the HTTP client and all service implementations:

```bash
dotnet add package Joaoaalves.Tiny
```

If you only need the contracts (to build your own implementations or to reference types in a domain project):

```bash
dotnet add package Joaoaalves.Tiny.Abstractions
```

## Obtaining an API token

1. Log in to your Tiny account.
2. Go to **Configurações → Integrações → API**.
3. Generate or copy your API token.

Keep the token secret — treat it like a password. Never commit it to source control.

## Registration

Register all Tiny services with the built-in Microsoft DI container:

```csharp
using Microsoft.Extensions.DependencyInjection;
using Joaoaalves.Tiny.Core.Extensions;

services.AddTiny("your-api-token");
```

Pass an optional delegate to override the base address or other options:

```csharp
services.AddTiny("your-api-token", options =>
{
    options.BaseAddress = new Uri("https://api.tiny.com.br/api2/");
});
```

## Products

### Retrieve a product by ID

```csharp
var service = provider.GetRequiredService<ITinyProductService>();

var product = await service.GetByIdAsync(123456789);
if (product is not null)
{
    Console.WriteLine($"{product.Name} — R$ {product.Price:F2}");
}
```

### Search products

```csharp
using Joaoaalves.Tiny.Abstractions.DTOs.Requests.Products;
using Joaoaalves.Tiny.Abstractions.Enums;

var result = await service.SearchAsync(new SearchProductsRequest
{
    Query  = "camiseta",
    Status = ProductStatus.Active,
    Page   = 1
});

Console.WriteLine($"Page {result.Page} of {result.TotalPages}");
foreach (var p in result.Items)
    Console.WriteLine($"  [{p.Sku}] {p.Name} — R$ {p.Price:F2}");
```

### Create products

```csharp
var results = await service.CreateAsync(
[
    new UpsertProductData
    {
        Sequence = 1,
        Name     = "Camiseta Branca G",
        Sku      = "CAM-001-G",
        Unit     = "UN",
        Price    = 59.90m,
        Status   = ProductStatus.Active,
        Type     = ProductType.Product
    }
]);

foreach (var r in results)
{
    if (r.Success)
        Console.WriteLine($"Created ID {r.Id}");
    else
        Console.WriteLine($"Failed: {string.Join(", ", r.Errors)}");
}
```

### Update products

```csharp
var results = await service.UpdateAsync(
[
    new UpsertProductData { Id = 123456789, Price = 54.90m }
]);
```

## Orders

### Retrieve an order by ID

```csharp
var orders = provider.GetRequiredService<ITinyOrderService>();

var order = await orders.GetByIdAsync(55500);
if (order is not null)
{
    Console.WriteLine($"Order #{order.Number} — {order.Status}");
    Console.WriteLine($"Customer: {order.Customer.Name}");
    Console.WriteLine($"Total: R$ {order.OrderTotal:F2}");
}
```

### Search orders

```csharp
using Joaoaalves.Tiny.Abstractions.DTOs.Requests.Orders;

var result = await orders.SearchAsync(new SearchOrdersRequest
{
    StartDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-30)),
    Status    = "Aberto",
    Page      = 1
});

foreach (var o in result.Items)
    Console.WriteLine($"#{o.Number} {o.CustomerName} — R$ {o.Value:F2}");
```

## Stock

### Retrieve stock for a product

```csharp
var stock = provider.GetRequiredService<ITinyStockService>();

var position = await stock.GetByProductIdAsync(123456789);
if (position is not null)
{
    Console.WriteLine($"Balance: {position.Balance} | Reserved: {position.ReservedBalance}");
    foreach (var w in position.Warehouses)
        Console.WriteLine($"  {w.Name}: {w.Balance}");
}
```

### Record a stock movement

```csharp
using Joaoaalves.Tiny.Abstractions.DTOs.Requests.Stock;
using Joaoaalves.Tiny.Abstractions.Enums;

var result = await stock.UpdateAsync(new UpdateStockData
{
    ProductId     = 123456789,
    Type          = StockUpdateType.Entry,
    Quantity      = 50m,
    Notes         = "Reposição de fornecedor",
    WarehouseName = "Depósito Central"
});

Console.WriteLine(result.Success ? "Updated" : string.Join(", ", result.Errors));
```

## Error handling

All methods throw `TinyApiException` when the Tiny API returns a business error (status `"Erro"`):

```csharp
using Joaoaalves.Tiny.Core.Http;

try
{
    var product = await service.GetByIdAsync(999999999);
}
catch (TinyApiException ex)
{
    Console.WriteLine($"Tiny API error: {ex.Message}");
}
```

HTTP-level failures (timeouts, 5xx) throw the standard `HttpRequestException`.
