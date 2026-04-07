# HTTP Client

## TinyHttpClient

`TinyHttpClient` is the single entry point for all HTTP calls made to the Tiny API. It is `internal` and registered by `IHttpClientFactory` via `AddHttpClient<TinyHttpClient>`.

### Responsibilities

- Injects `token` and `formato=json` into every request URL
- Builds the complete query string from caller-supplied parameters
- Sends an HTTP POST with an empty body (Tiny API V2 convention)
- Reads and deserialises the `{ "retorno": { ... } }` response envelope
- Throws `TinyApiException` when the envelope `status` is not `"OK"`

### API convention

Every Tiny API V2 call is an HTTP POST. Parameters — including write data like product JSON — are URL-encoded in the query string. The request body is always empty. This is an intentional (unusual) design of the Tiny API.

```
POST https://api.tiny.com.br/api2/produto.obter.php
     ?token=abc123
     &formato=json
     &id=123456789
```

### Response envelope

```json
{
  "retorno": {
    "status_processamento": "3",
    "status": "OK",
    "produto": { ... }
  }
}
```

When `status` is `"Erro"`, `TinyHttpClient.PostAsync` throws `TinyApiException` with the error details extracted from the envelope.

### PostAsync

```csharp
internal Task<TResponse> PostAsync<TResponse>(
    string endpoint,
    IEnumerable<KeyValuePair<string, string?>> parameters,
    CancellationToken cancellationToken)
    where TResponse : TinyApiBaseResponse
```

Called by every typed client. `endpoint` is the PHP filename (e.g. `produto.obter.php`). `parameters` are appended to the token and format parameters in the URL.

---

## TinyOptions

Configuration passed to `TinyHttpClient` via DI.

| Property | Type | Default | Description |
|---|---|---|---|
| `Token` | `string` | required | Tiny API token |
| `BaseAddress` | `Uri` | `https://api.tiny.com.br/api2/` | Base URL |

---

## TinyApiException

Thrown when the Tiny API returns a business error.

```csharp
public sealed class TinyApiException : Exception
{
    public string? ErrorCode { get; }
    public IReadOnlyList<string> Errors { get; }
}
```

`Message` contains the human-readable summary. `Errors` contains the individual error strings returned by the API. `ErrorCode` contains the Tiny `codigo_erro` field.

---

## FlexibleStringConverter

A `JsonConverter<string?>` registered on the response deserialisation options. The Tiny API V2 returns certain numeric fields (such as `pagina` and `numero_paginas`) inconsistently — sometimes as JSON strings, sometimes as JSON numbers — depending on the endpoint and account configuration.

`FlexibleStringConverter` accepts any scalar JSON token and returns it as a `string?`, so all `string?` DTO fields are automatically tolerant of this inconsistency. Callers never need to handle the ambiguity.
