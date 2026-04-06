namespace Joaoaalves.Tiny.Abstractions.Enums;

/// <summary>
/// Controls the sort direction of a paginated query.
/// Maps to the <c>sort</c> field in search requests.
/// </summary>
public enum SortOrder
{
    /// <summary>Oldest or lowest values first. API value: "ASC".</summary>
    Ascending,

    /// <summary>Newest or highest values first. API value: "DESC".</summary>
    Descending
}
