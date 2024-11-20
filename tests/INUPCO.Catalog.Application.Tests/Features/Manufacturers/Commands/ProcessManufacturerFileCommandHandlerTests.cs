using INUPCO.Catalog.Application.Common.Interfaces;
using INUPCO.Catalog.Application.Features.Manufacturers.Commands;
using INUPCO.Catalog.Domain.Contracts;
using INUPCO.Catalog.Domain.Entities.Manufacturers;
using INUPCO.Catalog.Domain.Entities.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;

public class ProcessManufacturerFileCommandHandlerTests
{
    private readonly Mock<IExcelProcessor> _excelProcessorMock;
    private readonly Mock<IManufacturerRepository> _manufacturerRepositoryMock;
    private readonly Mock<ISubsidiaryRepository> _subsidiaryRepositoryMock;
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IMemoryCache> _cacheMock;
    private readonly ProcessManufacturerFileCommandHandler _handler;

    public ProcessManufacturerFileCommandHandlerTests()
    {
        _excelProcessorMock = new Mock<IExcelProcessor>();
        _manufacturerRepositoryMock = new Mock<IManufacturerRepository>();
        _subsidiaryRepositoryMock = new Mock<ISubsidiaryRepository>();
        _productRepositoryMock = new Mock<IProductRepository>();
        _cacheMock = new Mock<IMemoryCache>();

        _handler = new ProcessManufacturerFileCommandHandler(
            _excelProcessorMock.Object,
            _manufacturerRepositoryMock.Object,
            _subsidiaryRepositoryMock.Object,
            _productRepositoryMock.Object,
            _cacheMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldProcessFileAndReturnResult()
    {
        // Arrange
        var fileMock = new Mock<IFormFile>();
        var stream = new MemoryStream();
        fileMock.Setup(f => f.OpenReadStream()).Returns(stream);

        var command = new ProcessManufacturerFileCommand { File = fileMock.Object };

        var manufacturerImports = new List<INUPCO.Catalog.Application.Common.Interfaces.ManufacturerImportDto>
        {
            new INUPCO.Catalog.Application.Common.Interfaces.ManufacturerImportDto { Name = "Manufacturer1", Country = "Country1", TradeCode = "TradeCode1" }
        };

        _excelProcessorMock.Setup(p => p.ProcessManufacturersFromExcel(It.IsAny<Stream>()))
            .ReturnsAsync(manufacturerImports);

        _productRepositoryMock.Setup(p => p.GetByTradeCodesAsync(It.IsAny<List<string>>()))
            .ReturnsAsync(new Dictionary<string, Product>());

        // Setup cache mock
        var cacheEntry = new Mock<ICacheEntry>();
        _cacheMock.Setup(x => x.CreateEntry(It.IsAny<object>()))
            .Returns(cacheEntry.Object);

        var manufacturer = Manufacturer.Create("Manufacturer1", "Country1");

        var manufacturerNames = new List<string> { "Manufacturer1" };
        _manufacturerRepositoryMock.Setup(m => m.GetByNamesAsync(manufacturerNames))
            .ReturnsAsync(new List<Manufacturer> { manufacturer });

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(0, result.ProcessedCount);
        Assert.NotNull(result.Errors);
    }
} 