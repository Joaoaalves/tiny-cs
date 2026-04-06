namespace Joaoaalves.Tiny.Abstractions.Entities.Orders;

/// <summary>
/// A single payment instalment on an order's payment schedule.
/// </summary>
public sealed class OrderInstallment
{
    /// <summary>Number of days after the order date when this instalment is due.</summary>
    public int Days { get; init; }

    /// <summary>The due date of this instalment.</summary>
    public DateTime Date { get; init; }

    /// <summary>The amount due for this instalment.</summary>
    public decimal Value { get; init; }

    /// <summary>Optional notes specific to this instalment.</summary>
    public string? Notes { get; init; }
}
