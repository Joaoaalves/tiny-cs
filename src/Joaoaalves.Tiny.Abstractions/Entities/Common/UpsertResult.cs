namespace Joaoaalves.Tiny.Abstractions.Entities.Common;

/// <summary>
/// Represents the outcome of a single record within a batch create or update operation.
/// Tiny processes multiple records in one request and returns one result per record.
/// </summary>
public sealed class UpsertResult
{
    /// <summary>
    /// The 1-based sequence number that identifies this record within the batch request.
    /// </summary>
    public int Sequence { get; init; }

    /// <summary>Whether this individual record was processed successfully.</summary>
    public bool Success { get; init; }

    /// <summary>The Tiny-assigned ID of the created or updated record, if successful.</summary>
    public long? Id { get; init; }

    /// <summary>Error messages returned for this record when <see cref="Success"/> is false.</summary>
    public IReadOnlyList<string> Errors { get; init; } = [];

    /// <summary>
    /// IDs of the variation records created alongside this product, when applicable.
    /// Only populated on successful create/update of a product with variations.
    /// </summary>
    public IReadOnlyList<long> VariationIds { get; init; } = [];
}
