namespace JudgeBot.Core.Models;

public class StatusType
{
    public Guid Uid { get; init; }
    public string Name { get; init; } = null!;
    public string? Description { get; init; } 
}