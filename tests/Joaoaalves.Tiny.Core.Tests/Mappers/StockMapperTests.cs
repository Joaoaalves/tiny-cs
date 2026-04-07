using Joaoaalves.Tiny.Abstractions.Enums;
using Joaoaalves.Tiny.Core.DTOs.Stock;
using Joaoaalves.Tiny.Core.Mappers;

namespace Joaoaalves.Tiny.Core.Tests.Mappers;

public class StockMapperTests
{
    [Fact]
    public void ToEntity_FullStock_MapsAllFields()
    {
        var json = new TinyProductStockJson
        {
            Id = "123456789",
            Name = "Camiseta Branca G",
            Sku = "CAM-001",
            Unit = "UN",
            Balance = "42",
            ReservedBalance = "3",
            Warehouses =
            [
                new TinyStockWarehouseListItem
                {
                    Warehouse = new TinyStockWarehouseJson
                    {
                        Name = "Depósito Central",
                        Exclude = "N",
                        Balance = "42",
                        Company = "Loja Principal"
                    }
                }
            ]
        };

        var stock = StockMapper.ToEntity(json);

        Assert.Equal(123456789L, stock.Id);
        Assert.Equal("Camiseta Branca G", stock.Name);
        Assert.Equal("CAM-001", stock.Sku);
        Assert.Equal("UN", stock.Unit);
        Assert.Equal(42m, stock.Balance);
        Assert.Equal(3m, stock.ReservedBalance);
        Assert.Single(stock.Warehouses);
        Assert.Equal("Depósito Central", stock.Warehouses[0].Name);
        Assert.False(stock.Warehouses[0].Exclude);
        Assert.Equal(42m, stock.Warehouses[0].Balance);
        Assert.Equal("Loja Principal", stock.Warehouses[0].Company);
    }

    [Fact]
    public void ToEntity_WarehouseExcludeS_SetsExcludeTrue()
    {
        var json = new TinyProductStockJson
        {
            Id = "1",
            Balance = "0",
            Warehouses =
            [
                new TinyStockWarehouseListItem
                {
                    Warehouse = new TinyStockWarehouseJson { Name = "W", Exclude = "S", Balance = "0" }
                }
            ]
        };

        var stock = StockMapper.ToEntity(json);
        Assert.True(stock.Warehouses[0].Exclude);
    }

    [Fact]
    public void ToUpdateEntry_FullEntry_MapsAllFields()
    {
        var json = new TinyStockUpdateEntryJson
        {
            Id = "123456789",
            Name = "Camiseta Branca G",
            Sku = "CAM-001",
            Unit = "UN",
            VariationType = "V",
            Location = "A1",
            UpdatedAt = "06/04/2024 14:30:00",
            Balance = "42",
            ReservedBalance = "3",
            Warehouses = []
        };

        var entry = StockMapper.ToUpdateEntry(json);

        Assert.Equal(123456789L, entry.Id);
        Assert.Equal("Camiseta Branca G", entry.Name);
        Assert.Equal(VariationType.Variation, entry.VariationType);
        Assert.Equal("A1", entry.Location);
        Assert.Equal(new DateTime(2024, 4, 6, 14, 30, 0), entry.UpdatedAt);
        Assert.Equal(42m, entry.Balance);
        Assert.Equal(3m, entry.ReservedBalance);
    }

    [Theory]
    [InlineData("P", VariationType.Parent)]
    [InlineData("V", VariationType.Variation)]
    [InlineData("", VariationType.Normal)]
    [InlineData(null, VariationType.Normal)]
    public void ToUpdateEntry_VariationTypeMapping_MapsCorrectly(string? code, VariationType expected)
    {
        var json = new TinyStockUpdateEntryJson { Balance = "0" , VariationType = code };
        Assert.Equal(expected, StockMapper.ToUpdateEntry(json).VariationType);
    }

    [Fact]
    public void ToEntity_NullReservedBalance_ReturnsNullOnEntity()
    {
        var json = new TinyProductStockJson { Id = "1", Balance = "10", ReservedBalance = null };
        var stock = StockMapper.ToEntity(json);
        Assert.Null(stock.ReservedBalance);
    }
}
