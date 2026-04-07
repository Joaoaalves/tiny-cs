# Changelog

## Unreleased

### Added

- `ITinyProductService` with `GetByIdAsync`, `SearchAsync`, `CreateAsync`, `UpdateAsync`
- `ITinyOrderService` with `GetByIdAsync`, `SearchAsync`
- `ITinyStockService` with `GetByProductIdAsync`, `UpdateAsync`, `ListUpdatesAsync`
- `TinyHttpClient` — low-level HTTP wrapper handling token injection, query-string POST convention, envelope unwrapping, and error mapping to `TinyApiException`
- `FlexibleStringConverter` — tolerates numeric JSON tokens in `string?` DTO fields to handle Tiny API V2 inconsistencies
- `AddTiny` DI extension for `IServiceCollection`
- 100 unit tests covering clients, mappers, and services
- Integration tests (read-only, gated by `Category=Integration`)
- Docsify v4 documentation site
