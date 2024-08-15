using System.Reflection;
using JudgeBot.Presentation.Attributes;
using JudgeBot.Presentation.Extensions;
using JudgeBot.Presentation.Models;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace JudgeBot.Presentation.Resources.Keyboards;

public static partial class Keyboards
{
    private static List<MethodInfo>? _keyboards;

    public static List<MethodInfo> KeyboardsObjectsMethodInfos
    {
        get
        {
            if (_keyboards is null)
            {
                _keyboards = typeof(Keyboards).GetMethods().Where(m => m.ReturnType == typeof(KeyboardObject))
                    .ToList();
            }

            return _keyboards;
        }
    }

    public static void CheckKeyboards()
    {
        var keyboards = typeof(Keyboards).GetMethods();
        var keyboardsIds = new List<int>();

        foreach (var keyboard in keyboards)
        {
            var attr = keyboard.GetCustomAttribute<KeyboardIdAttribute>();
            if (attr is null)
                continue; // throw new Exception($"Empty keyboard id at {nameof(keyboard)}");

            if (keyboardsIds.Contains(attr.Id))
                throw new Exception($"Duplicate keyboard id at {nameof(keyboard)}");

            keyboardsIds.Add(attr.Id);
        }
    }

    [KeyboardId(Id = 1)]
    public static KeyboardObject StartKeyboard(Update? update = null, Message? message = null, string lang = "en")
    {
        Utils.Utils.SetUserCulture(lang);

        return new KeyboardObject
        {
            Keyboard = new InlineKeyboardMarkup()
                .AddButtonWithJson(Resources.Presentation.MyActs, new CallbackData { HandlerId = 1 })
                .AddNewRow()
                .AddButtonWithJson(Resources.Presentation.SettingsMenuBtnLbl, new CallbackData
                {
                    HandlerId = 3
                })
                .AddNewRow()
                .AddButtonWithJson(Resources.Presentation.HelpCommandBtnLbl, new CallbackData
                {
                    HandlerId = 6
                }),
            MessageDescription =
                string.Format(Presentation.StartMenuDescription,
                    $"@{update?.CallbackQuery?.From?.Username ?? message?.From?.Username}")
        };
    }

    [KeyboardId(Id = 6)]
    public static KeyboardObject YesNoKeyboard(Update? update = null, Message? message = null, string lang = "en")
    {
        Utils.Utils.SetUserCulture(lang);

        return new KeyboardObject
        {
            Keyboard = new InlineKeyboardMarkup()
                .AddButton(Presentation.Yes, "1")
                .AddButton(Presentation.No, "0"),
            MessageDescription = String.Empty
        };
    }
}