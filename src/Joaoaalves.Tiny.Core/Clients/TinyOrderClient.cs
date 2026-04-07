using Joaoaalves.Tiny.Abstractions.DTOs.Requests.Orders;
using Joaoaalves.Tiny.Abstractions.Enums;
using Joaoaalves.Tiny.Core.DTOs.Orders;
using Joaoaalves.Tiny.Core.Http;

namespace Joaoaalves.Tiny.Core.Clients;

/// <summary>
/// Typed client for the Tiny order endpoints.
/// Handles URL parameter construction for search and retrieval.
/// </summary>
internal sealed class TinyOrderClient : ITinyOrderClient
{
    private readonly TinyHttpClient _http;

    public TinyOrderClient(TinyHttpClient http) => _http = http;

    /// <summary>Calls <c>pedido.obter.php</c> with the given order ID.</summary>
    public Task<TinyGetOrderResponse> GetByIdAsync(long id, CancellationToken ct)
        => _http.PostAsync<TinyGetOrderResponse>(
            "pedido.obter.php",
            [new("id", id.ToString())],
            ct);

    /// <summary>Calls <c>pedidos.pesquisa.php</c> with the given search filters.</summary>
    public Task<TinySearchOrdersResponse> SearchAsync(SearchOrdersRequest request, CancellationToken ct)
    {
        var parameters = new List<KeyValuePair<string, string?>>();

        if (!string.IsNullOrEmpty(request.Number))
            parameters.Add(new("numero", request.Number));

        if (!string.IsNullOrEmpty(request.CustomerName))
            parameters.Add(new("cliente", request.CustomerName));

        if (!string.IsNullOrEmpty(request.CustomerTaxId))
            parameters.Add(new("cpf_cnpj", request.CustomerTaxId));

        if (request.StartDate.HasValue)
            parameters.Add(new("dataInicial", request.StartDate.Value.ToString("dd/MM/yyyy")));

        if (request.EndDate.HasValue)
            parameters.Add(new("dataFinal", request.EndDate.Value.ToString("dd/MM/yyyy")));

        if (request.UpdatedSince.HasValue)
            parameters.Add(new("dataAtualizacao", request.UpdatedSince.Value.ToString("dd/MM/yyyy HH:mm:ss")));

        if (!string.IsNullOrEmpty(request.Status))
            parameters.Add(new("situacao", request.Status));

        if (!string.IsNullOrEmpty(request.EcommerceNumber))
            parameters.Add(new("numeroEcommerce", request.EcommerceNumber));

        if (request.SellerId.HasValue)
            parameters.Add(new("idVendedor", request.SellerId.Value.ToString()));

        if (!string.IsNullOrEmpty(request.SellerName))
            parameters.Add(new("nomeVendedor", request.SellerName));

        if (!string.IsNullOrEmpty(request.Marker))
            parameters.Add(new("marcador", request.Marker));

        if (request.OccurrenceStartDate.HasValue)
            parameters.Add(new("dataInicialOcorrencia", request.OccurrenceStartDate.Value.ToString("dd/MM/yyyy")));

        if (request.OccurrenceEndDate.HasValue)
            parameters.Add(new("dataFinalOcorrencia", request.OccurrenceEndDate.Value.ToString("dd/MM/yyyy")));

        parameters.Add(new("pagina", request.Page.ToString()));

        if (request.SortOrder.HasValue)
            parameters.Add(new("sort", request.SortOrder.Value == SortOrder.Descending ? "DESC" : "ASC"));

        return _http.PostAsync<TinySearchOrdersResponse>("pedidos.pesquisa.php", parameters, ct);
    }
}
