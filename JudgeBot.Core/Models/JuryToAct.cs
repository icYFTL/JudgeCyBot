using System.ComponentModel.DataAnnotations.Schema;

namespace JudgeBot.Core.Models;

public class JuryToAct
{
    public Guid Uid { get; init; }
    public long UserId { get; init; }
    public Guid ActUid { get; init; }

    [ForeignKey("UserId")]
    public virtual User User { get; init; }

    [ForeignKey("ActUid")]
    public virtual Act Act { get; init; }
}