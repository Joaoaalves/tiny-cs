using Joaoaalves.Tiny.Abstractions.DTOs.Requests.Stock;
using Joaoaalves.Tiny.Abstractions.Enums;
using Joaoaalves.Tiny.Core.Clients;
using Joaoaalves.Tiny.Core.DTOs.Common;
using Joaoaalves.Tiny.Core.DTOs.Stock;
using Joaoaalves.Tiny.Core.Services;
using Moq;

namespace Joaoaalves.Tiny.Core.Tests.Services;

public class TinyStockServiceTests
{
    private readonly Mock<ITinyStockClient> _client = new();
    private readonly TinyStockService _service;

    public TinyStockServiceTests()
    {
        _service = new TinyStockService(_client.Object);
    }

    [Fact]
    public async Task GetByProductIdAsync_StockFound_ReturnsEntity()
    {
        _client.Setup(c => c.GetByProductIdAsync(123L, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TinyGetProductStockResponse
            {
                Status = "OK",
                Product = new TinyProductStockJson
                {
                    Id = "123",
                    Name = "Camiseta Branca G",
                    Balance = "42",
                    ReservedBalance = "3"
                }
            });

        var result = await _service.GetByProductIdAsync(123L);

        Assert.NotNull(result);
        Assert.Equal(123L, result.Id);
        Assert.Equal("Camiseta Branca G", result.Name);
        Assert.Equal(42m, result.Balance);
        Assert.Equal(3m, result.ReservedBalance);
    }

    [Fact]
    public async Task GetByProductIdAsync_NullProduct_ReturnsNull()
    {
        _client.Setup(c => c.GetByProductIdAsync(1L, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TinyGetProductStockResponse { Status = "OK", Product = null });

        var result = await _service.GetByProductIdAsync(1L);

        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_OkResponse_ReturnsSuccess()
    {
        _client.Setup(c => c.UpdateAsync(It.IsAny<UpdateStockData>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TinyUpdateStockResponse
            {
                Status = "OK",
                Registros = new TinyUpdateStockRegistrosJson
                {
                    Record = new TinyUpdateStockRegistroJson
                    {
                        Sequence = "1",
                        Status = "OK",
                        Id = "123"
                    }
                }
            });

        var result = await _service.UpdateAsync(new UpdateStockData
        {
            ProductId = 123L,
            Type = StockUpdateType.Entry,
            Quantity = 10m
        });

        Assert.True(result.Success);
        Assert.Equal(123L, result.Id);
        Assert.Equal(1, result.Sequence);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task UpdateAsync_NullRecord_ReturnsFailure()
    {
        _client.Setup(c => c.UpdateAsync(It.IsAny<UpdateStockData>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TinyUpdateStockResponse
            {
                Status = "OK",
                Registros = null
            });

        var result = await _service.UpdateAsync(new UpdateStockData { ProductId = 1L });

        Assert.False(result.Success);
        Assert.Single(result.Errors);
    }

    [Fact]
    public async Task UpdateAsync_ErrorRecord_ReturnsErrors()
    {
        _client.Setup(c => c.UpdateAsync(It.IsAny<UpdateStockData>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TinyUpdateStockResponse
            {
                Status = "OK",
                Registros = new TinyUpdateStockRegistrosJson
                {
                    Record = new TinyUpdateStockRegistroJson
                    {
                        Sequence = "1",
                        Status = "Erro",
                        Errors = [new TinyApiErrorListItem { Error = "Produto não encontrado." }]
                    }
                }
            });

        var result = await _service.UpdateAsync(new UpdateStockData { ProductId = 99L });

        Assert.False(result.Success);
        Assert.Single(result.Errors);
        Assert.Equal("Produto não encontrado.", result.Errors[0]);
    }

    [Fact]
    public async Task ListUpdatesAsync_OkResponse_ReturnsMappedPagedResult()
    {
        _client.Setup(c => c.ListUpdatesAsync(It.IsAny<ListStockUpdatesRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TinyListStockUpdatesResponse
            {
                Status = "OK",
                Page = "1",
                TotalPages = "1",
                Products =
                [
                    new TinyStockUpdateEntryListItem
                    {
                        Product = new TinyStockUpdateEntryJson
                        {
                            Id = "123",
                            Name = "Camiseta Branca G",
                            Balance = "42",
                            UpdatedAt = "06/04/2024 14:30:00",
                            VariationType = ""
                        }
                    }
                ]
            });

        var result = await _service.ListUpdatesAsync(new ListStockUpdatesRequest
        {
            UpdatedSince = new DateTime(2024, 4, 6),
            Page = 1
        });

        Assert.Equal(1, result.Page);
        Assert.Equal(1, result.TotalPages);
        Assert.Single(result.Items);
        Assert.Equal(123L, result.Items[0].Id);
        Assert.Equal(42m, result.Items[0].Balance);
        Assert.Equal(VariationType.Normal, result.Items[0].VariationType);
    }

    [Fact]
    public async Task ListUpdatesAsync_NullProducts_ReturnsEmptyItems()
    {
        _client.Setup(c => c.ListUpdatesAsync(It.IsAny<ListStockUpdatesRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TinyListStockUpdatesResponse
            {
                Status = "OK",
                Page = "1",
                TotalPages = "1",
                Products = null
            });

        var result = await _service.ListUpdatesAsync(new ListStockUpdatesRequest { UpdatedSince = DateTime.Today });

        Assert.Empty(result.Items);
    }
}
