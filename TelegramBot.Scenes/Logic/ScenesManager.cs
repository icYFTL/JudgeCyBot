using System.Collections.Concurrent;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Scenes.Extensions;
using TelegramBot.Scenes.Models.Fundamentals;

namespace TelegramBot.Scenes.Logic;

public class ScenesManager
{
    private ConcurrentDictionary<UserInfo, BaseScene> _scenes = new();
    private readonly TelegramBotClient _client;

    public ScenesManager(TelegramBotClient client)
    {
        _client = client;
    }
    
    public async Task SetScene(Update update, BaseScene scene)
    {
        if (update.Message is not null)
            await SetScene(update.Message, scene).ConfigureAwait(false);

        throw new NullReferenceException("update.Message was null");
    }
    
    public async Task SetScene(Message msg, BaseScene scene)
    {
        _scenes[new UserInfo(msg.From.Id, msg.Chat.Id)] = scene;
        await OnMessageReceivedAsync(msg, UpdateType.Message).ConfigureAwait(false); // Cause we need to start the scene
    }
    
    public void ClearScene(Update update)
    {
        if (update.Message is not null)
            ClearScene(update.Message);
        
        throw new NullReferenceException("update.Message was null");
    }

    public void ClearScene(Message msg)
    {
        _scenes.Remove(new UserInfo(msg.From.Id, msg.Chat.Id), out _);
    }

    internal async Task OnMessageReceivedAsync(Message message, UpdateType type)
    {
        await OnUpdateReceivedAsync(new Update {Message = message}).ConfigureAwait(false); // :D
    }
    
    internal async Task OnUpdateReceivedAsync(Update update)
    {
        var key = update.Type == UpdateType.Message ? update.GetUserInfoFromMessage() : update.GetUserInfoFromCallbackQuery();

        if (!_scenes.TryGetValue(key, out var scene))
        {
            return;
        }

        var context = new SceneContext
        {
            Client = _client,
            Scene = scene,
            Update = update
        };

        if (!scene.IsInitialized)
        {
            await scene.Enter(context).ConfigureAwait(false);
            return;
        }

        Delegate? action;

        scene.Queue.TryPeek(out action);

        if (action is not Func<SceneContext, Task<bool>> func)
        {
            throw new InvalidOperationException("Unsupported delegate type.");
        }

        var result = await func.Invoke(context)!;
        if (result)
        {
            scene.Queue.TryDequeue(out _);
            if (!scene.Queue.Any())
            {
                await scene.Leave(context).ConfigureAwait(false);
                ClearScene(update);
            }
        }
    }
}