using System.ComponentModel.DataAnnotations.Schema;

namespace JudgeBot.Core.Models;

public class Status
{
    public Guid Uid { get; init; }
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
    public Guid StatusTypeUid { get; init; }
    
    [ForeignKey("StatusTypeUid")]
    public virtual StatusType StatusType { get; init; } = null!;
}