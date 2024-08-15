namespace JudgeBot.Core.Models;

public class Role
{
    public Guid Uid { get; init; }
    public string Name { get; init; } = null!;
    public string? Description { get; init; }

    public virtual IList<Permission> Permissions { get; set; }
    public virtual IList<User> Users { get; set; }

}