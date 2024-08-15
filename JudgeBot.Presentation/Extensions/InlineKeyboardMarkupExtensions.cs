using System.Reflection;
using System.Text.Json;
using JudgeBot.Presentation.Attributes;
using JudgeBot.Presentation.Models;
using JudgeBot.Presentation.Resources.Keyboards;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace JudgeBot.Presentation.Extensions;

public static class InlineKeyboardMarkupExtensions
{
    public static int? GetKeyboardId(this InlineKeyboardMarkup keyboardMarkup)
    {
        var keyboards = typeof(Keyboards).GetProperties();
        foreach (var prop in keyboards)
        {
            var currentKeyboardId = prop.GetCustomAttribute<KeyboardIdAttribute>();
            if ((InlineKeyboardMarkup)prop.GetValue(null)! == keyboardMarkup) return currentKeyboardId!.Id;
        }

        return null;
    }

    public static InlineKeyboardMarkup AddButtonWithJson(this InlineKeyboardMarkup keyboardMarkup, string text, object callbackData)
    {
        var data = JsonSerializer.Serialize(callbackData);
        var button = InlineKeyboardButton.WithCallbackData(text, data);

        keyboardMarkup.AddButton(button);

        return keyboardMarkup;
    }
    
    public static KeyboardObject? GetPrevKeyboard(this InlineKeyboardMarkup keyboardMarkup, Update? update = null, Message? message = null, string lang = "en")
    {
        foreach (var row in keyboardMarkup.InlineKeyboard)
        {
            foreach (var btn in row)
            {
                var btnData = btn.GetJsonFromCallbackData<CallbackData>();
                if (btnData is null) continue;

                if (btnData.HandlerId == 0)
                {
                    if (btnData.PrevKeyboard is null) return null;
                    
                    foreach (var methodInfo in Keyboards.KeyboardsObjectsMethodInfos)
                    {
                        var currentKeyboardId = methodInfo.GetCustomAttribute<KeyboardIdAttribute>();
                        if (currentKeyboardId!.Id != btnData.PrevKeyboard.PrevKeyboardId) continue;

                        return (KeyboardObject)methodInfo.Invoke(null, new object? []{update, message, lang})!;
                    }
                }
            }
        }
        
        return null;
    }
}