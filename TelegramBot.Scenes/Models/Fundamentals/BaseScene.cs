using System.Collections.Concurrent;
using Telegram.Bot.Types.Enums;
using TelegramBot.Scenes.Models.Interfaces;

namespace TelegramBot.Scenes.Models.Fundamentals;

public class BaseScene : IScene
{
    private Func<SceneContext, Task>? _onEnter;
    private Func<SceneContext, Task>? _onLeave;
    internal readonly ConcurrentQueue<Delegate> Queue;

    public readonly string Name;
    public bool IsInitialized { get; protected set; } = false;
    public bool IsDone { get; protected set; } = false;

    public BaseScene(string name)
    {
        Name = name;
        Queue = new ConcurrentQueue<Delegate>();
    }

    public virtual BaseScene OnEnter(Func<SceneContext, Task>? onEnter)
    {
        _onEnter = onEnter;
        return this;
    }

    public virtual BaseScene OnLeave(Func<SceneContext, Task>? onLeave)
    {
        _onLeave = onLeave;
        return this;
    }

    public virtual BaseScene Command(string command, Func<SceneContext, Task<bool>> onCommand)
    {
        async Task<bool> CommandHandler(SceneContext context)
        {
            if (context.Update.Message?.Text == command)
            {
                return await onCommand(context).ConfigureAwait(false);
            }

            return false;
        }

        Queue.Enqueue((Func<SceneContext, Task<bool>>)CommandHandler);
        
        return this;
    }

    public virtual BaseScene Hears(string text, Func<SceneContext, Task<bool>> onHears)
    {
        async Task<bool> CommandHandler((string text, SceneContext context) tuple)
        {
            if (tuple.context.Update.Message?.Text != null && tuple.context.Update.Message.Text.Contains(text))
            {
                return await onHears(tuple.context).ConfigureAwait(false);
            }

            return false;
        }
        
        Queue.Enqueue((Func<(string text, SceneContext Context), Task<bool>>)CommandHandler);
        
        return this;
    }

    public virtual BaseScene On(UpdateType [] types, Func<SceneContext, Task<bool>> onEvent)
    {
        async Task<bool> CommandHandler(SceneContext context)
        {
            if (types.Contains(context.Update.Type))
            {
                return await onEvent(context).ConfigureAwait(false);
            }

            return false;
        }
        
        Queue.Enqueue((Func<SceneContext, Task<bool>>)CommandHandler);
        
        return this;
    }
    public virtual BaseScene On(UpdateType type, Func<SceneContext, Task<bool>> onEvent)
    {
        async Task<bool> CommandHandler(SceneContext context)
        {
            if (context.Update.Type == type)
            {
                return await onEvent(context).ConfigureAwait(false);
            }

            return false;
        }
        
        Queue.Enqueue((Func<SceneContext, Task<bool>>)CommandHandler);
        
        return this;
    }

    internal virtual async Task Leave(SceneContext context)
    {
        if (_onLeave != null)
        {
            await _onLeave.Invoke(context).ConfigureAwait(false);
        }

        IsDone = true;
    }

    internal virtual async Task Enter(SceneContext context)
    {
        if (_onEnter != null)
        {
            await _onEnter.Invoke(context).ConfigureAwait(false);
        }
        IsInitialized = true;
    }
}