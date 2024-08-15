using JudgeBot.Infrastructure.Database.Repositories;
using JudgeBot.Presentation.Attributes;
using JudgeBot.Presentation.Logic.Commands;
using JudgeBot.Presentation.Models.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Scenes.Logic;
using User = JudgeBot.Core.Models.User;

namespace JudgeBot.Presentation.Logic.Events;

public class MentionEvent(IServiceScope scope) : ITelegramEvent, ITelegramTextEvent
{
    public UpdateType Type => UpdateType.Message;
    public string Text => scope.ServiceProvider.GetRequiredService<IConfiguration>()["Telegram:BotMention"]!;  // Idk
    public async Task ExecuteAsync(Message msg, TelegramBotClient client, User user, CancellationToken cancellationToken)
    {
        if (msg.ReplyToMessage is null)
            await new StartCommand(scope).ExecuteAsync(msg, client, user, cancellationToken).ConfigureAwait(false);

        var userRepository = scope.ServiceProvider.GetRequiredService<UserRepository>();
        var targetUser = await userRepository.GetUserByIdAsync(msg.ReplyToMessage!.From!.Id, cancellationToken).ConfigureAwait(false);
        targetUser ??= await userRepository.AddUserAsync(new User
        {
            Id = msg.ReplyToMessage.From!.Id
        }, default).ConfigureAwait(false);

        var sceneManager = scope.ServiceProvider.GetRequiredService<ScenesManager>();

        var scene = Scenes.Scenes.CreateActScene(user, targetUser, userRepository, cancellationToken);
        await sceneManager.SetScene(msg, scene);
    }
}