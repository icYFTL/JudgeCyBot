using JudgeBot.Presentation.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace JudgeBot.Presentation.Logic.CallbackHandlers.Bases;

public abstract class BasePersonalCallbackHandler
{
    public async Task ExecuteAsync(Update update, TelegramBotClient client, Core.Models.User user, CallbackData callbackData,
        CancellationToken cancellationToken)
    {
        if (!ValidateUpdate(update))
        {
            return;
        }

        await ExecuteCoreAsync(update, client, user, callbackData, cancellationToken).ConfigureAwait(false);
    }

    protected abstract Task ExecuteCoreAsync(Update update, TelegramBotClient client, Core.Models.User user, CallbackData callbackData,
        CancellationToken cancellationToken);

    protected virtual bool ValidateUpdate(Update update)
    {
        if (update.CallbackQuery?.From is null || update.CallbackQuery?.Message?.ReplyToMessage is null)
        {
            return false;
        }

        if (update.CallbackQuery.From.Id != update.CallbackQuery.Message.ReplyToMessage.From.Id)
        {
            return false;
        }

        return true;
    }
}