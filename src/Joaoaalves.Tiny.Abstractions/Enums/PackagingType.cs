namespace Joaoaalves.Tiny.Abstractions.Enums;

/// <summary>
/// Describes the type of packaging used for a product.
/// Maps to the <c>tipoEmbalagem</c> field in the API.
/// </summary>
public enum PackagingType
{
    /// <summary>An envelope or flat packaging. API value: 1.</summary>
    Envelope = 1,

    /// <summary>A box or package. API value: 2.</summary>
    PackageOrBox = 2,

    /// <summary>A roll or cylindrical container. API value: 3.</summary>
    RollOrCylinder = 3
}
