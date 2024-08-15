using System.ComponentModel.DataAnnotations.Schema;

namespace JudgeBot.Core.Models;

public class UserInRole
{
    public Guid Uid { get; init; }
    public long UserId { get; init; }
    public Guid RoleUid { get; init; }

    [ForeignKey("UserId")]
    public virtual User User { get; init; } = null!;
    
    [ForeignKey("RoleUid")]
    public virtual Role Role { get; init; } = null!;
}