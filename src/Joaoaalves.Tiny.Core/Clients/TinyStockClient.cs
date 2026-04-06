using System.Text.Json;
using Joaoaalves.Tiny.Abstractions.DTOs.Requests.Stock;
using Joaoaalves.Tiny.Abstractions.Enums;
using Joaoaalves.Tiny.Core.DTOs.Stock;
using Joaoaalves.Tiny.Core.Http;

namespace Joaoaalves.Tiny.Core.Clients;

/// <summary>
/// Typed client for the Tiny stock endpoints.
/// </summary>
internal sealed class TinyStockClient
{
    private readonly TinyHttpClient _http;

    public TinyStockClient(TinyHttpClient http) => _http = http;

    /// <summary>Calls <c>produto.obter.estoque.php</c> for the given product ID.</summary>
    internal Task<TinyGetProductStockResponse> GetByProductIdAsync(long productId, CancellationToken ct)
        => _http.PostAsync<TinyGetProductStockResponse>(
            "produto.obter.estoque.php",
            [new("id", productId.ToString())],
            ct);

    /// <summary>
    /// Calls <c>produto.atualizarestoque.php</c>.
    /// The movement data is serialised to JSON and sent as the <c>estoque</c> query parameter.
    /// </summary>
    internal Task<TinyUpdateStockResponse> UpdateAsync(UpdateStockData data, CancellationToken ct)
    {
        var json = JsonSerializer.Serialize(new TinyUpdateStockDataJson
        {
            ProductId = data.ProductId,
            Type = MapStockUpdateType(data.Type),
            Date = data.Date?.ToString("yyyy-MM-dd HH:mm:ss"),
            Quantity = data.Quantity,
            UnitPrice = data.UnitPrice,
            Notes = data.Notes,
            WarehouseName = data.WarehouseName
        }, TinyHttpClient.RequestJsonOptions);

        return _http.PostAsync<TinyUpdateStockResponse>(
            "produto.atualizarestoque.php",
            [new("estoque", json)],
            ct);
    }

    /// <summary>
    /// Calls <c>produtos.atualizacoes.estoque.php</c>.
    /// Requires the "API para estoque em tempo real" extension on the Tiny account.
    /// </summary>
    internal Task<TinyListStockUpdatesResponse> ListUpdatesAsync(ListStockUpdatesRequest request, CancellationToken ct)
    {
        var parameters = new List<KeyValuePair<string, string?>>
        {
            new("dataAlteracao", request.UpdatedSince.ToString("dd/MM/yyyy HH:mm:ss")),
            new("pagina", request.Page.ToString())
        };

        return _http.PostAsync<TinyListStockUpdatesResponse>(
            "produtos.atualizacoes.estoque.php", parameters, ct);
    }

    private static string MapStockUpdateType(StockUpdateType type) => type switch
    {
        StockUpdateType.Entry => "E",
        StockUpdateType.Exit => "S",
        _ => "B"
    };
}
