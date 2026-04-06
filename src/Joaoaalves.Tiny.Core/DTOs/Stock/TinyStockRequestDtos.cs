using System.Text.Json.Serialization;

namespace Joaoaalves.Tiny.Core.DTOs.Stock;

internal sealed class TinyUpdateStockDataJson
{
    [JsonPropertyName("idProduto")]
    public long ProductId { get; init; }

    [JsonPropertyName("tipo")]
    public string? Type { get; init; }

    [JsonPropertyName("data")]
    public string? Date { get; init; }

    [JsonPropertyName("quantidade")]
    public decimal Quantity { get; init; }

    [JsonPropertyName("precoUnitario")]
    public decimal? UnitPrice { get; init; }

    [JsonPropertyName("observacoes")]
    public string? Notes { get; init; }

    [JsonPropertyName("deposito")]
    public string? WarehouseName { get; init; }
}
