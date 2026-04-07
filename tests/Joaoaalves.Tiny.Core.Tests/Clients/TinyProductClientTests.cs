using Joaoaalves.Tiny.Abstractions.DTOs.Requests.Products;
using Joaoaalves.Tiny.Abstractions.Enums;
using Joaoaalves.Tiny.Core.Clients;
using Joaoaalves.Tiny.Core.Http;
using Joaoaalves.Tiny.Core.Tests.Mocks;
using Moq;

namespace Joaoaalves.Tiny.Core.Tests.Clients;

public class TinyProductClientTests
{
    private static (TinyProductClient Client, Mock<System.Net.Http.HttpMessageHandler> Handler) Build(string json)
    {
        var (handler, httpClient) = HttpMessageHandlerMock.Create(json);
        var options = new TinyOptions { Token = "test-token" };
        var tinyHttp = new TinyHttpClient(httpClient, options);
        return (new TinyProductClient(tinyHttp), handler);
    }

    [Fact]
    public async Task GetByIdAsync_ValidId_SendsCorrectUrl()
    {
        var (client, handler) = Build(TinyResponseFixtures.GetProductOk);

        await client.GetByIdAsync(123456789L, CancellationToken.None);

        HttpMessageHandlerMock.VerifyRequest(handler, r =>
            r.Method == System.Net.Http.HttpMethod.Post &&
            r.RequestUri!.PathAndQuery.Contains("produto.obter.php") &&
            r.RequestUri.Query.Contains("token=test-token") &&
            r.RequestUri.Query.Contains("id=123456789"),
            Times.Once());
    }

    [Fact]
    public async Task GetByIdAsync_OkResponse_DeserializesProduct()
    {
        var (client, _) = Build(TinyResponseFixtures.GetProductOk);

        var result = await client.GetByIdAsync(123456789L, CancellationToken.None);

        Assert.NotNull(result.Product);
        Assert.Equal("123456789", result.Product.Id);
        Assert.Equal("Camiseta Branca G", result.Product.Name);
        Assert.Equal("CAM-001", result.Product.Sku);
        Assert.Equal("59.90", result.Product.Price);
    }

    [Fact]
    public async Task SearchAsync_WithQuery_IncludesPesquisaParam()
    {
        var (client, handler) = Build(TinyResponseFixtures.SearchProductsOk);
        var request = new SearchProductsRequest { Query = "camiseta", Page = 1 };

        await client.SearchAsync(request, CancellationToken.None);

        HttpMessageHandlerMock.VerifyRequest(handler, r =>
            r.RequestUri!.Query.Contains("pesquisa=camiseta"),
            Times.Once());
    }

    [Fact]
    public async Task SearchAsync_WithStatus_IncludesSituacaoParam()
    {
        var (client, handler) = Build(TinyResponseFixtures.SearchProductsOk);
        var request = new SearchProductsRequest { Status = ProductStatus.Inactive, Page = 1 };

        await client.SearchAsync(request, CancellationToken.None);

        HttpMessageHandlerMock.VerifyRequest(handler, r =>
            r.RequestUri!.Query.Contains("situacao=I"),
            Times.Once());
    }

    [Fact]
    public async Task SearchAsync_OkResponse_DeserializesPagedResult()
    {
        var (client, _) = Build(TinyResponseFixtures.SearchProductsOk);

        var result = await client.SearchAsync(new SearchProductsRequest { Page = 1 }, CancellationToken.None);

        Assert.Equal("1", result.Page);
        Assert.Equal("2", result.TotalPages);
        Assert.Equal(2, result.Products!.Count);
        Assert.Equal("111", result.Products[0].Product!.Id);
        Assert.Equal("222", result.Products[1].Product!.Id);
    }

    [Fact]
    public async Task CreateAsync_Products_CallsIncluirEndpoint()
    {
        var (client, handler) = Build(TinyResponseFixtures.CreateProductOk);
        var products = new[] { new UpsertProductData { Name = "Novo Produto", Price = 19.90m } };

        await client.CreateAsync(products, CancellationToken.None);

        HttpMessageHandlerMock.VerifyRequest(handler, r =>
            r.RequestUri!.PathAndQuery.Contains("produto.incluir.php"),
            Times.Once());
    }

    [Fact]
    public async Task CreateAsync_OkResponse_DeserializesRecord()
    {
        var (client, _) = Build(TinyResponseFixtures.CreateProductOk);
        var products = new[] { new UpsertProductData { Name = "Novo Produto", Price = 19.90m } };

        var result = await client.CreateAsync(products, CancellationToken.None);

        Assert.Single(result.Records!);
        Assert.Equal("OK", result.Records![0].Record!.Status);
        Assert.Equal("999", result.Records[0].Record!.Id);
    }

    [Fact]
    public async Task CreateAsync_WithVariations_DeserializesVariationIds()
    {
        var (client, _) = Build(TinyResponseFixtures.CreateProductWithVariationsOk);
        var products = new[] { new UpsertProductData { Name = "Com Variações", Price = 50m } };

        var result = await client.CreateAsync(products, CancellationToken.None);

        var record = result.Records![0].Record!;
        Assert.Equal(2, record.Variations!.Count);
        Assert.Equal("801", record.Variations[0].Variation!.Id);
    }

    [Fact]
    public async Task UpdateAsync_Products_CallsAlterarEndpoint()
    {
        var (client, handler) = Build(TinyResponseFixtures.CreateProductOk);
        var products = new[] { new UpsertProductData { Id = 999L, Name = "Atualizado", Price = 25m } };

        await client.UpdateAsync(products, CancellationToken.None);

        HttpMessageHandlerMock.VerifyRequest(handler, r =>
            r.RequestUri!.PathAndQuery.Contains("produto.alterar.php"),
            Times.Once());
    }

    [Fact]
    public async Task GetByIdAsync_ApiError_ThrowsTinyApiException()
    {
        var (client, _) = Build(TinyResponseFixtures.ApiError);

        await Assert.ThrowsAsync<TinyApiException>(
            () => client.GetByIdAsync(1L, CancellationToken.None));
    }
}
