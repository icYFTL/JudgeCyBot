using Telegram.Bot;
using Telegram.Bot.Types;

namespace JudgeBot.Presentation.Models.Interfaces;

public interface ITelegramCommand
{
    public string Command { get; }
    public Task ExecuteAsync(Message msg, TelegramBotClient client, Core.Models.User user, CancellationToken cancellationToken);
}