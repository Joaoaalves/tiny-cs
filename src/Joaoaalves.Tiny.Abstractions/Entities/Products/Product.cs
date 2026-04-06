using Joaoaalves.Tiny.Abstractions.Enums;

namespace Joaoaalves.Tiny.Abstractions.Entities.Products;

/// <summary>
/// The full representation of a product in Tiny, including pricing, logistics,
/// fiscal data, and variation information.
/// </summary>
public sealed class Product
{
    /// <summary>The Tiny internal ID of the product.</summary>
    public long Id { get; init; }

    /// <summary>When the product record was first created in Tiny.</summary>
    public DateTime? CreatedAt { get; init; }

    /// <summary>The display name of the product. Max 120 characters.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>The seller's SKU code for the product. Max 30 characters.</summary>
    public string? Sku { get; init; }

    /// <summary>The unit of measure (e.g. "UN", "PC", "KG"). Max 3 characters.</summary>
    public string? Unit { get; init; }

    /// <summary>The regular sale price.</summary>
    public decimal Price { get; init; }

    /// <summary>The promotional sale price, when active.</summary>
    public decimal PromotionalPrice { get; init; }

    /// <summary>The NCM fiscal classification code. Max 10 characters.</summary>
    public string? Ncm { get; init; }

    /// <summary>
    /// The origin code of the product according to Brazilian fiscal tables.
    /// 0 = national, 1-8 = imported variants.
    /// </summary>
    public string? Origin { get; init; }

    /// <summary>The GTIN/EAN barcode of the product. Max 14 characters.</summary>
    public string? Gtin { get; init; }

    /// <summary>The GTIN/EAN barcode of the product packaging. Max 14 characters.</summary>
    public string? PackagingGtin { get; init; }

    /// <summary>Physical location of the product in the warehouse. Max 50 characters.</summary>
    public string? Location { get; init; }

    /// <summary>Net weight of the product in kilograms.</summary>
    public decimal NetWeight { get; init; }

    /// <summary>Gross weight of the product including packaging, in kilograms.</summary>
    public decimal GrossWeight { get; init; }

    /// <summary>The minimum acceptable stock level before a reorder is triggered.</summary>
    public decimal MinimumStock { get; init; }

    /// <summary>The maximum stock level to maintain for this product.</summary>
    public decimal MaximumStock { get; init; }

    /// <summary>The Tiny ID of the linked supplier.</summary>
    public long? SupplierId { get; init; }

    /// <summary>The internal code of the supplier in Tiny. Max 15 characters.</summary>
    public string? SupplierCode { get; init; }

    /// <summary>The product code used by the supplier. Max 20 characters.</summary>
    public string? SupplierProductCode { get; init; }

    /// <summary>Number of units per shipping box. Max 3 characters.</summary>
    public string? UnitsPerBox { get; init; }

    /// <summary>The direct purchase cost price.</summary>
    public decimal CostPrice { get; init; }

    /// <summary>The weighted average cost price calculated by Tiny.</summary>
    public decimal AverageCostPrice { get; init; }

    /// <summary>The current lifecycle status of the product.</summary>
    public ProductStatus Status { get; init; }

    /// <summary>Whether the record represents a product or a service.</summary>
    public ProductType Type { get; init; }

    /// <summary>IPI tax class. Applicable only to beverages and cigarettes.</summary>
    public string? IpiClass { get; init; }

    /// <summary>Fixed IPI tax value. Use only for products with specific IPI taxation.</summary>
    public decimal FixedIpiValue { get; init; }

    /// <summary>Service list code (Lista de Serviços) for service-type products.</summary>
    public string? ServiceListCode { get; init; }

    /// <summary>Additional HTML description shown in commercial proposals and sales orders.</summary>
    public string? AdditionalDescription { get; init; }

    /// <summary>General internal notes about the product.</summary>
    public string? Notes { get; init; }

    /// <summary>Warranty description. Max 20 characters.</summary>
    public string? Warranty { get; init; }

    /// <summary>CEST fiscal substitution code. Max 9 characters.</summary>
    public string? Cest { get; init; }

    /// <summary>Whether this product is standalone, a parent, or a variation.</summary>
    public VariationType VariationType { get; init; }

    /// <summary>
    /// The child variations of this product.
    /// Only populated when <see cref="VariationType"/> is <see cref="Enums.VariationType.Parent"/>.
    /// </summary>
    public IReadOnlyList<ProductVariation> Variations { get; init; } = [];

    /// <summary>
    /// The Tiny ID of the parent product.
    /// Only populated when <see cref="VariationType"/> is <see cref="Enums.VariationType.Variation"/>.
    /// </summary>
    public long? ParentProductId { get; init; }

    /// <summary>Whether the product is made to order rather than kept in stock.</summary>
    public bool MadeToOrder { get; init; }

    /// <summary>Number of calendar days required to prepare the product for dispatch.</summary>
    public int? PreparationDays { get; init; }

    /// <summary>
    /// Grid attributes for this variation (e.g. Tamanho → GG, Cor → Branco).
    /// Only populated when <see cref="VariationType"/> is <see cref="Enums.VariationType.Variation"/>.
    /// </summary>
    public IReadOnlyDictionary<string, string> Grid { get; init; } = new Dictionary<string, string>();

    /// <summary>The brand or manufacturer name of the product.</summary>
    public string? Brand { get; init; }

    /// <summary>The type of packaging used for shipping.</summary>
    public PackagingType? PackagingType { get; init; }

    /// <summary>Height of the product packaging in centimetres.</summary>
    public decimal? PackagingHeight { get; init; }

    /// <summary>Width of the product packaging in centimetres.</summary>
    public decimal? PackagingWidth { get; init; }

    /// <summary>Length of the product packaging in centimetres.</summary>
    public decimal? PackagingLength { get; init; }

    /// <summary>Diameter of the product packaging in centimetres (for cylindrical packages).</summary>
    public decimal? PackagingDiameter { get; init; }

    /// <summary>
    /// The category path of the product, using " >> " as a hierarchy separator.
    /// Example: "Bebês >> Higiene".
    /// </summary>
    public string? Category { get; init; }

    /// <summary>URLs of images and file attachments hosted on Tiny's servers.</summary>
    public IReadOnlyList<string> Attachments { get; init; } = [];

    /// <summary>URLs of external images not hosted on Tiny.</summary>
    public IReadOnlyList<string> ExternalImages { get; init; } = [];

    /// <summary>The structural classification of the product.</summary>
    public ProductClass ProductClass { get; init; }

    /// <summary>
    /// The component products and their quantities when this is a kit product.
    /// Only populated when <see cref="ProductClass"/> is <see cref="Enums.ProductClass.Kit"/>.
    /// </summary>
    public IReadOnlyList<ProductKitItem> KitItems { get; init; } = [];

    /// <summary>SEO metadata for e-commerce catalogue pages. Null when not configured.</summary>
    public ProductSeo? Seo { get; init; }
}
