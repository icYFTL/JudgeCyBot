using JudgeBot.Presentation.Logic.CallbackHandlers.Bases;
using JudgeBot.Presentation.Models;
using JudgeBot.Presentation.Models.Interfaces;
using JudgeBot.Presentation.Resources;
using JudgeBot.Presentation.Resources.Keyboards;
using Telegram.Bot;
using Telegram.Bot.Types;
using User = JudgeBot.Core.Models.User;

namespace JudgeBot.Presentation.Logic.CallbackHandlers.Acts;

public class ShowMyActsCallbackHandler(IServiceScope scope) : BasePersonalCallbackHandler, ITelegramCallbackHandler
{
    public int HandlerId => 1;
    
    protected override async Task ExecuteCoreAsync(Update update, TelegramBotClient client, User user, CallbackData callbackData,
        CancellationToken cancellationToken)
    {
        var keyboard = Keyboards.MyActsKeyboard(update, null, user.LanguageId);
        await client.EditMessageTextAsync(update.CallbackQuery!.Message!.Chat.Id, update.CallbackQuery.Message.MessageId,
            keyboard.MessageDescription, replyMarkup: keyboard.Keyboard,
            cancellationToken: cancellationToken);
    }
}