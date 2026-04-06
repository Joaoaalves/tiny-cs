namespace Joaoaalves.Tiny.Abstractions.Entities.Orders;

/// <summary>
/// A label or tag applied to an order for visual classification in Tiny.
/// </summary>
public sealed class OrderMarker
{
    /// <summary>The Tiny ID of the marker.</summary>
    public long Id { get; init; }

    /// <summary>The display label of the marker.</summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>The hex colour code of the marker (e.g. "#808080").</summary>
    public string? Color { get; init; }
}
