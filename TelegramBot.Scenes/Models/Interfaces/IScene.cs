using Telegram.Bot.Types.Enums;
using TelegramBot.Scenes.Models.Fundamentals;

namespace TelegramBot.Scenes.Models.Interfaces;

public interface IScene
{
    BaseScene OnEnter(Func<SceneContext, Task> onEnter);
    BaseScene OnLeave(Func<SceneContext, Task> onLeave);
    BaseScene Command(string command, Func<SceneContext, Task<bool>> onCommand);
    BaseScene Hears(string text, Func<SceneContext, Task<bool>> onHears);
    BaseScene On(UpdateType type, Func<SceneContext, Task<bool>> onOn);
}
