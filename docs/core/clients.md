# Typed Clients

Typed clients sit between the services and `TinyHttpClient`. Each client is responsible for mapping a service-level operation to the correct Tiny endpoint, building the URL parameters, and serialising any nested JSON payloads.

All three clients are `internal sealed` and implement their respective interface (`ITinyProductClient`, `ITinyOrderClient`, `ITinyStockClient`).

## TinyProductClient

| Method | Endpoint | Notes |
|---|---|---|
| `GetByIdAsync(id)` | `produto.obter.php` | Sends `id` param |
| `SearchAsync(request)` | `produtos.pesquisa.php` | Sends `pesquisa`, `situacao`, `pagina` |
| `CreateAsync(products)` | `produto.incluir.php` | Serialises product list to JSON, sends as `produto` param |
| `UpdateAsync(products)` | `produto.alterar.php` | Same serialisation as create |

Product create/update serialises a nested JSON structure to the `produto` query parameter:

```json
{
  "produtos": [
    { "produto": { "nome": "...", "preco": 59.90, ... } }
  ]
}
```

Status, type, and product class enums are mapped to their API codes (`"A"`, `"P"`, `"K"`, etc.) before serialisation.

## TinyOrderClient

| Method | Endpoint | Notes |
|---|---|---|
| `GetByIdAsync(id)` | `pedido.obter.php` | Sends `id` param |
| `SearchAsync(request)` | `pedidos.pesquisa.php` | Sends up to 13 optional filter params |

Dates are formatted as `dd/MM/yyyy` for date filters and `dd/MM/yyyy HH:mm:ss` for the `dataAtualizacao` filter.

## TinyStockClient

| Method | Endpoint | Notes |
|---|---|---|
| `GetByProductIdAsync(productId)` | `produto.obter.estoque.php` | Sends `id` param |
| `UpdateAsync(data)` | `produto.atualizarestoque.php` | Serialises movement to JSON, sends as `estoque` param |
| `ListUpdatesAsync(request)` | `produtos.atualizacoes.estoque.php` | Requires "API para estoque em tempo real" extension |

Stock update serialises the movement to the `estoque` query parameter:

```json
{
  "idProduto": 123456789,
  "tipo": "E",
  "quantidade": 50,
  "observacoes": "Reposição"
}
```

`ListUpdatesAsync` returns `404` when the "API para estoque em tempo real" extension is not enabled on the Tiny account.
