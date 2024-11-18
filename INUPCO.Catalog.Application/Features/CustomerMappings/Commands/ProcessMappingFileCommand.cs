using INUPCO.Catalog.Application.Common.Interfaces;
using INUPCO.Catalog.Domain.Contracts;
using INUPCO.Catalog.Domain.Contracts.Customers;
using INUPCO.Catalog.Domain.Entities.Customers;
using MediatR;
using Microsoft.AspNetCore.Http;
using ICustomerGenericItemMappingRepository = INUPCO.Catalog.Domain.Contracts.Customers.ICustomerGenericItemMappingRepository;

namespace INUPCO.Catalog.Application.Features.CustomerMappings.Commands;

public record ProcessMappingFileCommand : IRequest<ProcessMappingFileResult>
{
    public required IFormFile File { get; init; }
}

public record ProcessMappingFileResult
{
    public int SuccessCount { get; init; }
    public List<string> Errors { get; init; } = new();
    public byte[]? ErrorReport { get; init; }
}

public class ProcessMappingFileCommandHandler : IRequestHandler<ProcessMappingFileCommand, ProcessMappingFileResult>
{
    private readonly IExcelProcessor _excelProcessor;
    private readonly IGenericItemPharmaRepository _genericItemRepository;
    private readonly ICustomerGenericItemMappingRepository _mappingRepository;

    public ProcessMappingFileCommandHandler(
        IExcelProcessor excelProcessor,
        IGenericItemPharmaRepository genericItemRepository,
        ICustomerGenericItemMappingRepository mappingRepository)
    {
        _excelProcessor = excelProcessor;
        _genericItemRepository = genericItemRepository;
        _mappingRepository = mappingRepository;
    }

    public async Task<ProcessMappingFileResult> Handle(ProcessMappingFileCommand request, CancellationToken cancellationToken)
    {
        var errors = new List<string>();
        var successCount = 0;

        using var stream = request.File.OpenReadStream();
        var mappings = await _excelProcessor.ProcessMappingsFromExcel(stream);

        foreach (var mapping in mappings)
        {
            try
            {
                var exists = await _mappingRepository.ExistsAsync(mapping.CustomerCode, mapping.CustomerSpecificCode);
                if (exists)
                {
                    errors.Add($"Mapping for customer code {mapping.CustomerCode} with specific code {mapping.CustomerSpecificCode} already exists");
                    continue;
                }

                var genericItem = await _genericItemRepository.GetByCodeAsync(mapping.GenericItemCode);
                if (genericItem == null)
                {
                    errors.Add($"Generic item with code {mapping.GenericItemCode} not found");
                    continue;
                }

                var newMapping = CustomerGenericItemPharmaCodeMapping.Create(
                    mapping.CustomerCode,
                    mapping.CustomerSpecificCode,
                    genericItem);

                await _mappingRepository.AddAsync(newMapping);
                successCount++;
            }
            catch (Exception ex)
            {
                errors.Add($"Error processing mapping: {ex.Message}");
            }
        }

        var errorReport = errors.Any() ? _excelProcessor.GenerateErrorReport(errors) : null;

        return new ProcessMappingFileResult
        {
            SuccessCount = successCount,
            Errors = errors,
            ErrorReport = errorReport
        };
    }
} 