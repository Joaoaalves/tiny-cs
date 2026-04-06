using Joaoaalves.Tiny.Abstractions.Enums;

namespace Joaoaalves.Tiny.Abstractions.Entities.Orders;

/// <summary>
/// The full representation of a sales order in Tiny, including customer details,
/// line items, payment schedule, and logistics information.
/// </summary>
public sealed class Order
{
    /// <summary>The Tiny internal ID of the order.</summary>
    public long Id { get; init; }

    /// <summary>The sequential order number assigned by Tiny.</summary>
    public string Number { get; init; } = string.Empty;

    /// <summary>The date the order was placed.</summary>
    public DateTime OrderDate { get; init; }

    /// <summary>The expected fulfillment or delivery date.</summary>
    public DateTime? ExpectedDate { get; init; }

    /// <summary>The date the order was invoiced.</summary>
    public DateTime? InvoiceDate { get; init; }

    /// <summary>The customer who placed the order.</summary>
    public OrderCustomer Customer { get; init; } = new();

    /// <summary>The line items included in this order.</summary>
    public IReadOnlyList<OrderItem> Items { get; init; } = [];

    /// <summary>The payment instalment schedule for this order.</summary>
    public IReadOnlyList<OrderInstallment> Installments { get; init; } = [];

    /// <summary>Labels applied to the order for internal classification.</summary>
    public IReadOnlyList<OrderMarker> Markers { get; init; } = [];

    /// <summary>Human-readable payment terms (e.g. "30 60 90").</summary>
    public string? PaymentCondition { get; init; }

    /// <summary>The payment method name (e.g. "crediario", "boleto").</summary>
    public string? PaymentMethod { get; init; }

    /// <summary>The payment medium used (e.g. "Dinheiro", "Cartão de Crédito").</summary>
    public string? PaymentMedium { get; init; }

    /// <summary>The name of the carrier or freight company.</summary>
    public string? CarrierName { get; init; }

    /// <summary>Who bears the freight cost for this order.</summary>
    public FreightResponsibility? FreightResponsibility { get; init; }

    /// <summary>The freight cost amount.</summary>
    public decimal FreightValue { get; init; }

    /// <summary>The total discount applied to the order.</summary>
    public decimal DiscountValue { get; init; }

    /// <summary>The sum of all line item values before freight and discounts.</summary>
    public decimal ProductsTotal { get; init; }

    /// <summary>The final total amount of the order.</summary>
    public decimal OrderTotal { get; init; }

    /// <summary>The buyer's purchase order reference number.</summary>
    public string? PurchaseOrderNumber { get; init; }

    /// <summary>The name of the warehouse that will fulfill this order.</summary>
    public string? Warehouse { get; init; }

    /// <summary>Shipping dispatch method code.</summary>
    public string? ShippingMethod { get; init; }

    /// <summary>The specific shipping service selected (e.g. "SEDEX - CONTRATO (40436)").</summary>
    public string? ShippingService { get; init; }

    /// <summary>The current workflow status of the order.</summary>
    public string Status { get; init; } = string.Empty;

    /// <summary>General notes or observations about the order.</summary>
    public string? Notes { get; init; }

    /// <summary>The Tiny ID of the seller responsible for this order.</summary>
    public long? SellerId { get; init; }

    /// <summary>The name of the seller responsible for this order.</summary>
    public string? SellerName { get; init; }

    /// <summary>The shipping carrier's tracking code.</summary>
    public string? TrackingCode { get; init; }

    /// <summary>URL to track the shipment on the carrier's website.</summary>
    public string? TrackingUrl { get; init; }

    /// <summary>The Tiny ID of the fiscal invoice (Nota Fiscal) linked to this order.</summary>
    public long? InvoiceId { get; init; }

    /// <summary>Payments processed through integrated payment gateways.</summary>
    public IReadOnlyList<OrderIntegratedPayment> IntegratedPayments { get; init; } = [];
}
