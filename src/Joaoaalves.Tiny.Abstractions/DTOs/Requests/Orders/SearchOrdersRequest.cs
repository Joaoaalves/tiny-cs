using Joaoaalves.Tiny.Abstractions.Enums;

namespace Joaoaalves.Tiny.Abstractions.DTOs.Requests.Orders;

/// <summary>
/// Filters and pagination options for the order search endpoint
/// (<c>pedidos.pesquisa.php</c>).
/// At least one of the marked fields must be provided.
/// </summary>
public sealed class SearchOrdersRequest
{
    /// <summary>Filter by Tiny order number.</summary>
    public string? Number { get; init; }

    /// <summary>Filter by customer name or partial name/code.</summary>
    public string? CustomerName { get; init; }

    /// <summary>Filter by customer CPF or CNPJ.</summary>
    public string? CustomerTaxId { get; init; }

    /// <summary>Start of the order creation date range (inclusive). Format: dd/MM/yyyy.</summary>
    public DateOnly? StartDate { get; init; }

    /// <summary>End of the order creation date range (inclusive). Format: dd/MM/yyyy.</summary>
    public DateOnly? EndDate { get; init; }

    /// <summary>
    /// Filter orders updated on or after this timestamp.
    /// Format: dd/MM/yyyy HH:mm:ss.
    /// </summary>
    public DateTime? UpdatedSince { get; init; }

    /// <summary>
    /// Filter by order workflow status (e.g. "Aberto", "Atendido").
    /// Corresponds to the Tiny status table for orders.
    /// </summary>
    public string? Status { get; init; }

    /// <summary>Filter by the order number from the originating e-commerce platform.</summary>
    public string? EcommerceNumber { get; init; }

    /// <summary>Filter by the Tiny ID of the assigned seller.</summary>
    public long? SellerId { get; init; }

    /// <summary>Filter by the name of the assigned seller.</summary>
    public string? SellerName { get; init; }

    /// <summary>Filter by a marker label applied to orders.</summary>
    public string? Marker { get; init; }

    /// <summary>Start of the occurrence date range (inclusive). Format: dd/MM/yyyy.</summary>
    public DateOnly? OccurrenceStartDate { get; init; }

    /// <summary>End of the occurrence date range (inclusive). Format: dd/MM/yyyy.</summary>
    public DateOnly? OccurrenceEndDate { get; init; }

    /// <summary>
    /// Page number to retrieve (1-based). Defaults to 1.
    /// Each page contains up to 100 records.
    /// </summary>
    public int Page { get; init; } = 1;

    /// <summary>Sort direction for the result set.</summary>
    public SortOrder? SortOrder { get; init; }
}
