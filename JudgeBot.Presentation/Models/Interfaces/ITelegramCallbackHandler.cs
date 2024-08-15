using Telegram.Bot;
using Telegram.Bot.Types;

namespace JudgeBot.Presentation.Models.Interfaces;

public interface ITelegramCallbackHandler
{
    public int HandlerId { get; }

    public Task ExecuteAsync(Update update, TelegramBotClient client, JudgeBot.Core.Models.User user, CallbackData callbackData,
        CancellationToken cancellationToken);
}