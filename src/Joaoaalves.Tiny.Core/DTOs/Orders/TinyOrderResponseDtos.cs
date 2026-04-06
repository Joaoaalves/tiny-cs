using System.Text.Json.Serialization;
using Joaoaalves.Tiny.Core.DTOs.Common;

namespace Joaoaalves.Tiny.Core.DTOs.Orders;

internal sealed class TinyGetOrderResponse : TinyApiBaseResponse
{
    [JsonPropertyName("pedido")]
    public TinyOrderJson? Order { get; init; }
}

internal sealed class TinySearchOrdersResponse : TinyPagedBaseResponse
{
    [JsonPropertyName("pedidos")]
    public List<TinyOrderSummaryListItem>? Orders { get; init; }
}

internal sealed class TinyOrderSummaryListItem
{
    [JsonPropertyName("pedido")]
    public TinyOrderSummaryJson? Order { get; init; }
}

internal sealed class TinyOrderSummaryJson
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("numero")]
    public string? Number { get; init; }

    [JsonPropertyName("numero_ecommerce")]
    public string? EcommerceNumber { get; init; }

    [JsonPropertyName("data_pedido")]
    public string? OrderDate { get; init; }

    [JsonPropertyName("data_prevista")]
    public string? ExpectedDate { get; init; }

    [JsonPropertyName("nome")]
    public string? CustomerName { get; init; }

    [JsonPropertyName("valor")]
    public string? Value { get; init; }

    [JsonPropertyName("id_vendedor")]
    public string? SellerId { get; init; }

    [JsonPropertyName("nome_vendedor")]
    public string? SellerName { get; init; }

    [JsonPropertyName("situacao")]
    public string? Status { get; init; }

    [JsonPropertyName("codigo_rastreamento")]
    public string? TrackingCode { get; init; }
}

internal sealed class TinyOrderJson
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("numero")]
    public string? Number { get; init; }

    [JsonPropertyName("data_pedido")]
    public string? OrderDate { get; init; }

    [JsonPropertyName("data_prevista")]
    public string? ExpectedDate { get; init; }

    [JsonPropertyName("data_faturamento")]
    public string? InvoiceDate { get; init; }

    [JsonPropertyName("cliente")]
    public TinyOrderCustomerJson? Customer { get; init; }

    [JsonPropertyName("itens")]
    public List<TinyOrderItemListItem>? Items { get; init; }

    [JsonPropertyName("parcelas")]
    public List<TinyOrderInstallmentListItem>? Installments { get; init; }

    [JsonPropertyName("marcadores")]
    public List<TinyOrderMarkerListItem>? Markers { get; init; }

    [JsonPropertyName("condicao_pagamento")]
    public string? PaymentCondition { get; init; }

    [JsonPropertyName("forma_pagamento")]
    public string? PaymentMethod { get; init; }

    [JsonPropertyName("meio_pagamento")]
    public string? PaymentMedium { get; init; }

    [JsonPropertyName("nome_transportador")]
    public string? CarrierName { get; init; }

    [JsonPropertyName("frete_por_conta")]
    public string? FreightResponsibility { get; init; }

    [JsonPropertyName("valor_frete")]
    public string? FreightValue { get; init; }

    [JsonPropertyName("valor_desconto")]
    public string? DiscountValue { get; init; }

    [JsonPropertyName("total_produtos")]
    public string? ProductsTotal { get; init; }

    [JsonPropertyName("total_pedido")]
    public string? OrderTotal { get; init; }

    [JsonPropertyName("numero_ordem_compra")]
    public string? PurchaseOrderNumber { get; init; }

    [JsonPropertyName("deposito")]
    public string? Warehouse { get; init; }

    [JsonPropertyName("forma_envio")]
    public string? ShippingMethod { get; init; }

    [JsonPropertyName("forma_frete")]
    public string? ShippingService { get; init; }

    [JsonPropertyName("situacao")]
    public string? Status { get; init; }

    [JsonPropertyName("obs")]
    public string? Notes { get; init; }

    [JsonPropertyName("id_vendedor")]
    public string? SellerId { get; init; }

    [JsonPropertyName("nome_vendedor")]
    public string? SellerName { get; init; }

    [JsonPropertyName("codigo_rastreamento")]
    public string? TrackingCode { get; init; }

    [JsonPropertyName("url_rastreamento")]
    public string? TrackingUrl { get; init; }

    [JsonPropertyName("id_nota_fiscal")]
    public string? InvoiceId { get; init; }

    [JsonPropertyName("pagamentos_integrados")]
    public List<TinyOrderIntegratedPaymentListItem>? IntegratedPayments { get; init; }
}

internal sealed class TinyOrderCustomerJson
{
    [JsonPropertyName("codigo")]
    public string? Code { get; init; }

    [JsonPropertyName("nome")]
    public string? Name { get; init; }

    [JsonPropertyName("nome_fantasia")]
    public string? TradeName { get; init; }

    [JsonPropertyName("tipo_pessoa")]
    public string? PersonType { get; init; }

    [JsonPropertyName("cpf_cnpj")]
    public string? TaxId { get; init; }

    [JsonPropertyName("ie")]
    public string? StateRegistration { get; init; }

    [JsonPropertyName("rg")]
    public string? NationalId { get; init; }

    [JsonPropertyName("endereco")]
    public string? Address { get; init; }

    [JsonPropertyName("numero")]
    public string? AddressNumber { get; init; }

    [JsonPropertyName("complemento")]
    public string? Complement { get; init; }

    [JsonPropertyName("bairro")]
    public string? Neighborhood { get; init; }

    [JsonPropertyName("cep")]
    public string? PostalCode { get; init; }

    [JsonPropertyName("cidade")]
    public string? City { get; init; }

    [JsonPropertyName("uf")]
    public string? State { get; init; }

    [JsonPropertyName("fone")]
    public string? Phone { get; init; }
}

internal sealed class TinyOrderItemListItem
{
    [JsonPropertyName("item")]
    public TinyOrderItemJson? Item { get; init; }
}

internal sealed class TinyOrderItemJson
{
    [JsonPropertyName("codigo")]
    public string? Sku { get; init; }

    [JsonPropertyName("descricao")]
    public string? Description { get; init; }

    [JsonPropertyName("unidade")]
    public string? Unit { get; init; }

    [JsonPropertyName("quantidade")]
    public string? Quantity { get; init; }

    [JsonPropertyName("valor_unitario")]
    public string? UnitPrice { get; init; }
}

internal sealed class TinyOrderInstallmentListItem
{
    [JsonPropertyName("parcela")]
    public TinyOrderInstallmentJson? Installment { get; init; }
}

internal sealed class TinyOrderInstallmentJson
{
    [JsonPropertyName("dias")]
    public string? Days { get; init; }

    [JsonPropertyName("data")]
    public string? Date { get; init; }

    [JsonPropertyName("valor")]
    public string? Value { get; init; }

    [JsonPropertyName("obs")]
    public string? Notes { get; init; }
}

internal sealed class TinyOrderMarkerListItem
{
    [JsonPropertyName("marcador")]
    public TinyOrderMarkerJson? Marker { get; init; }
}

internal sealed class TinyOrderMarkerJson
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("descricao")]
    public string? Description { get; init; }

    [JsonPropertyName("cor")]
    public string? Color { get; init; }
}

internal sealed class TinyOrderIntegratedPaymentListItem
{
    [JsonPropertyName("pagamento_integrado")]
    public TinyOrderIntegratedPaymentJson? Payment { get; init; }
}

internal sealed class TinyOrderIntegratedPaymentJson
{
    [JsonPropertyName("valor")]
    public string? Value { get; init; }

    [JsonPropertyName("tipo_pagamento")]
    public string? PaymentType { get; init; }

    [JsonPropertyName("cnpj_intermediador")]
    public string? IntermediaryTaxId { get; init; }

    [JsonPropertyName("codigo_autorizacao")]
    public string? AuthorizationCode { get; init; }

    [JsonPropertyName("codigo_bandeira")]
    public string? BrandCode { get; init; }
}
