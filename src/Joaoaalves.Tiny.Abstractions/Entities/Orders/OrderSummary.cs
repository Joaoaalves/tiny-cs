namespace Joaoaalves.Tiny.Abstractions.Entities.Orders;

/// <summary>
/// A lightweight projection of an order returned by the search endpoint.
/// Use <see cref="Order"/> to access the full detail set.
/// </summary>
public sealed class OrderSummary
{
    /// <summary>The Tiny internal ID of the order.</summary>
    public long Id { get; init; }

    /// <summary>The sequential order number assigned by Tiny.</summary>
    public long Number { get; init; }

    /// <summary>The order number from the originating e-commerce platform or external system.</summary>
    public string? EcommerceNumber { get; init; }

    /// <summary>The date the order was placed.</summary>
    public DateTime OrderDate { get; init; }

    /// <summary>The expected fulfillment or delivery date.</summary>
    public DateTime? ExpectedDate { get; init; }

    /// <summary>The customer's name.</summary>
    public string CustomerName { get; init; } = string.Empty;

    /// <summary>The total order value.</summary>
    public decimal Value { get; init; }

    /// <summary>The Tiny ID of the seller associated with this order.</summary>
    public long? SellerId { get; init; }

    /// <summary>The name of the seller associated with this order.</summary>
    public string? SellerName { get; init; }

    /// <summary>The current workflow status of the order (e.g. "Atendido", "Aberto").</summary>
    public string Status { get; init; } = string.Empty;

    /// <summary>The shipping carrier's tracking code, when available.</summary>
    public string? TrackingCode { get; init; }
}
