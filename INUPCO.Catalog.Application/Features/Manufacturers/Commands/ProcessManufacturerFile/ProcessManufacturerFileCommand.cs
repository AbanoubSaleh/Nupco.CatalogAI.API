using INUPCO.Catalog.Application.Common.Interfaces;
using INUPCO.Catalog.Domain.Contracts;
using INUPCO.Catalog.Domain.Entities.Manufacturers;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace INUPCO.Catalog.Application.Features.Manufacturers.Commands;

public record ProcessManufacturerFileCommand : IRequest<ProcessManufacturerFileResult>
{
    public required IFormFile File { get; init; }
}

public record ProcessManufacturerFileResult
{
    public int ProcessedCount { get; init; }
    public List<string> Errors { get; init; } = new();
    public byte[]? ErrorReport { get; init; }
}

public record ManufacturerImportDto
{
    public string TradeCode { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
}

public class ProcessManufacturerFileCommandHandler 
    : IRequestHandler<ProcessManufacturerFileCommand, ProcessManufacturerFileResult>
{
    private readonly IExcelProcessor _excelProcessor;
    private readonly IManufacturerRepository _manufacturerRepository;
    private readonly ISubsidiaryRepository _subsidiaryRepository;
    private readonly IProductRepository _productRepository;

    public ProcessManufacturerFileCommandHandler(
        IExcelProcessor excelProcessor,
        IManufacturerRepository manufacturerRepository,
        ISubsidiaryRepository subsidiaryRepository,
        IProductRepository productRepository)
    {
        _excelProcessor = excelProcessor;
        _manufacturerRepository = manufacturerRepository;
        _subsidiaryRepository = subsidiaryRepository;
        _productRepository = productRepository;
    }

    public async Task<ProcessManufacturerFileResult> Handle(
        ProcessManufacturerFileCommand request, 
        CancellationToken cancellationToken)
    {
        var errors = new List<string>();
        var processedCount = 0;

        using var stream = request.File.OpenReadStream();
        var imports = await _excelProcessor.ProcessManufacturersFromExcel(stream);

        foreach (var import in imports)
        {
            try
            {
                var product = await _productRepository.GetByTradeCodeAsync(import.TradeCode);
                if (product == null)
                {
                    errors.Add($"Product with trade code {import.TradeCode} not found");
                    continue;
                }

                var manufacturer = await _manufacturerRepository.GetByNameAndCountryAsync(import.Name, import.Country);
                var subsidiary = await _subsidiaryRepository.GetByNameAndCountryAsync(import.Name, import.Country);

                if (manufacturer != null || subsidiary != null)
                {
                    product.UpdateDetails(product.Name, import.TradeCode);
                    await _productRepository.UpdateAsync(product);
                    processedCount++;
                }
                else
                {
                    errors.Add($"No manufacturer or subsidiary found for {import.Name} in {import.Country}. Please create manufacturer first.");
                }
            }
            catch (Exception ex)
            {
                errors.Add($"Error processing row: {ex.Message}");
            }
        }

        return new ProcessManufacturerFileResult
        {
            ProcessedCount = processedCount,
            Errors = errors,
            ErrorReport = errors.Any() ? _excelProcessor.GenerateErrorReport(errors) : null
        };
    }
} 