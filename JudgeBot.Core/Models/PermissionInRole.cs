using System.ComponentModel.DataAnnotations.Schema;

namespace JudgeBot.Core.Models;

public class PermissionInRole
{
    public Guid Uid { get; init; }
    public Guid PermissionUid { get; init; }
    public Guid RoleUid { get; init; }
    
    [ForeignKey("PermissionUid")]
    public virtual Permission Permission { get; init; } = null!;
    
    [ForeignKey("RoleUid")]
    public virtual Role Role { get; init; } = null!;
}