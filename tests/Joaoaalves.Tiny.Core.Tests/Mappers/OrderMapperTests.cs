using Joaoaalves.Tiny.Abstractions.Enums;
using Joaoaalves.Tiny.Core.DTOs.Orders;
using Joaoaalves.Tiny.Core.Mappers;

namespace Joaoaalves.Tiny.Core.Tests.Mappers;

public class OrderMapperTests
{
    [Fact]
    public void ToEntity_FullOrder_MapsAllFields()
    {
        var json = new TinyOrderJson
        {
            Id = "55500",
            Number = "1001",
            OrderDate = "05/04/2024",
            ExpectedDate = "10/04/2024",
            Status = "Aberto",
            FreightValue = "15.00",
            DiscountValue = "5.00",
            ProductsTotal = "100.00",
            OrderTotal = "110.00",
            FreightResponsibility = "D",
            Customer = new TinyOrderCustomerJson
            {
                Name = "João da Silva",
                TaxId = "123.456.789-00",
                City = "São Paulo",
                State = "SP"
            }
        };

        var order = OrderMapper.ToEntity(json);

        Assert.Equal(55500L, order.Id);
        Assert.Equal("1001", order.Number);
        Assert.Equal(new DateTime(2024, 4, 5), order.OrderDate);
        Assert.Equal(new DateTime(2024, 4, 10), order.ExpectedDate);
        Assert.Equal("Aberto", order.Status);
        Assert.Equal(15.00m, order.FreightValue);
        Assert.Equal(5.00m, order.DiscountValue);
        Assert.Equal(100.00m, order.ProductsTotal);
        Assert.Equal(110.00m, order.OrderTotal);
        Assert.Equal(FreightResponsibility.Recipient, order.FreightResponsibility);
        Assert.Equal("João da Silva", order.Customer.Name);
        Assert.Equal("São Paulo", order.Customer.City);
    }

    [Fact]
    public void ToEntity_WithItems_MapsItems()
    {
        var json = new TinyOrderJson
        {
            OrderDate = "05/04/2024",
            Items =
            [
                new TinyOrderItemListItem
                {
                    Item = new TinyOrderItemJson
                    {
                        Sku = "SKU-A",
                        Description = "Produto A",
                        Unit = "UN",
                        Quantity = "2",
                        UnitPrice = "50.00"
                    }
                }
            ]
        };

        var order = OrderMapper.ToEntity(json);

        Assert.Single(order.Items);
        Assert.Equal("SKU-A", order.Items[0].Sku);
        Assert.Equal("Produto A", order.Items[0].Description);
        Assert.Equal(2m, order.Items[0].Quantity);
        Assert.Equal(50.00m, order.Items[0].UnitPrice);
    }

    [Theory]
    [InlineData("E", FreightResponsibility.Sender)]
    [InlineData("D", FreightResponsibility.Recipient)]
    [InlineData("T", FreightResponsibility.ThirdParty)]
    [InlineData(null, null)]
    [InlineData("X", null)]
    public void ToEntity_FreightResponsibility_MapsCorrectly(string? code, FreightResponsibility? expected)
    {
        var json = new TinyOrderJson { OrderDate = "01/01/2024", FreightResponsibility = code };
        Assert.Equal(expected, OrderMapper.ToEntity(json).FreightResponsibility);
    }

    [Fact]
    public void ToEntity_NullCustomer_ReturnsEmptyCustomer()
    {
        var json = new TinyOrderJson { OrderDate = "01/01/2024", Customer = null };
        var order = OrderMapper.ToEntity(json);
        Assert.NotNull(order.Customer);
        Assert.Equal(string.Empty, order.Customer.Name);
    }

    [Fact]
    public void ToSummary_OkJson_MapsFields()
    {
        var json = new TinyOrderSummaryJson
        {
            Id = "55500",
            Number = "1001",
            OrderDate = "05/04/2024",
            CustomerName = "João da Silva",
            Value = "110.00",
            Status = "Aberto",
            TrackingCode = "BR123456789BR"
        };

        var summary = OrderMapper.ToSummary(json);

        Assert.Equal(55500L, summary.Id);
        Assert.Equal(1001L, summary.Number);
        Assert.Equal(new DateTime(2024, 4, 5), summary.OrderDate);
        Assert.Equal("João da Silva", summary.CustomerName);
        Assert.Equal(110.00m, summary.Value);
        Assert.Equal("Aberto", summary.Status);
        Assert.Equal("BR123456789BR", summary.TrackingCode);
    }
}
