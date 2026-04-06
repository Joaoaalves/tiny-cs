using Joaoaalves.Tiny.Abstractions.Enums;

namespace Joaoaalves.Tiny.Abstractions.DTOs.Requests.Products;

/// <summary>
/// The product payload used in both create (<c>produto.incluir.php</c>) and
/// update (<c>produto.alterar.php</c>) requests.
/// All parameters are sent as a JSON string in the <c>produto</c> query parameter.
/// </summary>
public sealed class UpsertProductData
{
    /// <summary>
    /// 1-based sequence number that uniquely identifies this record within a batch request.
    /// Returned in the response to correlate results.
    /// </summary>
    public int Sequence { get; init; } = 1;

    /// <summary>
    /// The Tiny ID of the product to update.
    /// Required for update requests; omit when creating a new product.
    /// </summary>
    public long? Id { get; init; }

    /// <summary>The seller's SKU code. Max 30 characters.</summary>
    public string? Sku { get; init; }

    /// <summary>The display name of the product. Required. Max 120 characters.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>The unit of measure (e.g. "UN", "PC", "KG"). Required. Max 3 characters.</summary>
    public string Unit { get; init; } = string.Empty;

    /// <summary>The regular sale price. Required.</summary>
    public decimal Price { get; init; }

    /// <summary>The promotional sale price. Optional.</summary>
    public decimal? PromotionalPrice { get; init; }

    /// <summary>The NCM fiscal classification code. Max 10 characters.</summary>
    public string? Ncm { get; init; }

    /// <summary>Product origin code per Brazilian fiscal tables. Required.</summary>
    public string Origin { get; init; } = "0";

    /// <summary>GTIN/EAN barcode. Max 14 characters.</summary>
    public string? Gtin { get; init; }

    /// <summary>GTIN/EAN barcode of the packaging. Max 14 characters.</summary>
    public string? PackagingGtin { get; init; }

    /// <summary>Physical warehouse location. Max 50 characters.</summary>
    public string? Location { get; init; }

    /// <summary>Net weight in kilograms.</summary>
    public decimal? NetWeight { get; init; }

    /// <summary>Gross weight including packaging, in kilograms.</summary>
    public decimal? GrossWeight { get; init; }

    /// <summary>Minimum stock level before a reorder alert is triggered.</summary>
    public decimal? MinimumStock { get; init; }

    /// <summary>Maximum stock level to maintain.</summary>
    public decimal? MaximumStock { get; init; }

    /// <summary>Tiny ID of the linked supplier.</summary>
    public long? SupplierId { get; init; }

    /// <summary>Internal code of the supplier in Tiny. Max 15 characters.</summary>
    public string? SupplierCode { get; init; }

    /// <summary>Product code as used by the supplier. Max 20 characters.</summary>
    public string? SupplierProductCode { get; init; }

    /// <summary>Number of units per shipping box. Max 3 characters.</summary>
    public string? UnitsPerBox { get; init; }

    /// <summary>Direct purchase cost price.</summary>
    public decimal? CostPrice { get; init; }

    /// <summary>Lifecycle status of the product. Required.</summary>
    public ProductStatus Status { get; init; } = ProductStatus.Active;

    /// <summary>Whether the record is a product or a service. Required.</summary>
    public ProductType Type { get; init; } = ProductType.Product;

    /// <summary>IPI class code. Applicable only to beverages and cigarettes.</summary>
    public string? IpiClass { get; init; }

    /// <summary>Fixed IPI tax value. Use only for products with specific IPI taxation.</summary>
    public decimal? FixedIpiValue { get; init; }

    /// <summary>Service list code for service-type products.</summary>
    public string? ServiceListCode { get; init; }

    /// <summary>Additional HTML description shown on proposals and orders.</summary>
    public string? AdditionalDescription { get; init; }

    /// <summary>General internal notes.</summary>
    public string? Notes { get; init; }

    /// <summary>Warranty description. Max 20 characters.</summary>
    public string? Warranty { get; init; }

    /// <summary>CEST fiscal substitution code. Max 9 characters.</summary>
    public string? Cest { get; init; }

    /// <summary>Number of calendar days required to prepare the product for dispatch.</summary>
    public int? PreparationDays { get; init; }

    /// <summary>The brand or manufacturer name.</summary>
    public string? Brand { get; init; }

    /// <summary>Type of packaging used for shipping.</summary>
    public PackagingType? PackagingType { get; init; }

    /// <summary>Packaging height in centimetres.</summary>
    public decimal? PackagingHeight { get; init; }

    /// <summary>Packaging width in centimetres.</summary>
    public decimal? PackagingWidth { get; init; }

    /// <summary>Packaging length in centimetres.</summary>
    public decimal? PackagingLength { get; init; }

    /// <summary>Packaging diameter in centimetres (for cylindrical packages).</summary>
    public decimal? PackagingDiameter { get; init; }

    /// <summary>
    /// Category path using " >> " as a hierarchy separator.
    /// Example: "Bebês >> Higiene".
    /// </summary>
    public string? Category { get; init; }

    /// <summary>Structural classification of the product.</summary>
    public ProductClass? ProductClass { get; init; }
}
