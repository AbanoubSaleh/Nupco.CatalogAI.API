namespace INUPCO.Catalog.Domain.DTOs;

public class GenericItemPharmaCreateForMigrationDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public string? CustomerCode { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? CreatedBy { get; set; }
} 