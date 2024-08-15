using JudgeBot.Presentation.Extensions;
using JudgeBot.Presentation.Logic.CallbackHandlers.Bases;
using JudgeBot.Presentation.Models;
using JudgeBot.Presentation.Models.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using User = JudgeBot.Core.Models.User;

namespace JudgeBot.Presentation.Logic.CallbackHandlers;

public class BackCallbackHandler(IServiceScope scope) : BasePersonalCallbackHandler, ITelegramCallbackHandler
{
    public int HandlerId => 0;

    protected override async Task ExecuteCoreAsync(Update update, TelegramBotClient client, User user,
        CallbackData callbackData,
        CancellationToken cancellationToken)
    {
        var prevKeyboard = update.CallbackQuery!.Message!.ReplyMarkup!.GetPrevKeyboard(update, null, user.LanguageId);

        await client.EditMessageTextAsync(update.CallbackQuery!.Message!.Chat.Id,
            update.CallbackQuery!.Message!.MessageId, prevKeyboard!.MessageDescription,
            replyMarkup: prevKeyboard.Keyboard, cancellationToken: cancellationToken);
    }
}