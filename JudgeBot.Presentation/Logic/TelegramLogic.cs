using System.Reflection;
using System.Text.Json;
using JudgeBot.Infrastructure.Database.Repositories;
using JudgeBot.Presentation.Attributes;
using JudgeBot.Presentation.Models;
using JudgeBot.Presentation.Models.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using User = JudgeBot.Core.Models.User;

namespace JudgeBot.Presentation.Logic;

public class TelegramLogic(TelegramBotClient client, IServiceScopeFactory scopeFactory, UserRepository userRepository)
{
    private List<Type>? _commandHandlers;
    private List<Type>? _callbackHandlers;
    private List<Type>? _eventHandlers;

    protected List<Type> CommandHandlers
    {
        get
        {
            return _commandHandlers ??= Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.IsClass && x.IsAssignableTo(typeof(ITelegramCommand)))
                .ToList();
        }
    }

    protected List<Type> CallbackHandlers
    {
        get
        {
            return _callbackHandlers ??= Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.IsClass && x.IsAssignableTo(typeof(ITelegramCallbackHandler)))
                .ToList();
        }
    }
    
    protected List<Type> EventHandlers
    {
        get
        {
            return _eventHandlers ??= Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.IsClass && x.IsAssignableTo(typeof(ITelegramEvent)))
                .ToList();
        }
    }

    public async Task OnMessageAsync(Message msg, UpdateType type)
    {
        var scope = scopeFactory.CreateScope();
        var user = await userRepository.GetUserByIdAsync(msg.From!.Id, default).ConfigureAwait(false);
        user ??= await userRepository.AddUserAsync(new User
        {
            Id = msg.From.Id
        }, default).ConfigureAwait(false);

        if (String.IsNullOrEmpty(msg.Text)) return;

        if (msg.Text.StartsWith("/"))
        {
            foreach (var cmd in CommandHandlers)
            {
                var inst = (ITelegramCommand)Activator.CreateInstance(cmd, scope)!;
                if (inst.Command == msg.Text)
                {
                    if (inst.GetType().GetCustomAttribute<PrivateChatOnlyAttribute>() is not null &&
                        msg.Chat.Type != ChatType.Private)
                        return;

                    await inst.ExecuteAsync(msg, client, user, default).ConfigureAwait(false);
                    return;
                }
            }
        }
        else
        {
            foreach (var eventHandler in EventHandlers)
            {
                var inst = (ITelegramEvent)Activator.CreateInstance(eventHandler, scope)!;
                if (inst.Type == UpdateType.Message && type == UpdateType.Message)
                {
                    if (msg.Text.Contains(((ITelegramTextEvent)inst).Text))
                    {
                        if (inst.GetType().GetCustomAttribute<PrivateChatOnlyAttribute>() is not null &&
                            msg.Chat.Type != ChatType.Private)
                            return;
                        
                        await inst.ExecuteAsync(msg, client, user, default).ConfigureAwait(false);
                        return;
                    }
                }
//                 else if (inst.Type == type)
//                 {
//                     // TODO
// #warning Carefully, all events will be executed 
//                     await inst.ExecuteAsync(msg, client, user, default);    
//                 }
            }
        }

        // await client.SendTextMessageAsync(msg.From!.Id, Resources.Presentation.InvalidCommand,
        //     replyParameters: new ReplyParameters
        //     {
        //         ChatId = msg.Chat.Id,
        //         MessageId = msg.MessageId
        //     });
    }

    public async Task OnUpdateAsync(Update update)
    {
        if (update.Type != UpdateType.CallbackQuery) return;

        var callbackData = JsonSerializer.Deserialize<CallbackData>(update.CallbackQuery!.Data!);
        if (callbackData is null)
        {
            await client.AnswerCallbackQueryAsync(update.CallbackQuery!.Id).ConfigureAwait(false);
            return;
        }

        var scope = scopeFactory.CreateScope();

        var user = await userRepository.GetUserByIdAsync(update.CallbackQuery.From.Id, default).ConfigureAwait(false);
        user ??= await userRepository.AddUserAsync(new User
        {
            Id = update.CallbackQuery.From.Id
        }, default).ConfigureAwait(false);

        foreach (var cmd in CallbackHandlers)
        {
            var inst = (ITelegramCallbackHandler)Activator.CreateInstance(cmd, scope)!;

            if (inst.HandlerId == callbackData!.HandlerId)
            {
                await inst.ExecuteAsync(update, client, user, callbackData, default).ConfigureAwait(false);
                await client.AnswerCallbackQueryAsync(update.CallbackQuery!.Id).ConfigureAwait(false);
                return;
            }
        }
    }

    public void CheckHandlers()
    {
        var scope = scopeFactory.CreateScope();
        var handlersIds = new List<int>();

        foreach (var handle in CallbackHandlers)
        {
            var inst = (ITelegramCallbackHandler)Activator.CreateInstance(handle, scope)!;
            if (handlersIds.Contains(inst.HandlerId))
                throw new Exception($"Duplicate callback handler HandlerId at {nameof(inst)}");

            handlersIds.Add(inst.HandlerId);
        }
    }
}