using JudgeBot.Presentation.Attributes;
using JudgeBot.Presentation.Extensions;
using JudgeBot.Presentation.Models;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace JudgeBot.Presentation.Resources.Keyboards;

public static partial class Keyboards
{
    [KeyboardId(Id = 2)]
    public static KeyboardObject MyActsKeyboard(Update? update = null, Message? message = null, string lang = "en")
    {
        Utils.Utils.SetUserCulture(lang);
        
        return new KeyboardObject
        {
            Keyboard = new InlineKeyboardMarkup()
                .AddButtonWithJson(Resources.Presentation.DefendantBtnLbl, new CallbackData
                {
                    HandlerId = 2
                })
                .AddButton(Resources.Presentation.VictimBtnLbl, "a_my_victim_acts")
                .AddButton(Resources.Presentation.AccuserBtnLbl, "a_my_accuser_acts")
                .AddNewRow()
                .AddButton(Resources.Presentation.MagistrateBtnLbl, "a_my_magistrate_acts")
                .AddButton(Resources.Presentation.JuryBtnLbl, "a_my_jury_acts")
                .AddNewRow()
                .AddButtonWithJson(Presentation.BackBtnLbl, new CallbackData
                {
                    HandlerId = 0
                }),
            MessageDescription = Presentation.MyActsMenuDescription
        };
    }
    
    [KeyboardId(Id = 5)]
    public static KeyboardObject NewActVictimSelectorKeyboard(Update? update = null, Message? message = null, string lang = "en")
    {
        Utils.Utils.SetUserCulture(lang);
        
        return new KeyboardObject
        {
            Keyboard = new InlineKeyboardMarkup()
                .AddButton(Resources.Presentation.NewActSceneSetActVictimIsMe, "me")
                .AddButton(Resources.Presentation.NewActSceneSetActVictimIsNobody, "nobody"),
            MessageDescription = Presentation.NewActSceneSetActVictim
        };
    }
    
    
}