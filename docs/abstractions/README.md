# Joaoaalves.Tiny.Abstractions

The contracts package. It has zero runtime dependencies and defines every type that consumers of the library interact with.

## Contents

| Folder | What's inside |
|---|---|
| [`Entities/`](entities.md) | Domain objects returned by services |
| [`Interfaces/`](interfaces.md) | Service contracts |
| [`DTOs/`](dtos.md) | Request objects passed to service methods |
| [`Enums/`](enums.md) | Typed enumerations for Tiny API codes |

## Design goals

- **No implementation details** — nothing in this package knows about HTTP, JSON, or Tiny's wire format
- **Immutable** — all entity properties use `init`-only accessors
- **English-only surface** — Tiny API uses Portuguese field names internally; the library translates them all

## Depending only on Abstractions

If you want to build a mock, an adapter, or a separate implementation, you can depend solely on this package:

```bash
dotnet add package Joaoaalves.Tiny.Abstractions
```

Your code then targets the interfaces and entity types without pulling in any HTTP or DI infrastructure.
