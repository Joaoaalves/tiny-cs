namespace Joaoaalves.Tiny.Abstractions.Enums;

/// <summary>
/// Represents the lifecycle status of a product in Tiny.
/// Maps to the <c>situacao</c> field in the API.
/// </summary>
public enum ProductStatus
{
    /// <summary>The product is active and available. API value: "A".</summary>
    Active,

    /// <summary>The product is inactive. API value: "I".</summary>
    Inactive,

    /// <summary>The product has been deleted. API value: "E".</summary>
    Deleted
}
