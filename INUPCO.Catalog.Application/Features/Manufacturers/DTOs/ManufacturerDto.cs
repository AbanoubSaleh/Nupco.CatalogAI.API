namespace INUPCO.Catalog.Application.Features.Manufacturers.DTOs;

public record ManufacturerDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public List<ProductDto> Products { get; init; } = new();
    public List<SubsidiaryDto> Subsidiaries { get; init; } = new();
}

public record ProductDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string TradeCode { get; init; } = string.Empty;
}

public record SubsidiaryDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
} 