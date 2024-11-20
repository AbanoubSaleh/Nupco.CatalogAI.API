using INUPCO.Catalog.Application.Common.Interfaces;
using INUPCO.Catalog.Domain.Contracts;
using INUPCO.Catalog.Domain.Entities.Manufacturers;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
        var errors = new ConcurrentBag<string>();
        var processedCount = 0;

        using var stream = request.File.OpenReadStream();
        var imports = await _excelProcessor.ProcessManufacturersFromExcel(stream);

        var manufacturers = await _manufacturerRepository.GetByNamesAsync(
            imports.Select(i => i.Name).Distinct().ToList());
        var manufacturersByName = manufacturers.ToDictionary(m => m.Name, m => m);

        var productsByTradeCode = await _productRepository.GetByTradeCodesAsync(
            imports.Select(i => i.TradeCode).Distinct().ToList());

        var batchSize = 100;
        var batches = imports.Chunk(batchSize);
        
        foreach (var batch in batches)
        {
            var tasks = batch.Select(async import =>
            {
                try
                {
                    if (!productsByTradeCode.TryGetValue(import.TradeCode, out var product))
                    {
                        errors.Add($"Product with trade code {import.TradeCode} not found");
                        return;
                    }

                    if (manufacturersByName == null || !manufacturersByName.TryGetValue(import.Name, out var manufacturer))
                    {
                        errors.Add($"Manufacturer {import.Name} not found");
                        return;
                    }

                    if (manufacturer.Country == import.Country)
                    {
                        product.AssignToManufacturer(manufacturer.Id);
                        Interlocked.Increment(ref processedCount);
                        return;
                    }

                    var subsidiary = manufacturer.Subsidiaries
                        .FirstOrDefault(s => s.Country == import.Country);

                    if (subsidiary != null)
                    {
                        product.AssignToSubsidiary(subsidiary);
                        Interlocked.Increment(ref processedCount);
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
            });

            await Task.WhenAll(tasks);
            
            await _productRepository.BulkUpdateAsync(
                batch.Where(b => productsByTradeCode.ContainsKey(b.TradeCode))
                    .Select(b => productsByTradeCode[b.TradeCode])
                    .Where(p => p != null)
                    .ToList());
        }

        return new ProcessManufacturerFileResult
        {
            ProcessedCount = processedCount,
            Errors = errors.ToList(),
            ErrorReport = errors.Any() ? _excelProcessor.GenerateErrorReport(errors.ToList()) : null
        };
    }
} 