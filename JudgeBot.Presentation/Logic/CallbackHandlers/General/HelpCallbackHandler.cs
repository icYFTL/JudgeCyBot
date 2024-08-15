using JudgeBot.Presentation.Logic.CallbackHandlers.Bases;
using JudgeBot.Presentation.Models;
using JudgeBot.Presentation.Models.Interfaces;
using JudgeBot.Presentation.Resources.Keyboards;
using Telegram.Bot;
using Telegram.Bot.Types;
using User = JudgeBot.Core.Models.User;

namespace JudgeBot.Presentation.Logic.CallbackHandlers.General;

public class HelpCallbackHandler(IServiceScope scope) : BasePersonalCallbackHandler, ITelegramCallbackHandler
{
    public int HandlerId => 6;
    protected override async Task ExecuteCoreAsync(Update update, TelegramBotClient client, User user, CallbackData callbackData,
        CancellationToken cancellationToken)
    {
        var keyboard = Keyboards.StartKeyboard(update, null, user.LanguageId);
        
        await client.EditMessageTextAsync(update.CallbackQuery!.Message!.Chat.Id,
            update.CallbackQuery!.Message!.MessageId, Resources.Presentation.HelpCommandDescription,
            replyMarkup: keyboard.Keyboard,
            cancellationToken: cancellationToken);
    }
}