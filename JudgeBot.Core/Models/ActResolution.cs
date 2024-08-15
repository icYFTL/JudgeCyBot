namespace JudgeBot.Core.Models;

public class ActResolution
{
    public Guid Uid { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsPositive { get; set; } // Мб лишнее
}