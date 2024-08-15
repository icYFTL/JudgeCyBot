using JudgeBot.Presentation.Logic.CallbackHandlers.Bases;
using JudgeBot.Presentation.Models;
using JudgeBot.Presentation.Models.Interfaces;
using JudgeBot.Presentation.Resources.Keyboards;
using Telegram.Bot;
using Telegram.Bot.Types;
using User = JudgeBot.Core.Models.User;

namespace JudgeBot.Presentation.Logic.CallbackHandlers.Settings;

public class ShowSettingsMenuCallbackHandler(IServiceScope scope) : BasePersonalCallbackHandler, ITelegramCallbackHandler
{
    public int HandlerId => 3;

    protected override async Task ExecuteCoreAsync(Update update, TelegramBotClient client, User user, CallbackData callbackData,
        CancellationToken cancellationToken)
    {
        var keyboard = Keyboards.SettingsKeyboard(update, null, user.LanguageId);
        await client.EditMessageTextAsync(update.CallbackQuery!.Message!.Chat.Id, update.CallbackQuery.Message.MessageId,
            keyboard.MessageDescription, replyMarkup: keyboard.Keyboard,
            cancellationToken: cancellationToken);
    }
}