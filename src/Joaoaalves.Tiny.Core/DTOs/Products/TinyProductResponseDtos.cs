using System.Text.Json.Serialization;
using Joaoaalves.Tiny.Core.DTOs.Common;

namespace Joaoaalves.Tiny.Core.DTOs.Products;

internal sealed class TinyGetProductResponse : TinyApiBaseResponse
{
    [JsonPropertyName("produto")]
    public TinyProductJson? Product { get; init; }
}

internal sealed class TinySearchProductsResponse : TinyPagedBaseResponse
{
    [JsonPropertyName("produtos")]
    public List<TinyProductSummaryListItem>? Products { get; init; }
}

internal sealed class TinyProductSummaryListItem
{
    [JsonPropertyName("produto")]
    public TinyProductSummaryJson? Product { get; init; }
}

internal sealed class TinyProductSummaryJson
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("nome")]
    public string? Name { get; init; }

    [JsonPropertyName("codigo")]
    public string? Sku { get; init; }

    [JsonPropertyName("preco")]
    public string? Price { get; init; }

    [JsonPropertyName("preco_promocional")]
    public string? PromotionalPrice { get; init; }

    [JsonPropertyName("preco_custo")]
    public string? CostPrice { get; init; }

    [JsonPropertyName("preco_custo_medio")]
    public string? AverageCostPrice { get; init; }

    [JsonPropertyName("unidade")]
    public string? Unit { get; init; }

    [JsonPropertyName("gtin")]
    public string? Gtin { get; init; }

    [JsonPropertyName("tipoVariacao")]
    public string? VariationType { get; init; }

    [JsonPropertyName("localizacao")]
    public string? Location { get; init; }

    [JsonPropertyName("situacao")]
    public string? Status { get; init; }

    [JsonPropertyName("data_criacao")]
    public string? CreatedAt { get; init; }
}

internal sealed class TinyProductJson
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("data_criacao")]
    public string? CreatedAt { get; init; }

    [JsonPropertyName("nome")]
    public string? Name { get; init; }

    [JsonPropertyName("codigo")]
    public string? Sku { get; init; }

    [JsonPropertyName("unidade")]
    public string? Unit { get; init; }

    [JsonPropertyName("preco")]
    public string? Price { get; init; }

    [JsonPropertyName("preco_promocional")]
    public string? PromotionalPrice { get; init; }

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
    public string? NetWeight { get; init; }

    [JsonPropertyName("peso_bruto")]
    public string? GrossWeight { get; init; }

    [JsonPropertyName("estoque_minimo")]
    public string? MinimumStock { get; init; }

    [JsonPropertyName("estoque_maximo")]
    public string? MaximumStock { get; init; }

    [JsonPropertyName("id_fornecedor")]
    public string? SupplierId { get; init; }

    [JsonPropertyName("codigo_fornecedor")]
    public string? SupplierCode { get; init; }

    [JsonPropertyName("codigo_pelo_fornecedor")]
    public string? SupplierProductCode { get; init; }

    [JsonPropertyName("unidade_por_caixa")]
    public string? UnitsPerBox { get; init; }

    [JsonPropertyName("preco_custo")]
    public string? CostPrice { get; init; }

    [JsonPropertyName("preco_custo_medio")]
    public string? AverageCostPrice { get; init; }

    [JsonPropertyName("situacao")]
    public string? Status { get; init; }

    [JsonPropertyName("tipo")]
    public string? Type { get; init; }

    [JsonPropertyName("classe_ipi")]
    public string? IpiClass { get; init; }

    [JsonPropertyName("valor_ipi_fixo")]
    public string? FixedIpiValue { get; init; }

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

    [JsonPropertyName("tipoVariacao")]
    public string? VariationType { get; init; }

    [JsonPropertyName("variacoes")]
    public List<TinyProductVariationListItem>? Variations { get; init; }

    [JsonPropertyName("idProdutoPai")]
    public string? ParentProductId { get; init; }

    [JsonPropertyName("sob_encomenda")]
    public string? MadeToOrder { get; init; }

    [JsonPropertyName("dias_preparacao")]
    public string? PreparationDays { get; init; }

    [JsonPropertyName("grade")]
    public Dictionary<string, string>? Grid { get; init; }

    [JsonPropertyName("marca")]
    public string? Brand { get; init; }

    [JsonPropertyName("tipoEmbalagem")]
    public string? PackagingType { get; init; }

    [JsonPropertyName("alturaEmbalagem")]
    public string? PackagingHeight { get; init; }

    [JsonPropertyName("larguraEmbalagem")]
    public string? PackagingWidth { get; init; }

    [JsonPropertyName("comprimentoEmbalagem")]
    public string? PackagingLength { get; init; }

    [JsonPropertyName("diametroEmbalagem")]
    public string? PackagingDiameter { get; init; }

    [JsonPropertyName("categoria")]
    public string? Category { get; init; }

    [JsonPropertyName("anexos")]
    public List<TinyProductAttachmentJson>? Attachments { get; init; }

    [JsonPropertyName("imagens_externas")]
    public List<TinyProductExternalImageListItem>? ExternalImages { get; init; }

    [JsonPropertyName("classe_produto")]
    public string? ProductClass { get; init; }

    [JsonPropertyName("kit")]
    public List<TinyProductKitItemListItem>? KitItems { get; init; }

    [JsonPropertyName("seo_title")]
    public string? SeoTitle { get; init; }

    [JsonPropertyName("seo_keywords")]
    public string? SeoKeywords { get; init; }

    [JsonPropertyName("link_video")]
    public string? VideoUrl { get; init; }

    [JsonPropertyName("seo_description")]
    public string? SeoDescription { get; init; }

    [JsonPropertyName("slug")]
    public string? Slug { get; init; }
}

internal sealed class TinyProductVariationListItem
{
    [JsonPropertyName("variacao")]
    public TinyProductVariationJson? Variation { get; init; }
}

internal sealed class TinyProductVariationJson
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("codigo")]
    public string? Sku { get; init; }

    [JsonPropertyName("preco")]
    public string? Price { get; init; }

    [JsonPropertyName("grade")]
    public Dictionary<string, string>? Grid { get; init; }
}

internal sealed class TinyProductAttachmentJson
{
    [JsonPropertyName("anexo")]
    public string? Url { get; init; }
}

internal sealed class TinyProductExternalImageListItem
{
    [JsonPropertyName("imagem_externa")]
    public TinyProductExternalImageJson? Image { get; init; }
}

internal sealed class TinyProductExternalImageJson
{
    [JsonPropertyName("url")]
    public string? Url { get; init; }
}

internal sealed class TinyProductKitItemListItem
{
    [JsonPropertyName("item")]
    public TinyProductKitItemJson? Item { get; init; }
}

internal sealed class TinyProductKitItemJson
{
    [JsonPropertyName("id_produto")]
    public string? ProductId { get; init; }

    [JsonPropertyName("quantidade")]
    public string? Quantity { get; init; }
}
