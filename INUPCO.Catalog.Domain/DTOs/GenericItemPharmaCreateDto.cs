namespace INUPCO.Catalog.Domain.DTOs;

public class GenericItemPharmaCreateDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public string? CustomerCode { get; set; }
} 