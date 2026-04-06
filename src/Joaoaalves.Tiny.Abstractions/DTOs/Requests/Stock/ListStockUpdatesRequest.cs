namespace Joaoaalves.Tiny.Abstractions.DTOs.Requests.Stock;

/// <summary>
/// Parameters for the stock updates queue endpoint (<c>produtos.atualizacoes.estoque.php</c>).
/// Requires the "API para estoque em tempo real" extension to be active on the Tiny account.
/// Records retrieved are removed from the queue and marked as processed.
/// </summary>
public sealed class ListStockUpdatesRequest
{
    /// <summary>
    /// Only return products whose stock was updated on or after this timestamp.
    /// The API accepts either a date ("dd/MM/yyyy") or a date and time
    /// ("dd/MM/yyyy HH:mm:ss"). Required.
    /// </summary>
    public DateTime UpdatedSince { get; init; }

    /// <summary>
    /// Page number to retrieve (1-based). Defaults to 1.
    /// Each page contains up to 100 records.
    /// </summary>
    public int Page { get; init; } = 1;
}
