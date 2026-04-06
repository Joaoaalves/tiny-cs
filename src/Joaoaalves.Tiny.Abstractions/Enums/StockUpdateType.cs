namespace Joaoaalves.Tiny.Abstractions.Enums;

/// <summary>
/// Defines how a stock update affects the current balance.
/// Maps to the <c>tipo</c> field in the stock update request.
/// </summary>
public enum StockUpdateType
{
    /// <summary>Adds the given quantity to the current stock. API value: "E".</summary>
    Entry,

    /// <summary>Subtracts the given quantity from the current stock. API value: "S".</summary>
    Exit,

    /// <summary>Sets the stock to exactly the given quantity. API value: "B".</summary>
    Balance
}
