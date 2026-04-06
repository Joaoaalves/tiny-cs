namespace Joaoaalves.Tiny.Abstractions.Entities.Orders;

/// <summary>
/// A payment recorded through an integrated payment gateway on an order.
/// </summary>
public sealed class OrderIntegratedPayment
{
    /// <summary>The amount charged in this payment transaction.</summary>
    public decimal Value { get; init; }

    /// <summary>Numeric code representing the payment type used by the gateway.</summary>
    public int PaymentType { get; init; }

    /// <summary>CNPJ of the payment intermediary (e.g. Mercado Pago, PagSeguro).</summary>
    public string? IntermediaryTaxId { get; init; }

    /// <summary>The authorisation code returned by the payment gateway.</summary>
    public string? AuthorizationCode { get; init; }

    /// <summary>Numeric code identifying the card brand used, when applicable.</summary>
    public int? BrandCode { get; init; }
}
