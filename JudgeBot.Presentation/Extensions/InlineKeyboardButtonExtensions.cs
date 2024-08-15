using System.Text.Json;
using Telegram.Bot.Types.ReplyMarkups;

namespace JudgeBot.Presentation.Extensions;

public static class InlineKeyboardButtonExtensions
{
    public static T? GetJsonFromCallbackData<T>(this InlineKeyboardButton btn)
    {
        if (String.IsNullOrEmpty(btn.CallbackData)) return default;
        try
        {
            return JsonSerializer.Deserialize<T>(btn.CallbackData);
        }
        catch (JsonException)
        {
            return default;
        }
    }
}