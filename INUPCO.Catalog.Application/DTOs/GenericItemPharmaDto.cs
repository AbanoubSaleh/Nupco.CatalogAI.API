using INUPCO.Catalog.Domain.Enums;

namespace INUPCO.Catalog.Application.DTOs;

public record GenericItemPharmaDto
{
    public int Id { get; init; }
    public string Code { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string? CustomerCode { get; init; }
    public bool IsActive { get; init; }
    public string Status { get; init; } = string.Empty;
    public IReadOnlyCollection<CommentDto> Comments { get; init; } = Array.Empty<CommentDto>();
}

public record CommentDto
{
    public string Content { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
    public DateTime CreatedDate { get; init; }
} 