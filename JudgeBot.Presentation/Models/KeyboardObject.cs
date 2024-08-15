using Telegram.Bot.Types.ReplyMarkups;

namespace JudgeBot.Presentation.Models;

public class KeyboardObject
{
    public InlineKeyboardMarkup Keyboard { get; init; } = null!;
    public string MessageDescription { get; init; } = null!;
}