# Dependency Injection

## AddTiny

`TinyServiceCollectionExtensions` provides two overloads of `AddTiny` on `IServiceCollection`.

### Basic registration

```csharp
services.AddTiny("your-api-token");
```

### With options override

```csharp
services.AddTiny("your-api-token", options =>
{
    options.BaseAddress = new Uri("https://api.tiny.com.br/api2/");
});
```

The delegate receives a `TinyOptions` instance after the token has been set. Use it to override the base address if needed (for example, to point at a test double).

## What gets registered

| Registration | Lifetime | Description |
|---|---|---|
| `TinyOptions` | Singleton | Holds the token and base address |
| `HttpClient` (via `AddHttpClient<TinyHttpClient>`) | Managed by `IHttpClientFactory` | Underlying HTTP client |
| `ITinyProductClient → TinyProductClient` | Transient | Typed client for product endpoints |
| `ITinyOrderClient → TinyOrderClient` | Transient | Typed client for order endpoints |
| `ITinyStockClient → TinyStockClient` | Transient | Typed client for stock endpoints |
| `ITinyProductService → TinyProductService` | Transient | Product service |
| `ITinyOrderService → TinyOrderService` | Transient | Order service |
| `ITinyStockService → TinyStockService` | Transient | Stock service |

## Usage without a DI container

If you are not using `IServiceCollection`, you can build the object graph manually:

```csharp
using Joaoaalves.Tiny.Core.Http;
using Joaoaalves.Tiny.Core.Clients;
using Joaoaalves.Tiny.Core.Services;

var options = new TinyOptions { Token = "your-api-token" };
var httpClient = new HttpClient { BaseAddress = options.BaseAddress };
var tinyHttp = new TinyHttpClient(httpClient, options);

var productClient  = new TinyProductClient(tinyHttp);
var productService = new TinyProductService(productClient);
```

This is useful for console applications or scenarios where full DI is not available. Note that `HttpClient` lifetime management is your responsibility in this case.

## Replacing a service

Because services are registered against their interfaces, you can replace any of them after calling `AddTiny`:

```csharp
services.AddTiny("token");
services.AddTransient<ITinyProductService, MyCustomProductService>();
```

The last `AddTransient` wins — the built-in implementation is overridden by yours.
