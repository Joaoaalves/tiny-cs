using Joaoaalves.Tiny.Abstractions.DTOs.Requests.Stock;
using Joaoaalves.Tiny.Abstractions.Enums;
using Joaoaalves.Tiny.Core.Clients;
using Joaoaalves.Tiny.Core.Http;
using Joaoaalves.Tiny.Core.Tests.Mocks;
using Moq;

namespace Joaoaalves.Tiny.Core.Tests.Clients;

public class TinyStockClientTests
{
    private static (TinyStockClient Client, Mock<System.Net.Http.HttpMessageHandler> Handler) Build(string json)
    {
        var (handler, httpClient) = HttpMessageHandlerMock.Create(json);
        var options = new TinyOptions { Token = "test-token" };
        var tinyHttp = new TinyHttpClient(httpClient, options);
        return (new TinyStockClient(tinyHttp), handler);
    }

    [Fact]
    public async Task GetByProductIdAsync_ValidId_SendsCorrectUrl()
    {
        var (client, handler) = Build(TinyResponseFixtures.GetStockOk);

        await client.GetByProductIdAsync(123456789L, CancellationToken.None);

        HttpMessageHandlerMock.VerifyRequest(handler, r =>
            r.Method == System.Net.Http.HttpMethod.Post &&
            r.RequestUri!.PathAndQuery.Contains("produto.obter.estoque.php") &&
            r.RequestUri.Query.Contains("id=123456789"),
            Times.Once());
    }

    [Fact]
    public async Task GetByProductIdAsync_OkResponse_DeserializesStock()
    {
        var (client, _) = Build(TinyResponseFixtures.GetStockOk);

        var result = await client.GetByProductIdAsync(123456789L, CancellationToken.None);

        Assert.NotNull(result.Product);
        Assert.Equal("123456789", result.Product.Id);
        Assert.Equal("42", result.Product.Balance);
        Assert.Equal("3", result.Product.ReservedBalance);
        Assert.Single(result.Product.Warehouses!);
        Assert.Equal("Depósito Central", result.Product.Warehouses![0].Warehouse!.Name);
    }

    [Fact]
    public async Task UpdateAsync_StockData_CallsCorrectEndpoint()
    {
        var (client, handler) = Build(TinyResponseFixtures.UpdateStockOk);
        var data = new UpdateStockData
        {
            ProductId = 123456789L,
            Type = StockUpdateType.Entry,
            Quantity = 5m,
            Notes = "Entrada de reposição"
        };

        await client.UpdateAsync(data, CancellationToken.None);

        HttpMessageHandlerMock.VerifyRequest(handler, r =>
            r.RequestUri!.PathAndQuery.Contains("produto.atualizarestoque.php") &&
            r.RequestUri.Query.Contains("estoque="),
            Times.Once());
    }

    [Fact]
    public async Task UpdateAsync_OkResponse_DeserializesRecord()
    {
        var (client, _) = Build(TinyResponseFixtures.UpdateStockOk);
        var data = new UpdateStockData { ProductId = 123456789L, Type = StockUpdateType.Entry, Quantity = 5m };

        var result = await client.UpdateAsync(data, CancellationToken.None);

        Assert.NotNull(result.Registros);
        Assert.NotNull(result.Registros.Record);
        Assert.Equal("OK", result.Registros.Record.Status);
        Assert.Equal("123456789", result.Registros.Record.Id);
    }

    [Fact]
    public async Task ListUpdatesAsync_Request_IncludesDateAndPageParams()
    {
        var (client, handler) = Build(TinyResponseFixtures.ListStockUpdatesOk);
        var request = new ListStockUpdatesRequest
        {
            UpdatedSince = new DateTime(2024, 4, 6, 0, 0, 0),
            Page = 1
        };

        await client.ListUpdatesAsync(request, CancellationToken.None);

        HttpMessageHandlerMock.VerifyRequest(handler, r =>
            r.RequestUri!.Query.Contains("dataAlteracao=06%2F04%2F2024") &&
            r.RequestUri.Query.Contains("pagina=1"),
            Times.Once());
    }

    [Fact]
    public async Task ListUpdatesAsync_OkResponse_DeserializesProducts()
    {
        var (client, _) = Build(TinyResponseFixtures.ListStockUpdatesOk);
        var request = new ListStockUpdatesRequest { UpdatedSince = DateTime.Today, Page = 1 };

        var result = await client.ListUpdatesAsync(request, CancellationToken.None);

        Assert.Equal("1", result.Page);
        Assert.Equal("1", result.TotalPages);
        Assert.Single(result.Products!);
        Assert.Equal("123456789", result.Products![0].Product!.Id);
    }

    [Fact]
    public async Task GetByProductIdAsync_ApiError_ThrowsTinyApiException()
    {
        var (client, _) = Build(TinyResponseFixtures.ApiError);

        await Assert.ThrowsAsync<TinyApiException>(
            () => client.GetByProductIdAsync(1L, CancellationToken.None));
    }
}
