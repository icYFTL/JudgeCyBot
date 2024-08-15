using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace JudgeBot.Presentation.Models.Interfaces;

public interface ITelegramEvent
{
    UpdateType Type { get; }
    Task ExecuteAsync(Message msg, TelegramBotClient client, Core.Models.User user, CancellationToken cancellationToken);
}