namespace JudgeBot.Core.Models;

public class Permission
{
    public Guid Uid { get; init; }
    public string Name { get; init; } = null!;
    public string? Description { get; init; }

    public virtual IList<Role> Roles { get; init; } = null!;
}