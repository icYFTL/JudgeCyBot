using Telegram.Bot.Types;
using TelegramBot.Scenes.Models.Fundamentals;

namespace TelegramBot.Scenes.Extensions;

public static class TelegramBotExtensions
{
    public static UserInfo GetUserInfoFromMessage(this Update update) =>
        new UserInfo(update.Message!.From!.Id, update.Message.Chat.Id);
    
    public static UserInfo GetUserInfoFromMessage(this Message message) =>
        new UserInfo(message!.From!.Id, message.Chat.Id);
    
    public static UserInfo GetUserInfoFromCallbackQuery(this Update update) =>
        new UserInfo(update.CallbackQuery!.From!.Id,update.CallbackQuery.Message!.Chat.Id);
}