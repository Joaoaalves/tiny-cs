using System.Globalization;
using Joaoaalves.Tiny.Abstractions.Entities.Products;
using Joaoaalves.Tiny.Abstractions.Enums;
using Joaoaalves.Tiny.Core.DTOs.Products;

namespace Joaoaalves.Tiny.Core.Mappers;

internal static class ProductMapper
{
    internal static Product ToEntity(TinyProductJson j) => new()
    {
        Id = ParseLong(j.Id),
        CreatedAt = ParseDate(j.CreatedAt),
        Name = j.Name ?? string.Empty,
        Sku = NullIfEmpty(j.Sku),
        Unit = NullIfEmpty(j.Unit),
        Price = ParseDecimal(j.Price),
        PromotionalPrice = ParseDecimal(j.PromotionalPrice),
        Ncm = NullIfEmpty(j.Ncm),
        Origin = NullIfEmpty(j.Origin),
        Gtin = NullIfEmpty(j.Gtin),
        PackagingGtin = NullIfEmpty(j.PackagingGtin),
        Location = NullIfEmpty(j.Location),
        NetWeight = ParseDecimal(j.NetWeight),
        GrossWeight = ParseDecimal(j.GrossWeight),
        MinimumStock = ParseDecimal(j.MinimumStock),
        MaximumStock = ParseDecimal(j.MaximumStock),
        SupplierId = ParseNullableLong(j.SupplierId),
        SupplierCode = NullIfEmpty(j.SupplierCode),
        SupplierProductCode = NullIfEmpty(j.SupplierProductCode),
        UnitsPerBox = NullIfEmpty(j.UnitsPerBox),
        CostPrice = ParseDecimal(j.CostPrice),
        AverageCostPrice = ParseDecimal(j.AverageCostPrice),
        Status = MapStatus(j.Status),
        Type = MapType(j.Type),
        IpiClass = NullIfEmpty(j.IpiClass),
        FixedIpiValue = ParseDecimal(j.FixedIpiValue),
        ServiceListCode = NullIfEmpty(j.ServiceListCode),
        AdditionalDescription = NullIfEmpty(j.AdditionalDescription),
        Notes = NullIfEmpty(j.Notes),
        Warranty = NullIfEmpty(j.Warranty),
        Cest = NullIfEmpty(j.Cest),
        VariationType = MapVariationType(j.VariationType),
        Variations = j.Variations?
            .Where(v => v.Variation is not null)
            .Select(v => ToVariation(v.Variation!))
            .ToList() ?? [],
        ParentProductId = ParseNullableLong(j.ParentProductId),
        MadeToOrder = string.Equals(j.MadeToOrder, "S", StringComparison.OrdinalIgnoreCase),
        PreparationDays = ParseNullableInt(j.PreparationDays),
        Grid = j.Grid ?? new Dictionary<string, string>(),
        Brand = NullIfEmpty(j.Brand),
        PackagingType = MapPackagingType(j.PackagingType),
        PackagingHeight = ParseNullableDecimal(j.PackagingHeight),
        PackagingWidth = ParseNullableDecimal(j.PackagingWidth),
        PackagingLength = ParseNullableDecimal(j.PackagingLength),
        PackagingDiameter = ParseNullableDecimal(j.PackagingDiameter),
        Category = NullIfEmpty(j.Category),
        Attachments = j.Attachments?
            .Select(a => a.Url)
            .Where(u => !string.IsNullOrEmpty(u))
            .Select(u => u!)
            .ToList() ?? [],
        ExternalImages = j.ExternalImages?
            .Where(e => e.Image is not null)
            .Select(e => e.Image!.Url)
            .Where(u => !string.IsNullOrEmpty(u))
            .Select(u => u!)
            .ToList() ?? [],
        ProductClass = MapProductClass(j.ProductClass),
        KitItems = j.KitItems?
            .Where(k => k.Item is not null)
            .Select(k => new ProductKitItem
            {
                ProductId = ParseLong(k.Item!.ProductId),
                Quantity = ParseDecimal(k.Item.Quantity)
            })
            .ToList() ?? [],
        Seo = BuildSeo(j)
    };

    internal static ProductSummary ToSummary(TinyProductSummaryJson j) => new()
    {
        Id = ParseLong(j.Id),
        Name = j.Name ?? string.Empty,
        Sku = NullIfEmpty(j.Sku),
        Price = ParseDecimal(j.Price),
        PromotionalPrice = ParseDecimal(j.PromotionalPrice),
        CostPrice = ParseNullableDecimal(j.CostPrice),
        AverageCostPrice = ParseNullableDecimal(j.AverageCostPrice),
        Unit = NullIfEmpty(j.Unit),
        Gtin = NullIfEmpty(j.Gtin),
        VariationType = MapVariationType(j.VariationType),
        Location = NullIfEmpty(j.Location),
        Status = string.IsNullOrEmpty(j.Status) ? null : MapStatus(j.Status),
        CreatedAt = ParseDate(j.CreatedAt)
    };

    private static ProductVariation ToVariation(TinyProductVariationJson j) => new()
    {
        Id = ParseLong(j.Id),
        Sku = NullIfEmpty(j.Sku),
        Price = ParseDecimal(j.Price),
        Grid = j.Grid ?? new Dictionary<string, string>()
    };

    private static ProductSeo? BuildSeo(TinyProductJson j)
    {
        if (string.IsNullOrEmpty(j.SeoTitle) &&
            string.IsNullOrEmpty(j.SeoKeywords) &&
            string.IsNullOrEmpty(j.VideoUrl) &&
            string.IsNullOrEmpty(j.SeoDescription) &&
            string.IsNullOrEmpty(j.Slug))
            return null;

        return new ProductSeo
        {
            Title = NullIfEmpty(j.SeoTitle),
            Keywords = NullIfEmpty(j.SeoKeywords),
            VideoUrl = NullIfEmpty(j.VideoUrl),
            Description = NullIfEmpty(j.SeoDescription),
            Slug = NullIfEmpty(j.Slug)
        };
    }

    private static ProductStatus MapStatus(string? value) => value switch
    {
        "I" => ProductStatus.Inactive,
        "E" => ProductStatus.Deleted,
        _ => ProductStatus.Active
    };

    private static ProductType MapType(string? value) =>
        string.Equals(value, "S", StringComparison.OrdinalIgnoreCase)
            ? ProductType.Service
            : ProductType.Product;

    private static VariationType MapVariationType(string? value) => value switch
    {
        "P" => VariationType.Parent,
        "V" => VariationType.Variation,
        _ => VariationType.Normal
    };

    private static PackagingType? MapPackagingType(string? value) => value switch
    {
        "1" => PackagingType.Envelope,
        "2" => PackagingType.PackageOrBox,
        "3" => PackagingType.RollOrCylinder,
        _ => null
    };

    private static ProductClass MapProductClass(string? value) => value switch
    {
        "K" => ProductClass.Kit,
        "V" => ProductClass.WithVariations,
        "F" => ProductClass.Manufactured,
        "M" => ProductClass.RawMaterial,
        _ => ProductClass.Simple
    };

    internal static long ParseLong(string? value) =>
        long.TryParse(value, out var v) ? v : 0;

    internal static long? ParseNullableLong(string? value) =>
        long.TryParse(value, out var v) && v != 0 ? v : null;

    internal static int? ParseNullableInt(string? value) =>
        int.TryParse(value, out var v) ? v : null;

    internal static decimal ParseDecimal(string? value) =>
        decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out var v) ? v : 0m;

    internal static decimal? ParseNullableDecimal(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return null;
        return decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out var v) ? v : null;
    }

    internal static DateTime? ParseDate(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return null;
        if (DateTime.TryParseExact(value, "dd/MM/yyyy HH:mm:ss",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt)) return dt;
        if (DateTime.TryParseExact(value, "dd/MM/yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out var d)) return d;
        return null;
    }

    internal static string? NullIfEmpty(string? value) =>
        string.IsNullOrWhiteSpace(value) ? null : value;
}
