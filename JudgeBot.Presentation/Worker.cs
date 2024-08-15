using JudgeBot.Infrastructure.Database.Repositories;
using JudgeBot.Presentation.Logic;
using JudgeBot.Presentation.Resources.Keyboards;
using Telegram.Bot;
using TelegramBot.Scenes.Extensions;
using TelegramBot.Scenes.Logic;

namespace JudgeBot.Presentation;

public class Worker(TelegramBotClient client, IServiceScopeFactory scopeFactory, UserRepository userRepository, ScenesManager scenesManager) : BackgroundService {
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var logic = new TelegramLogic(client, scopeFactory, userRepository);
        logic.CheckHandlers();
        Keyboards.CheckKeyboards();
        
        client.OnMessage += logic.OnMessageAsync;
        client.OnUpdate += logic.OnUpdateAsync;
        client.HandleScenes(scenesManager);
    }
}