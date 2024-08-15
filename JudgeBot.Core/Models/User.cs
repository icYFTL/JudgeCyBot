namespace JudgeBot.Core.Models;

public class User
{
    /// <summary>
    ///  Уникальный идентификатор
    /// </summary>
    public long Id { get; init; }
    /// <summary>
    ///  ID последнего сообщения с inline menu
    /// </summary>
    public long? MenuMessageId { get; init; }
    public string LanguageId { get; set; }

    public virtual IList<Role> Roles { get; set; }
    public virtual IList<Act> ActsAsAccuser { get; set; }
    public virtual IList<Act> ActsAsDefendant { get; set; }
    public virtual IList<Act> ActsAsVictim { get; set; }
    public virtual IList<Act> ActsAsMagistrate { get; set; }
    public virtual IList<Act> ActsAsJury { get; set; }
    public virtual IList<Act> ActsCreated { get; set; }
    public virtual IList<Act> ActsEdited { get; set; }
}