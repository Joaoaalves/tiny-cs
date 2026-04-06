namespace Joaoaalves.Tiny.Abstractions.Entities.Common;

/// <summary>
/// Wraps a page of results returned by a Tiny list endpoint.
/// </summary>
/// <typeparam name="T">The type of each item in the result set.</typeparam>
public sealed class PagedResult<T>
{
    /// <summary>The current page number (1-based).</summary>
    public int Page { get; init; }

    /// <summary>The total number of pages available for the query.</summary>
    public int TotalPages { get; init; }

    /// <summary>The items on the current page.</summary>
    public IReadOnlyList<T> Items { get; init; } = [];
}
