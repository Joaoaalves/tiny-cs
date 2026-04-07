using Joaoaalves.Tiny.Abstractions.Enums;
using Joaoaalves.Tiny.Core.DTOs.Products;
using Joaoaalves.Tiny.Core.Mappers;

namespace Joaoaalves.Tiny.Core.Tests.Mappers;

public class ProductMapperTests
{
    [Theory]
    [InlineData("123", 123L)]
    [InlineData("0", 0L)]
    [InlineData(null, 0L)]
    [InlineData("abc", 0L)]
    public void ParseLong_VariousInputs_ReturnsExpected(string? input, long expected)
    {
        Assert.Equal(expected, ProductMapper.ParseLong(input));
    }

    [Theory]
    [InlineData("42", 42L)]
    [InlineData("0", null)]
    [InlineData(null, null)]
    [InlineData("xyz", null)]
    public void ParseNullableLong_VariousInputs_ReturnsExpected(string? input, long? expected)
    {
        Assert.Equal(expected, ProductMapper.ParseNullableLong(input));
    }

    [Theory]
    [InlineData("10.5", 10.5)]
    [InlineData("1000.00", 1000.00)]
    [InlineData(null, 0.0)]
    [InlineData("", 0.0)]
    [InlineData("abc", 0.0)]
    public void ParseDecimal_VariousInputs_ReturnsExpected(string? input, double expected)
    {
        Assert.Equal((decimal)expected, ProductMapper.ParseDecimal(input));
    }

    [Theory]
    [InlineData("01/03/2024 10:00:00", 2024, 3, 1, 10, 0, 0)]
    [InlineData("15/01/2024", 2024, 1, 15, 0, 0, 0)]
    public void ParseDate_ValidFormats_ParsesCorrectly(string input, int year, int month, int day, int h, int m, int s)
    {
        var result = ProductMapper.ParseDate(input);
        Assert.NotNull(result);
        Assert.Equal(new DateTime(year, month, day, h, m, s), result.Value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("invalid-date")]
    public void ParseDate_InvalidInputs_ReturnsNull(string? input)
    {
        Assert.Null(ProductMapper.ParseDate(input));
    }

    [Theory]
    [InlineData("hello", "hello")]
    [InlineData(null, null)]
    [InlineData("", null)]
    [InlineData("   ", null)]
    public void NullIfEmpty_VariousInputs_ReturnsExpected(string? input, string? expected)
    {
        Assert.Equal(expected, ProductMapper.NullIfEmpty(input));
    }

    [Fact]
    public void ToEntity_FullProduct_MapsAllFields()
    {
        var json = new TinyProductJson
        {
            Id = "123456789",
            Name = "Camiseta Branca G",
            Sku = "CAM-001",
            Unit = "UN",
            Price = "59.90",
            Status = "A",
            Type = "P",
            VariationType = "",
            CreatedAt = "01/03/2024 10:00:00",
            Brand = "MinhaMarca",
            Category = "Vestuário",
            ProductClass = "S"
        };

        var entity = ProductMapper.ToEntity(json);

        Assert.Equal(123456789L, entity.Id);
        Assert.Equal("Camiseta Branca G", entity.Name);
        Assert.Equal("CAM-001", entity.Sku);
        Assert.Equal("UN", entity.Unit);
        Assert.Equal(59.90m, entity.Price);
        Assert.Equal(ProductStatus.Active, entity.Status);
        Assert.Equal(ProductType.Product, entity.Type);
        Assert.Equal(VariationType.Normal, entity.VariationType);
        Assert.Equal(new DateTime(2024, 3, 1, 10, 0, 0), entity.CreatedAt);
        Assert.Equal("MinhaMarca", entity.Brand);
    }

    [Theory]
    [InlineData("A", ProductStatus.Active)]
    [InlineData("I", ProductStatus.Inactive)]
    [InlineData("E", ProductStatus.Deleted)]
    [InlineData(null, ProductStatus.Active)]
    public void ToEntity_StatusMapping_MapsCorrectly(string? statusCode, ProductStatus expected)
    {
        var json = new TinyProductJson { Status = statusCode };
        var entity = ProductMapper.ToEntity(json);
        Assert.Equal(expected, entity.Status);
    }

    [Theory]
    [InlineData("P", ProductType.Product)]
    [InlineData("S", ProductType.Service)]
    [InlineData(null, ProductType.Product)]
    public void ToEntity_TypeMapping_MapsCorrectly(string? typeCode, ProductType expected)
    {
        var json = new TinyProductJson { Type = typeCode };
        var entity = ProductMapper.ToEntity(json);
        Assert.Equal(expected, entity.Type);
    }

    [Theory]
    [InlineData("P", VariationType.Parent)]
    [InlineData("V", VariationType.Variation)]
    [InlineData("", VariationType.Normal)]
    [InlineData(null, VariationType.Normal)]
    public void ToEntity_VariationTypeMapping_MapsCorrectly(string? code, VariationType expected)
    {
        var json = new TinyProductJson { VariationType = code };
        var entity = ProductMapper.ToEntity(json);
        Assert.Equal(expected, entity.VariationType);
    }

    [Fact]
    public void ToEntity_MadeToOrder_ParsesSFlag()
    {
        var json = new TinyProductJson { MadeToOrder = "S" };
        Assert.True(ProductMapper.ToEntity(json).MadeToOrder);

        var json2 = new TinyProductJson { MadeToOrder = "N" };
        Assert.False(ProductMapper.ToEntity(json2).MadeToOrder);
    }

    [Fact]
    public void ToEntity_NoSeoFields_ReturnsSeoNull()
    {
        var json = new TinyProductJson { Name = "Test" };
        Assert.Null(ProductMapper.ToEntity(json).Seo);
    }

    [Fact]
    public void ToEntity_WithSeoFields_BuildsSeo()
    {
        var json = new TinyProductJson { SeoTitle = "SEO Title", Slug = "produto-slug" };
        var seo = ProductMapper.ToEntity(json).Seo;
        Assert.NotNull(seo);
        Assert.Equal("SEO Title", seo.Title);
        Assert.Equal("produto-slug", seo.Slug);
    }

    [Fact]
    public void ToSummary_OkJson_MapsFields()
    {
        var json = new TinyProductSummaryJson
        {
            Id = "111",
            Name = "Produto A",
            Sku = "SKU-A",
            Price = "10.00",
            Status = "A",
            VariationType = "P",
            CreatedAt = "15/01/2024"
        };

        var summary = ProductMapper.ToSummary(json);

        Assert.Equal(111L, summary.Id);
        Assert.Equal("Produto A", summary.Name);
        Assert.Equal(10.00m, summary.Price);
        Assert.Equal(ProductStatus.Active, summary.Status);
        Assert.Equal(VariationType.Parent, summary.VariationType);
        Assert.Equal(new DateTime(2024, 1, 15), summary.CreatedAt);
    }
}
