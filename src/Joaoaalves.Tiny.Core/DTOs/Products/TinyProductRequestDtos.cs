using System.Text.Json.Serialization;

namespace Joaoaalves.Tiny.Core.DTOs.Products;

internal sealed class TinyUpsertProductRequestJson
{
    [JsonPropertyName("produtos")]
    public List<TinyUpsertProductItemJson> Products { get; init; } = [];
}

internal sealed class TinyUpsertProductItemJson
{
    [JsonPropertyName("produto")]
    public TinyUpsertProductDataJson Product { get; init; } = new();
}

internal sealed class TinyUpsertProductDataJson
{
    [JsonPropertyName("sequencia")]
    public int Sequence { get; init; }

    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("codigo")]
    public string? Sku { get; init; }

    [JsonPropertyName("nome")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("unidade")]
    public string Unit { get; init; } = string.Empty;

    [JsonPropertyName("preco")]
    public decimal Price { get; init; }

    [JsonPropertyName("preco_promocional")]
    public decimal? PromotionalPrice { get; init; }

    [JsonPropertyName("ncm")]
    public string? Ncm { get; init; }

    [JsonPropertyName("origem")]
    public string? Origin { get; init; }

    [JsonPropertyName("gtin")]
    public string? Gtin { get; init; }

    [JsonPropertyName("gtin_embalagem")]
    public string? PackagingGtin { get; init; }

    [JsonPropertyName("localizacao")]
    public string? Location { get; init; }

    [JsonPropertyName("peso_liquido")]
    public decimal? NetWeight { get; init; }

    [JsonPropertyName("peso_bruto")]
    public decimal? GrossWeight { get; init; }

    [JsonPropertyName("estoque_minimo")]
    public decimal? MinimumStock { get; init; }

    [JsonPropertyName("estoque_maximo")]
    public decimal? MaximumStock { get; init; }

    [JsonPropertyName("id_fornecedor")]
    public long? SupplierId { get; init; }

    [JsonPropertyName("codigo_fornecedor")]
    public string? SupplierCode { get; init; }

    [JsonPropertyName("codigo_pelo_fornecedor")]
    public string? SupplierProductCode { get; init; }

    [JsonPropertyName("unidade_por_caixa")]
    public string? UnitsPerBox { get; init; }

    [JsonPropertyName("preco_custo")]
    public decimal? CostPrice { get; init; }

    [JsonPropertyName("situacao")]
    public string? Status { get; init; }

    [JsonPropertyName("tipo")]
    public string? Type { get; init; }

    [JsonPropertyName("classe_ipi")]
    public string? IpiClass { get; init; }

    [JsonPropertyName("valor_ipi_fixo")]
    public decimal? FixedIpiValue { get; init; }

    [JsonPropertyName("cod_lista_servicos")]
    public string? ServiceListCode { get; init; }

    [JsonPropertyName("descricao_complementar")]
    public string? AdditionalDescription { get; init; }

    [JsonPropertyName("obs")]
    public string? Notes { get; init; }

    [JsonPropertyName("garantia")]
    public string? Warranty { get; init; }

    [JsonPropertyName("cest")]
    public string? Cest { get; init; }

    [JsonPropertyName("dias_preparacao")]
    public int? PreparationDays { get; init; }

    [JsonPropertyName("marca")]
    public string? Brand { get; init; }

    [JsonPropertyName("tipo_embalagem")]
    public int? PackagingType { get; init; }

    [JsonPropertyName("altura_embalagem")]
    public decimal? PackagingHeight { get; init; }

    [JsonPropertyName("largura_embalagem")]
    public decimal? PackagingWidth { get; init; }

    [JsonPropertyName("comprimento_embalagem")]
    public decimal? PackagingLength { get; init; }

    [JsonPropertyName("diametro_embalagem")]
    public decimal? PackagingDiameter { get; init; }

    [JsonPropertyName("categoria")]
    public string? Category { get; init; }

    [JsonPropertyName("classe_produto")]
    public string? ProductClass { get; init; }
}
