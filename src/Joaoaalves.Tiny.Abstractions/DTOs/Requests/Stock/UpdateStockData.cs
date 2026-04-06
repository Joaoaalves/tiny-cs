using Joaoaalves.Tiny.Abstractions.Enums;

namespace Joaoaalves.Tiny.Abstractions.DTOs.Requests.Stock;

/// <summary>
/// The payload for a single stock movement sent to <c>produto.atualizarestoque.php</c>.
/// All parameters are sent as a JSON string in the <c>estoque</c> query parameter.
/// </summary>
public sealed class UpdateStockData
{
    /// <summary>The Tiny ID of the product whose stock is being updated. Required.</summary>
    public long ProductId { get; init; }

    /// <summary>
    /// The type of stock movement.
    /// Defaults to <see cref="StockUpdateType.Balance"/> when not specified.
    /// </summary>
    public StockUpdateType Type { get; init; } = StockUpdateType.Balance;

    /// <summary>
    /// The date and time of the movement.
    /// Defaults to the current server time when not specified.
    /// Format expected by the API: yyyy-MM-dd HH:mm:ss.
    /// </summary>
    public DateTime? Date { get; init; }

    /// <summary>The quantity for this movement. Required.</summary>
    public decimal Quantity { get; init; }

    /// <summary>The unit cost at the time of this movement.</summary>
    public decimal? UnitPrice { get; init; }

    /// <summary>Optional notes describing the reason for this movement. Max 100 characters.</summary>
    public string? Notes { get; init; }

    /// <summary>
    /// The name of the warehouse to update.
    /// When not specified, the account's default warehouse is used.
    /// </summary>
    public string? WarehouseName { get; init; }
}
