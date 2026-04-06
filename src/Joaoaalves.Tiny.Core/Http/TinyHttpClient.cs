using System.Text.Json;
using Joaoaalves.Tiny.Core.DTOs.Common;

namespace Joaoaalves.Tiny.Core.Http;

/// <summary>
/// Low-level HTTP wrapper for the Tiny API V2.
/// Handles token injection, the query-string-only POST convention, envelope
/// unwrapping, and business-error mapping.
/// </summary>
internal sealed class TinyHttpClient
{
    private readonly HttpClient _http;
    private readonly string _token;

    private static readonly JsonSerializerOptions ResponseJsonOptions = new()
    {
        PropertyNameCaseInsensitive = false
    };

    internal static readonly JsonSerializerOptions RequestJsonOptions = new()
    {
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };

    /// <summary>
    /// Initialises the client. The <paramref name="http"/> instance is provided
    /// by <see cref="IHttpClientFactory"/> via the DI registration in
    /// <c>TinyServiceCollectionExtensions</c>.
    /// </summary>
    public TinyHttpClient(HttpClient http, TinyOptions options)
    {
        _http = http;
        _http.BaseAddress = options.BaseAddress;
        _token = options.Token;
    }

    /// <summary>
    /// Sends a POST to <paramref name="endpoint"/> with all parameters encoded
    /// in the query string (including <c>token</c> and <c>formato=json</c>).
    /// Deserialises the response envelope and throws <see cref="TinyApiException"/>
    /// when Tiny signals a business error.
    /// </summary>
    /// <typeparam name="TResponse">The concrete <see cref="TinyApiBaseResponse"/> subtype expected.</typeparam>
    /// <param name="endpoint">Endpoint filename, e.g. <c>produto.obter.php</c>.</param>
    /// <param name="parameters">Additional query-string parameters to include.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>The unwrapped <typeparamref name="TResponse"/> on success.</returns>
    /// <exception cref="TinyApiException">
    /// Thrown when the Tiny envelope status is not "OK" or the response body is empty.
    /// </exception>
    internal async Task<TResponse> PostAsync<TResponse>(
        string endpoint,
        IEnumerable<KeyValuePair<string, string?>> parameters,
        CancellationToken cancellationToken)
        where TResponse : TinyApiBaseResponse
    {
        var url = BuildUrl(endpoint, parameters);

        using var message = new HttpRequestMessage(HttpMethod.Post, url);
        using var httpResponse = await _http.SendAsync(message, cancellationToken);
        httpResponse.EnsureSuccessStatusCode();

        await using var stream = await httpResponse.Content.ReadAsStreamAsync(cancellationToken);

        var envelope = await JsonSerializer.DeserializeAsync<TinyApiEnvelope<TResponse>>(
            stream, ResponseJsonOptions, cancellationToken);

        var retorno = envelope?.Retorno
            ?? throw new TinyApiException("The API returned an empty or unrecognised response body.");

        if (!string.Equals(retorno.Status, "OK", StringComparison.OrdinalIgnoreCase))
            throw new TinyApiException(retorno);

        return retorno;
    }

    private string BuildUrl(string endpoint, IEnumerable<KeyValuePair<string, string?>> parameters)
    {
        var parts = new List<string>
        {
            $"token={Uri.EscapeDataString(_token)}",
            "formato=json"
        };

        foreach (var (key, value) in parameters)
        {
            if (value is not null)
                parts.Add($"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value)}");
        }

        return $"{endpoint}?{string.Join("&", parts)}";
    }
}
