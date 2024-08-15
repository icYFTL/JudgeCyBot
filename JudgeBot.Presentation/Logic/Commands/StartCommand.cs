using JudgeBot.Infrastructure.Database.Repositories;
using JudgeBot.Presentation.Attributes;
using JudgeBot.Presentation.Models.Interfaces;
using JudgeBot.Presentation.Resources.Keyboards;
using Telegram.Bot;
using Telegram.Bot.Types;
using User = JudgeBot.Core.Models.User;

namespace JudgeBot.Presentation.Logic.Commands;

[PrivateChatOnly]
public class StartCommand(IServiceScope scope) : ITelegramCommand
{
    public string Command => "/start";

    public async Task ExecuteAsync(Message msg, TelegramBotClient client, User user,
        CancellationToken cancellationToken)
    {
        var userRepository = scope.ServiceProvider.GetRequiredService<UserRepository>();
        
        if (!await userRepository.IsUserExistsAsync(msg.From!.Id, cancellationToken))
        {
            await userRepository.AddUserAsync(new User
            {
                Id = msg.From!.Id
            }, cancellationToken);
        }
        
        var keyboard = Keyboards.StartKeyboard(message: msg, lang: user.LanguageId);
        
        await client.SendTextMessageAsync(msg.Chat.Id, keyboard.MessageDescription,
            replyParameters: new ReplyParameters
            {
                ChatId = msg.Chat.Id,
                MessageId = msg.MessageId
            }, replyMarkup: keyboard.Keyboard,
            cancellationToken: cancellationToken);
    }
}