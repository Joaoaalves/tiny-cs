using Joaoaalves.Tiny.Abstractions.DTOs.Requests.Orders;
using Joaoaalves.Tiny.Abstractions.Enums;
using Joaoaalves.Tiny.Core.Clients;
using Joaoaalves.Tiny.Core.Http;
using Joaoaalves.Tiny.Core.Tests.Mocks;
using Moq;

namespace Joaoaalves.Tiny.Core.Tests.Clients;

public class TinyOrderClientTests
{
    private static (TinyOrderClient Client, Mock<System.Net.Http.HttpMessageHandler> Handler) Build(string json)
    {
        var (handler, httpClient) = HttpMessageHandlerMock.Create(json);
        var options = new TinyOptions { Token = "test-token" };
        var tinyHttp = new TinyHttpClient(httpClient, options);
        return (new TinyOrderClient(tinyHttp), handler);
    }

    [Fact]
    public async Task GetByIdAsync_ValidId_SendsCorrectUrl()
    {
        var (client, handler) = Build(TinyResponseFixtures.GetOrderOk);

        await client.GetByIdAsync(55500L, CancellationToken.None);

        HttpMessageHandlerMock.VerifyRequest(handler, r =>
            r.Method == System.Net.Http.HttpMethod.Post &&
            r.RequestUri!.PathAndQuery.Contains("pedido.obter.php") &&
            r.RequestUri.Query.Contains("token=test-token") &&
            r.RequestUri.Query.Contains("id=55500"),
            Times.Once());
    }

    [Fact]
    public async Task GetByIdAsync_OkResponse_DeserializesOrder()
    {
        var (client, _) = Build(TinyResponseFixtures.GetOrderOk);

        var result = await client.GetByIdAsync(55500L, CancellationToken.None);

        Assert.NotNull(result.Order);
        Assert.Equal("55500", result.Order.Id);
        Assert.Equal("1001", result.Order.Number);
        Assert.Equal("Aberto", result.Order.Status);
        Assert.NotNull(result.Order.Customer);
        Assert.Equal("João da Silva", result.Order.Customer.Name);
        Assert.Single(result.Order.Items!);
    }

    [Fact]
    public async Task SearchAsync_WithNumber_IncludesNumeroParam()
    {
        var (client, handler) = Build(TinyResponseFixtures.SearchOrdersOk);
        var request = new SearchOrdersRequest { Number = "1001", Page = 1 };

        await client.SearchAsync(request, CancellationToken.None);

        HttpMessageHandlerMock.VerifyRequest(handler, r =>
            r.RequestUri!.Query.Contains("numero=1001"),
            Times.Once());
    }

    [Fact]
    public async Task SearchAsync_WithDateRange_IncludesDateParams()
    {
        var (client, handler) = Build(TinyResponseFixtures.SearchOrdersOk);
        var request = new SearchOrdersRequest
        {
            StartDate = new DateOnly(2024, 4, 1),
            EndDate = new DateOnly(2024, 4, 30),
            Page = 1
        };

        await client.SearchAsync(request, CancellationToken.None);

        HttpMessageHandlerMock.VerifyRequest(handler, r =>
            r.RequestUri!.Query.Contains("dataInicial=01%2F04%2F2024") &&
            r.RequestUri.Query.Contains("dataFinal=30%2F04%2F2024"),
            Times.Once());
    }

    [Fact]
    public async Task SearchAsync_WithDescendingSort_IncludesSortDesc()
    {
        var (client, handler) = Build(TinyResponseFixtures.SearchOrdersOk);
        var request = new SearchOrdersRequest { SortOrder = SortOrder.Descending, Page = 1 };

        await client.SearchAsync(request, CancellationToken.None);

        HttpMessageHandlerMock.VerifyRequest(handler, r =>
            r.RequestUri!.Query.Contains("sort=DESC"),
            Times.Once());
    }

    [Fact]
    public async Task SearchAsync_OkResponse_DeserializesPagedResult()
    {
        var (client, _) = Build(TinyResponseFixtures.SearchOrdersOk);

        var result = await client.SearchAsync(new SearchOrdersRequest { Page = 1 }, CancellationToken.None);

        Assert.Equal("1", result.Page);
        Assert.Equal("1", result.TotalPages);
        Assert.Single(result.Orders!);
        Assert.Equal("55500", result.Orders![0].Order!.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ApiError_ThrowsTinyApiException()
    {
        var (client, _) = Build(TinyResponseFixtures.ApiError);

        await Assert.ThrowsAsync<TinyApiException>(
            () => client.GetByIdAsync(1L, CancellationToken.None));
    }
}
