using INUPCO.Catalog.Domain.Common;
using INUPCO.Catalog.Domain.Enums;

namespace INUPCO.Catalog.Domain.Entities.GenericItemPharmas;

/// <summary>
/// Represents a comment on a Generic Item Pharma
/// </summary>
public class Comment : BaseEntity
{
    private Comment() { } // For EF Core

    internal Comment(string content, string userId, CommentType type)
    {
        Content = content;
        UserId = userId;
        Type = type;
        CreatedDate = DateTime.UtcNow;
    }

    public string Content { get; private set; } = string.Empty;
    public string UserId { get; private set; } = string.Empty;
    public CommentType Type { get; private set; }
} 