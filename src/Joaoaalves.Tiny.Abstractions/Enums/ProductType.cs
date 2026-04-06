namespace Joaoaalves.Tiny.Abstractions.Enums;

/// <summary>
/// Indicates whether the record represents a physical product or a service.
/// Maps to the <c>tipo</c> field in the API.
/// </summary>
public enum ProductType
{
    /// <summary>A physical or tangible product. API value: "P".</summary>
    Product,

    /// <summary>A service offered. API value: "S".</summary>
    Service
}
