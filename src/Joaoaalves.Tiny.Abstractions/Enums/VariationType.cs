namespace Joaoaalves.Tiny.Abstractions.Enums;

/// <summary>
/// Describes the variation role of a product in a product family.
/// Maps to the <c>tipoVariacao</c> field in the API.
/// </summary>
public enum VariationType
{
    /// <summary>A regular product with no variation relationship. API value: "N".</summary>
    Normal,

    /// <summary>The parent product that owns a set of variations. API value: "P".</summary>
    Parent,

    /// <summary>A specific variation of a parent product. API value: "V".</summary>
    Variation
}
