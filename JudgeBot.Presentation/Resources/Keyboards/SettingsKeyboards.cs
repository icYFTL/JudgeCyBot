using JudgeBot.Presentation.Attributes;
using JudgeBot.Presentation.Extensions;
using JudgeBot.Presentation.Models;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace JudgeBot.Presentation.Resources.Keyboards;

public static partial class Keyboards
{
    [KeyboardId(Id = 3)]
    public static KeyboardObject SettingsKeyboard(Update? update = null, Message? message = null, string lang = "en")
    {
        Utils.Utils.SetUserCulture(lang);
        
        return new KeyboardObject
        {
            Keyboard = new InlineKeyboardMarkup()
                .AddButtonWithJson(Resources.Presentation.LanguageBtnLbl, new CallbackData
                {
                    HandlerId = 4
                })
                .AddNewRow()
                .AddButtonWithJson(Presentation.BackBtnLbl, new CallbackData
                {
                    HandlerId = 0
                }),
            MessageDescription = Presentation.SettingsMenuDescription
        };
    }
    
    [KeyboardId(Id = 4)]
    public static KeyboardObject LanguageSettingsKeyboard(Update? update = null, Message? message = null, string lang = "en")
    {
        Utils.Utils.SetUserCulture(lang);
        
        return new KeyboardObject
        {
            Keyboard = new InlineKeyboardMarkup()
                .AddButtonWithJson(Resources.Presentation.RussianBtnLbl, new CallbackData
                {
                    HandlerId = 5,
                    CustomData = "ru"
                })
                .AddButtonWithJson(Presentation.EnglishBtnLbl, new CallbackData
                {
                    HandlerId = 5,
                    CustomData = "en"
                })
                .AddNewRow()
                .AddButtonWithJson(Presentation.BackBtnLbl, new CallbackData
                {
                    HandlerId = 0,
                    PrevKeyboard = new CallbackDataBackwards
                    {
                        PrevKeyboardId = 3
                    }
                }),
            MessageDescription = Presentation.LanguageMenuDescription
        };
    }
}