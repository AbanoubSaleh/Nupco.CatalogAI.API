using Microsoft.AspNetCore.Http;

namespace INUPCO.Catalog.Application.Common.Interfaces;

public interface IExcelProcessor
{
    Task<List<CustomerMappingDto>> ProcessMappingsFromExcel(Stream fileStream);
    Task<byte[]> GenerateTemplate(IEnumerable<string> genericCodes);
    byte[] GenerateErrorReport(List<string> errors);
}

public record CustomerMappingDto
{
    public string CustomerCode { get; init; } = string.Empty;
    public string CustomerSpecificCode { get; init; } = string.Empty;
    public string GenericItemCode { get; init; } = string.Empty;
} 