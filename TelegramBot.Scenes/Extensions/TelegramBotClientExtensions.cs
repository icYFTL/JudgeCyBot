using Telegram.Bot;
using TelegramBot.Scenes.Logic;

namespace TelegramBot.Scenes.Extensions;

public static class TelegramBotClientExtensions
{
    public static void HandleScenes(this TelegramBotClient client, ScenesManager scenesManager)
    {
        client.OnUpdate += scenesManager.OnUpdateReceivedAsync;
        client.OnMessage += scenesManager.OnMessageReceivedAsync;
    }
}