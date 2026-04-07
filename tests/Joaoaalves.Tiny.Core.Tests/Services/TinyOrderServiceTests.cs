using Joaoaalves.Tiny.Abstractions.DTOs.Requests.Orders;
using Joaoaalves.Tiny.Core.Clients;
using Joaoaalves.Tiny.Core.DTOs.Orders;
using Joaoaalves.Tiny.Core.Services;
using Moq;

namespace Joaoaalves.Tiny.Core.Tests.Services;

public class TinyOrderServiceTests
{
    private readonly Mock<ITinyOrderClient> _client = new();
    private readonly TinyOrderService _service;

    public TinyOrderServiceTests()
    {
        _service = new TinyOrderService(_client.Object);
    }

    [Fact]
    public async Task GetByIdAsync_OrderFound_ReturnsEntity()
    {
        _client.Setup(c => c.GetByIdAsync(55500L, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TinyGetOrderResponse
            {
                Status = "OK",
                Order = new TinyOrderJson
                {
                    Id = "55500",
                    Number = "1001",
                    OrderDate = "05/04/2024",
                    Status = "Aberto",
                    Customer = new TinyOrderCustomerJson { Name = "João da Silva" }
                }
            });

        var result = await _service.GetByIdAsync(55500L);

        Assert.NotNull(result);
        Assert.Equal(55500L, result.Id);
        Assert.Equal("1001", result.Number);
        Assert.Equal("Aberto", result.Status);
        Assert.Equal("João da Silva", result.Customer.Name);
    }

    [Fact]
    public async Task GetByIdAsync_NullOrder_ReturnsNull()
    {
        _client.Setup(c => c.GetByIdAsync(1L, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TinyGetOrderResponse { Status = "OK", Order = null });

        var result = await _service.GetByIdAsync(1L);

        Assert.Null(result);
    }

    [Fact]
    public async Task SearchAsync_OkResponse_ReturnsMappedPagedResult()
    {
        _client.Setup(c => c.SearchAsync(It.IsAny<SearchOrdersRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TinySearchOrdersResponse
            {
                Status = "OK",
                Page = "1",
                TotalPages = "2",
                Orders =
                [
                    new TinyOrderSummaryListItem
                    {
                        Order = new TinyOrderSummaryJson
                        {
                            Id = "55500",
                            Number = "1001",
                            OrderDate = "05/04/2024",
                            CustomerName = "João da Silva",
                            Value = "110.00",
                            Status = "Aberto"
                        }
                    }
                ]
            });

        var result = await _service.SearchAsync(new SearchOrdersRequest { Page = 1 });

        Assert.Equal(1, result.Page);
        Assert.Equal(2, result.TotalPages);
        Assert.Single(result.Items);
        Assert.Equal(55500L, result.Items[0].Id);
        Assert.Equal(110.00m, result.Items[0].Value);
    }

    [Fact]
    public async Task SearchAsync_NullOrders_ReturnsEmptyItems()
    {
        _client.Setup(c => c.SearchAsync(It.IsAny<SearchOrdersRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TinySearchOrdersResponse { Status = "OK", Page = "1", TotalPages = "1", Orders = null });

        var result = await _service.SearchAsync(new SearchOrdersRequest());

        Assert.Empty(result.Items);
    }

    [Fact]
    public async Task SearchAsync_SkipsOrdersWithNullInnerObject()
    {
        _client.Setup(c => c.SearchAsync(It.IsAny<SearchOrdersRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TinySearchOrdersResponse
            {
                Status = "OK",
                Page = "1",
                TotalPages = "1",
                Orders =
                [
                    new TinyOrderSummaryListItem { Order = null },
                    new TinyOrderSummaryListItem
                    {
                        Order = new TinyOrderSummaryJson { Id = "1", OrderDate = "01/01/2024", Status = "OK" }
                    }
                ]
            });

        var result = await _service.SearchAsync(new SearchOrdersRequest());

        Assert.Single(result.Items);
    }
}
