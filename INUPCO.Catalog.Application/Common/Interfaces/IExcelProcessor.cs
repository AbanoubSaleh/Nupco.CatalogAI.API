using Microsoft.AspNetCore.Http;

namespace INUPCO.Catalog.Application.Common.Interfaces;

public interface IExcelProcessor
{
    Task<List<CustomerMappingDto>> ProcessMappingsFromExcel(Stream fileStream);
    Task<List<ManufacturerImportDto>> ProcessManufacturersFromExcel(Stream fileStream);
    Task<List<SubsidiaryImportDto>> ProcessSubsidiariesFromExcel(Stream fileStream);
    Task<byte[]> GenerateTemplate(IEnumerable<string> genericCodes);
    byte[] GenerateErrorReport(List<string> errors);
    byte[] GenerateManufacturerTemplate();
}

public record CustomerMappingDto
{
    public string CustomerCode { get; init; } = string.Empty;
    public string CustomerSpecificCode { get; init; } = string.Empty;
    public string GenericItemCode { get; init; } = string.Empty;
}

public record ManufacturerImportDto
{
    public string Name { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public string TradeCode { get; init; } = string.Empty;
}

public record SubsidiaryImportDto
{
    public string Name { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public string ManufacturerName { get; init; } = string.Empty;
    public string ManufacturerCountry { get; init; } = string.Empty;
} 