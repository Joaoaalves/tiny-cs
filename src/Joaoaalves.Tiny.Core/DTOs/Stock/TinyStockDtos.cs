using System.Text.Json.Serialization;
using Joaoaalves.Tiny.Core.DTOs.Common;

namespace Joaoaalves.Tiny.Core.DTOs.Stock;

internal sealed class TinyGetProductStockResponse : TinyApiBaseResponse
{
    [JsonPropertyName("produto")]
    public TinyProductStockJson? Product { get; init; }
}

internal sealed class TinyUpdateStockResponse : TinyApiBaseResponse
{
    [JsonPropertyName("registros")]
    public TinyUpdateStockRegistrosJson? Registros { get; init; }
}

internal sealed class TinyUpdateStockRegistrosJson
{
    [JsonPropertyName("registro")]
    public TinyUpdateStockRegistroJson? Record { get; init; }
}

internal sealed class TinyUpdateStockRegistroJson
{
    [JsonPropertyName("sequencia")]
    public string? Sequence { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }

    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("saldoEstoque")]
    public string? StockBalance { get; init; }

    [JsonPropertyName("saldoReservado")]
    public string? ReservedBalance { get; init; }

    [JsonPropertyName("registroCriado")]
    public bool RecordCreated { get; init; }

    [JsonPropertyName("erros")]
    public List<TinyApiErrorListItem>? Errors { get; init; }
}

internal sealed class TinyListStockUpdatesResponse : TinyPagedBaseResponse
{
    [JsonPropertyName("produtos")]
    public List<TinyStockUpdateEntryListItem>? Products { get; init; }
}

internal sealed class TinyStockUpdateEntryListItem
{
    [JsonPropertyName("produto")]
    public TinyStockUpdateEntryJson? Product { get; init; }
}

internal sealed class TinyProductStockJson
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("nome")]
    public string? Name { get; init; }

    [JsonPropertyName("codigo")]
    public string? Sku { get; init; }

    [JsonPropertyName("unidade")]
    public string? Unit { get; init; }

    [JsonPropertyName("saldo")]
    public string? Balance { get; init; }

    [JsonPropertyName("saldoReservado")]
    public string? ReservedBalance { get; init; }

    [JsonPropertyName("depositos")]
    public List<TinyStockWarehouseListItem>? Warehouses { get; init; }
}

internal sealed class TinyStockUpdateEntryJson
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("nome")]
    public string? Name { get; init; }

    [JsonPropertyName("codigo")]
    public string? Sku { get; init; }

    [JsonPropertyName("unidade")]
    public string? Unit { get; init; }

    [JsonPropertyName("tipo_variacao")]
    public string? VariationType { get; init; }

    [JsonPropertyName("localizacao")]
    public string? Location { get; init; }

    [JsonPropertyName("data_alteracao")]
    public string? UpdatedAt { get; init; }

    [JsonPropertyName("saldo")]
    public string? Balance { get; init; }

    [JsonPropertyName("saldoReservado")]
    public string? ReservedBalance { get; init; }

    [JsonPropertyName("depositos")]
    public List<TinyStockWarehouseListItem>? Warehouses { get; init; }
}

internal sealed class TinyStockWarehouseListItem
{
    [JsonPropertyName("deposito")]
    public TinyStockWarehouseJson? Warehouse { get; init; }
}

internal sealed class TinyStockWarehouseJson
{
    [JsonPropertyName("nome")]
    public string? Name { get; init; }

    [JsonPropertyName("desconsiderar")]
    public string? Exclude { get; init; }

    [JsonPropertyName("saldo")]
    public string? Balance { get; init; }

    [JsonPropertyName("empresa")]
    public string? Company { get; init; }
}
