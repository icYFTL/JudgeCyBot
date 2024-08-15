using JudgeBot.Core.Models;
using TelegramBot.Scenes.Models.Fundamentals;

namespace JudgeBot.Presentation.Logic.Scenes.Types;

public class ActScene : BaseScene
{
    public Act Act { get; set; }
    public int MessageId { get; set; }
    
    public ActScene(string name) : base(name)
    {
        Act = new Act();
    }
}