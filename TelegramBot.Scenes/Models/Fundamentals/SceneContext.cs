using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Scenes.Models.Fundamentals;

public partial class SceneContext
{
    public ITelegramBotClient Client { get; init; } = null!;
    public Update Update { get; init; } = null!;
    public BaseScene Scene { get; init; } = null!;
}