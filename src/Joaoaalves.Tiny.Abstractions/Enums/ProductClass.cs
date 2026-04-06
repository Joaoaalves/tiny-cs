namespace Joaoaalves.Tiny.Abstractions.Enums;

/// <summary>
/// Classifies the structural nature of a product.
/// Maps to the <c>classe_produto</c> field in the API.
/// </summary>
public enum ProductClass
{
    /// <summary>A standalone product with no composition. API value: "S".</summary>
    Simple,

    /// <summary>A kit composed of multiple products. API value: "K".</summary>
    Kit,

    /// <summary>A product that has variations (e.g. size, colour). API value: "V".</summary>
    WithVariations,

    /// <summary>A product that is manufactured internally. API value: "F".</summary>
    Manufactured,

    /// <summary>A raw material used in manufacturing. API value: "M".</summary>
    RawMaterial
}
