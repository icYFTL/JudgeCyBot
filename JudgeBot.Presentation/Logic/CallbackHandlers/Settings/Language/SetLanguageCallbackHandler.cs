using JudgeBot.Infrastructure.Database.Repositories;
using JudgeBot.Presentation.Logic.CallbackHandlers.Bases;
using JudgeBot.Presentation.Models;
using JudgeBot.Presentation.Models.Interfaces;
using JudgeBot.Presentation.Resources;
using JudgeBot.Presentation.Resources.Keyboards;
using Telegram.Bot;
using Telegram.Bot.Types;
using User = JudgeBot.Core.Models.User;

namespace JudgeBot.Presentation.Logic.CallbackHandlers.Settings.Language;

public class SetLanguageCallbackHandler(IServiceScope scope) : BasePersonalCallbackHandler, ITelegramCallbackHandler
{
    public int HandlerId => 5;
    protected override async Task ExecuteCoreAsync(Update update, TelegramBotClient client, User user, CallbackData callbackData,
        CancellationToken cancellationToken)
    {
        var userRepository = scope.ServiceProvider.GetRequiredService<UserRepository>();
        await userRepository.ChangeLanguageAsync(user, callbackData.CustomData!, cancellationToken);
        
        var keyboard = Keyboards.LanguageSettingsKeyboard(update, null, user.LanguageId);
        
        await client.EditMessageTextAsync(update.CallbackQuery!.Message!.Chat.Id, update.CallbackQuery.Message.MessageId,
            keyboard.MessageDescription, replyMarkup: keyboard.Keyboard,
            cancellationToken: cancellationToken);
    }
}