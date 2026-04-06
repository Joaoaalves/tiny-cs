using System.Text.Json.Serialization;

namespace Joaoaalves.Tiny.Core.DTOs.Common;

internal sealed class TinyApiEnvelope<T> where T : TinyApiBaseResponse
{
    [JsonPropertyName("retorno")]
    public T? Retorno { get; init; }
}

internal class TinyApiBaseResponse
{
    [JsonPropertyName("status_processamento")]
    public string? ProcessingStatus { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }

    [JsonPropertyName("codigo_erro")]
    public string? ErrorCode { get; init; }

    [JsonPropertyName("erros")]
    public List<TinyApiErrorListItem>? Errors { get; init; }
}

internal sealed class TinyApiErrorListItem
{
    [JsonPropertyName("erro")]
    public string? Error { get; init; }
}

internal class TinyPagedBaseResponse : TinyApiBaseResponse
{
    [JsonPropertyName("pagina")]
    public string? Page { get; init; }

    [JsonPropertyName("numero_paginas")]
    public string? TotalPages { get; init; }
}

internal sealed class TinyUpsertResponse : TinyApiBaseResponse
{
    [JsonPropertyName("registros")]
    public List<TinyUpsertRegistroListItem>? Records { get; init; }
}

internal sealed class TinyUpsertRegistroListItem
{
    [JsonPropertyName("registro")]
    public TinyUpsertRegistroJson? Record { get; init; }
}

internal sealed class TinyUpsertRegistroJson
{
    [JsonPropertyName("sequencia")]
    public string? Sequence { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }

    [JsonPropertyName("codigo_erro")]
    public string? ErrorCode { get; init; }

    [JsonPropertyName("erros")]
    public List<TinyApiErrorListItem>? Errors { get; init; }

    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("variacoes")]
    public List<TinyVariacaoIdListItem>? Variations { get; init; }
}

internal sealed class TinyVariacaoIdListItem
{
    [JsonPropertyName("variacao")]
    public TinyVariacaoIdJson? Variation { get; init; }
}

internal sealed class TinyVariacaoIdJson
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }
}
