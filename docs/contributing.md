# Contributing

## Git flow

| Branch | Purpose |
|---|---|
| `main` | Published releases only |
| `develop` | Integration branch |
| `feature/*` | New capabilities, cut from `develop` |
| `release/*` | Release preparation, merged into both `main` and `develop` |
| `hotfix/*` | Critical fixes against `main` |

All feature branches are cut from `develop` and merged back into `develop` via pull request.

## Commit conventions

Use the following prefixes:

| Prefix | Use for |
|---|---|
| `feat:` | New capability |
| `fix:` | Bug fix |
| `docs:` | Documentation only |
| `chore:` | Tooling, build, configuration |
| `test:` | Adding or updating tests |
| `refactor:` | Code restructure without behaviour change |

No `Co-Authored-By` trailers. Keep the subject line under 72 characters.

## Code style

- Target framework: `net10.0`
- Nullable reference types and implicit usings are enabled
- All public members in both `src` projects must have XML doc comments
- Comments must be plain prose — no decorative separators (`// ---`, `// ===`, `// ***`)
- Do not add error handling or fallbacks for scenarios that cannot happen

## Architecture rules

- `Abstractions` must never reference `Core`
- `Core` references `Abstractions` only
- Test projects reference the project under test plus `Abstractions`

## Testing

All tests use **xUnit** and **Moq**.

For HTTP client tests, mock `HttpMessageHandler` directly — do not create hand-rolled fakes for `HttpClient`:

```csharp
var mock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
mock.Protected()
    .Setup<Task<HttpResponseMessage>>("SendAsync", ...)
    .ReturnsAsync(new HttpResponseMessage { ... });

var client = new HttpClient(mock.Object);
```

Service tests mock the typed client interfaces (`ITinyProductClient`, etc.), not `TinyHttpClient`.

Integration tests must be gated with `[Trait("Category", "Integration")]` and must only call read-only endpoints. Write operations are never permitted in integration tests.

## Running tests

Unit tests only:

```bash
dotnet test --filter "Category!=Integration"
```

Integration tests (requires a live token):

```bash
$env:TINY_TOKEN="your-token"; dotnet test --filter "Category=Integration"
```
