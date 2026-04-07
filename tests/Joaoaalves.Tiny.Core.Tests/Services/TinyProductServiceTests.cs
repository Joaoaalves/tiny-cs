using Joaoaalves.Tiny.Abstractions.DTOs.Requests.Products;
using Joaoaalves.Tiny.Abstractions.Enums;
using Joaoaalves.Tiny.Core.Clients;
using Joaoaalves.Tiny.Core.DTOs.Common;
using Joaoaalves.Tiny.Core.DTOs.Products;
using Joaoaalves.Tiny.Core.Services;
using Moq;

namespace Joaoaalves.Tiny.Core.Tests.Services;

public class TinyProductServiceTests
{
    private readonly Mock<ITinyProductClient> _client = new();
    private readonly TinyProductService _service;

    public TinyProductServiceTests()
    {
        _service = new TinyProductService(_client.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ProductFound_ReturnsEntity()
    {
        _client.Setup(c => c.GetByIdAsync(123L, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TinyGetProductResponse
            {
                Status = "OK",
                Product = new TinyProductJson { Id = "123", Name = "Produto Teste", Price = "49.90" }
            });

        var result = await _service.GetByIdAsync(123L);

        Assert.NotNull(result);
        Assert.Equal(123L, result.Id);
        Assert.Equal("Produto Teste", result.Name);
        Assert.Equal(49.90m, result.Price);
    }

    [Fact]
    public async Task GetByIdAsync_NullProduct_ReturnsNull()
    {
        _client.Setup(c => c.GetByIdAsync(1L, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TinyGetProductResponse { Status = "OK", Product = null });

        var result = await _service.GetByIdAsync(1L);

        Assert.Null(result);
    }

    [Fact]
    public async Task SearchAsync_OkResponse_ReturnsMappedPagedResult()
    {
        _client.Setup(c => c.SearchAsync(It.IsAny<SearchProductsRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TinySearchProductsResponse
            {
                Status = "OK",
                Page = "1",
                TotalPages = "3",
                Products =
                [
                    new TinyProductSummaryListItem
                    {
                        Product = new TinyProductSummaryJson { Id = "10", Name = "A", Price = "5.00", Status = "A" }
                    },
                    new TinyProductSummaryListItem
                    {
                        Product = new TinyProductSummaryJson { Id = "20", Name = "B", Price = "8.00", Status = "I" }
                    }
                ]
            });

        var result = await _service.SearchAsync(new SearchProductsRequest { Page = 1 });

        Assert.Equal(1, result.Page);
        Assert.Equal(3, result.TotalPages);
        Assert.Equal(2, result.Items.Count);
        Assert.Equal(10L, result.Items[0].Id);
        Assert.Equal(ProductStatus.Inactive, result.Items[1].Status);
    }

    [Fact]
    public async Task SearchAsync_NullProducts_ReturnsEmptyItems()
    {
        _client.Setup(c => c.SearchAsync(It.IsAny<SearchProductsRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TinySearchProductsResponse { Status = "OK", Page = "1", TotalPages = "1", Products = null });

        var result = await _service.SearchAsync(new SearchProductsRequest());

        Assert.Empty(result.Items);
    }

    [Fact]
    public async Task CreateAsync_OkResponse_ReturnsMappedResults()
    {
        _client.Setup(c => c.CreateAsync(It.IsAny<IEnumerable<UpsertProductData>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TinyUpsertResponse
            {
                Status = "OK",
                Records =
                [
                    new TinyUpsertRegistroListItem
                    {
                        Record = new TinyUpsertRegistroJson { Sequence = "1", Status = "OK", Id = "999" }
                    }
                ]
            });

        var result = await _service.CreateAsync([new UpsertProductData { Name = "Novo", Price = 10m }]);

        Assert.Single(result);
        Assert.True(result[0].Success);
        Assert.Equal(999L, result[0].Id);
        Assert.Equal(1, result[0].Sequence);
    }

    [Fact]
    public async Task CreateAsync_ErrorRecord_ReturnsFailed()
    {
        _client.Setup(c => c.CreateAsync(It.IsAny<IEnumerable<UpsertProductData>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TinyUpsertResponse
            {
                Status = "OK",
                Records =
                [
                    new TinyUpsertRegistroListItem
                    {
                        Record = new TinyUpsertRegistroJson
                        {
                            Sequence = "1",
                            Status = "Erro",
                            Errors = [new TinyApiErrorListItem { Error = "Nome obrigatório." }]
                        }
                    }
                ]
            });

        var result = await _service.CreateAsync([new UpsertProductData()]);

        Assert.Single(result);
        Assert.False(result[0].Success);
        Assert.Single(result[0].Errors);
        Assert.Equal("Nome obrigatório.", result[0].Errors[0]);
    }

    [Fact]
    public async Task CreateAsync_WithVariations_MapsVariationIds()
    {
        _client.Setup(c => c.CreateAsync(It.IsAny<IEnumerable<UpsertProductData>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TinyUpsertResponse
            {
                Status = "OK",
                Records =
                [
                    new TinyUpsertRegistroListItem
                    {
                        Record = new TinyUpsertRegistroJson
                        {
                            Sequence = "1",
                            Status = "OK",
                            Id = "800",
                            Variations =
                            [
                                new TinyVariacaoIdListItem { Variation = new TinyVariacaoIdJson { Id = "801" } },
                                new TinyVariacaoIdListItem { Variation = new TinyVariacaoIdJson { Id = "802" } }
                            ]
                        }
                    }
                ]
            });

        var result = await _service.CreateAsync([new UpsertProductData { Name = "Com Variações", Price = 50m }]);

        Assert.Equal(2, result[0].VariationIds.Count);
        Assert.Contains(801L, result[0].VariationIds);
        Assert.Contains(802L, result[0].VariationIds);
    }

    [Fact]
    public async Task UpdateAsync_OkResponse_ReturnsMappedResults()
    {
        _client.Setup(c => c.UpdateAsync(It.IsAny<IEnumerable<UpsertProductData>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TinyUpsertResponse
            {
                Status = "OK",
                Records =
                [
                    new TinyUpsertRegistroListItem
                    {
                        Record = new TinyUpsertRegistroJson { Sequence = "1", Status = "OK", Id = "123" }
                    }
                ]
            });

        var result = await _service.UpdateAsync([new UpsertProductData { Id = 123L, Name = "Atualizado", Price = 20m }]);

        Assert.Single(result);
        Assert.True(result[0].Success);
        Assert.Equal(123L, result[0].Id);
    }
}
