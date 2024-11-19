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
                // Step 1: Check for product with trade code
                var product = await _productRepository.GetByTradeCodeAsync(import.TradeCode);
                if (product == null)
                {
                    errors.Add($"Product with trade code {import.TradeCode} not found");
                    continue;
                }

                // Step 2: Check for manufacturer
                var manufacturer = await _manufacturerRepository.GetByNameAsync(import.Name);
                if (manufacturer == null)
                {
                    errors.Add($"Manufacturer {import.Name} not found");
                    continue;
                }

                // Step 3: Check if manufacturer is in the same country
                if (manufacturer.Country == import.Country)
                {
                    // Assign product to manufacturer directly
                    product.AssignToManufacturer(manufacturer.Id);
                    await _productRepository.UpdateAsync(product);
                    processedCount++;
                    continue;
                }

                // Step 4: If manufacturer exists but different country, check subsidiaries
                var subsidiaryInCountry = await _subsidiaryRepository.GetByManufacturerAndCountryAsync(
                    manufacturer.Id, 
                    import.Country
                );

                if (subsidiaryInCountry != null)
                {
                    // Assign product to subsidiary
                    product.AssignToSubsidiary(subsidiaryInCountry);
                    await _productRepository.UpdateAsync(product);
                    processedCount++;
                }
                else
                {
                    errors.Add($"No subsidiary found for manufacturer {import.Name} in country {import.Country}");
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