using Joaoaalves.Tiny.Abstractions.Entities.Stock;
using Joaoaalves.Tiny.Abstractions.Enums;
using Joaoaalves.Tiny.Core.DTOs.Stock;

namespace Joaoaalves.Tiny.Core.Mappers;

internal static class StockMapper
{
    internal static ProductStock ToEntity(TinyProductStockJson j) => new()
    {
        Id = ProductMapper.ParseLong(j.Id),
        Name = j.Name ?? string.Empty,
        Sku = ProductMapper.NullIfEmpty(j.Sku),
        Unit = ProductMapper.NullIfEmpty(j.Unit),
        Balance = ProductMapper.ParseDecimal(j.Balance),
        ReservedBalance = ProductMapper.ParseNullableDecimal(j.ReservedBalance),
        Warehouses = j.Warehouses?
            .Where(w => w.Warehouse is not null)
            .Select(w => ToWarehouse(w.Warehouse!))
            .ToList() ?? []
    };

    internal static StockUpdateEntry ToUpdateEntry(TinyStockUpdateEntryJson j) => new()
    {
        Id = ProductMapper.ParseLong(j.Id),
        Name = j.Name ?? string.Empty,
        Sku = ProductMapper.NullIfEmpty(j.Sku),
        Unit = ProductMapper.NullIfEmpty(j.Unit),
        VariationType = MapVariationType(j.VariationType),
        Location = ProductMapper.NullIfEmpty(j.Location),
        UpdatedAt = ProductMapper.ParseDate(j.UpdatedAt),
        Balance = ProductMapper.ParseDecimal(j.Balance),
        ReservedBalance = ProductMapper.ParseNullableDecimal(j.ReservedBalance),
        Warehouses = j.Warehouses?
            .Where(w => w.Warehouse is not null)
            .Select(w => ToWarehouse(w.Warehouse!))
            .ToList() ?? []
    };

    private static StockWarehouse ToWarehouse(TinyStockWarehouseJson j) => new()
    {
        Name = j.Name ?? string.Empty,
        Exclude = string.Equals(j.Exclude, "S", StringComparison.OrdinalIgnoreCase),
        Balance = ProductMapper.ParseDecimal(j.Balance),
        Company = ProductMapper.NullIfEmpty(j.Company)
    };

    private static VariationType MapVariationType(string? value) => value switch
    {
        "P" => VariationType.Parent,
        "V" => VariationType.Variation,
        _ => VariationType.Normal
    };
}
