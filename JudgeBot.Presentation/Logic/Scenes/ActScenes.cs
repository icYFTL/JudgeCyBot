using System.Text;
using JudgeBot.Infrastructure.Database.Repositories;
using JudgeBot.Presentation.Logic.Scenes.Types;
using JudgeBot.Presentation.Resources.Keyboards;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Enums;
using TelegramBot.Scenes.Models.Fundamentals;
using User = JudgeBot.Core.Models.User;

namespace JudgeBot.Presentation.Logic.Scenes;

public static partial class Scenes
{
    public static BaseScene CreateActScene(User user, User targetUser, UserRepository userRepository, CancellationToken cancellationToken)
    {
        Utils.Utils.SetUserCulture(user.LanguageId);

        var scene = new ActScene("CreateActScene");
        scene
            .OnEnter(async (ctx) =>
            {
                var msg = await ctx.Client.SendTextMessageAsync(ctx.Update.Message.Chat.Id,
                        Resources.Presentation.NewActSceneSetActName, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);
                ((ActScene)ctx.Scene).MessageId = msg.MessageId;
                ((ActScene)ctx.Scene).Act.DefendantId = targetUser.Id;
                ((ActScene)ctx.Scene).Act.AccuserId = user.Id;
            })
            .On(UpdateType.Message, async (ctx) =>
            {
                if (ctx.Update!.Message!.Text!.Length == 0)
                {
                    await ctx.Client.EditMessageTextAsync(ctx.Update.Message.Chat.Id, scene.MessageId,
                            Resources.Presentation.InvalidActName, cancellationToken: cancellationToken)
                        .ConfigureAwait(false);
                    return false;
                }

                ((ActScene)ctx.Scene).Act.Name = ctx.Update.Message.Text;
                try
                {
                    await ctx.Client.DeleteMessageAsync(ctx.Update.Message.Chat.Id, ctx.Update.Message.MessageId,
                        cancellationToken);
                }
                catch (ApiRequestException)
                {
                    // Don't have any rights... Ok
                }

                await ctx.Client.EditMessageTextAsync(ctx.Update.Message.Chat.Id, scene.MessageId,
                        Resources.Presentation.NewActSceneSetActDescription, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                return true;
            })
            .On(UpdateType.Message, async (ctx) =>
                {
                    if (ctx.Update!.Message!.Text!.Length == 0)
                    {
                        await ctx.Client.EditMessageTextAsync(ctx.Update.Message.Chat.Id, scene.MessageId,
                                Resources.Presentation.InvalidActDescription, cancellationToken: cancellationToken)
                            .ConfigureAwait(false);
                        return false;
                    }

                    ((ActScene)ctx.Scene).Act.Description = ctx.Update.Message.Text;
                    try
                    {
                        await ctx.Client.DeleteMessageAsync(ctx.Update.Message.Chat.Id, ctx.Update.Message.MessageId,
                            cancellationToken);
                    }
                    catch (ApiRequestException)
                    {
                        // Don't have any rights... Ok
                    }

                    var keyboard = Keyboards.NewActVictimSelectorKeyboard(ctx.Update, null, user.LanguageId);

                    await ctx.Client.EditMessageTextAsync(ctx.Update.Message.Chat.Id, scene.MessageId,
                            keyboard.MessageDescription, replyMarkup: keyboard.Keyboard,
                            cancellationToken: cancellationToken)
                        .ConfigureAwait(false);
                    return true;
                }
            ).On([UpdateType.Message, UpdateType.CallbackQuery], async (ctx) =>
            {
                if (ctx.Update.Type == UpdateType.CallbackQuery)
                {
                    if (ctx.Update.CallbackQuery!.Data == "me")
                        ((ActScene)ctx.Scene).Act.VictimId = ctx.Update.CallbackQuery.From.Id;

                    await ctx.Client
                        .AnswerCallbackQueryAsync(ctx.Update.CallbackQuery.Id, cancellationToken: cancellationToken)
                        .ConfigureAwait(false);
                }
                else if (ctx.Update.Message is not null)
                {
                    var keyboard = Keyboards.NewActVictimSelectorKeyboard(ctx.Update, null, user.LanguageId);
                    
                    if (ctx.Update.Message.Entities is not null)
                    {
                        var entity = ctx.Update.Message.Entities.FirstOrDefault();
                        
                        if (entity is not null && entity.Type == MessageEntityType.TextMention)
                        {
                            var localEntity = await userRepository.GetUserByIdAsync(entity.User!.Id, cancellationToken);
                            localEntity ??= await userRepository.AddUserAsync(new User
                            {
                                Id = entity.User!.Id
                            }, cancellationToken);
                            
                            ((ActScene)ctx.Scene).Act.VictimId = localEntity.Id;
                        }
                    }
                    else
                    {
                        await ctx.Client.EditMessageTextAsync(ctx.Update.Message!.Chat.Id, scene.MessageId,
                                Resources.Presentation.NewActSceneSetActVictimInvalidMention,
                                replyMarkup: keyboard.Keyboard,
                                cancellationToken: cancellationToken)
                            .ConfigureAwait(false);
                    }
                }

                var yesNoKeyboard = Keyboards.YesNoKeyboard(ctx.Update, null, user.LanguageId);

                var sb = new StringBuilder(Utils.Utils.SanitizeMarkdown(((ActScene)ctx.Scene).Act.Name));
                sb.AppendLine();
                sb.AppendLine(Utils.Utils.SanitizeMarkdown(((ActScene)ctx.Scene).Act.Description));
                sb.AppendLine();
                
                sb.AppendLine($"[{Resources.Presentation.AccuserBtnLbl}](tg://user?id={((ActScene)ctx.Scene).Act.AccuserId})");
                sb.AppendLine($"[{Resources.Presentation.DefendantBtnLbl}](tg://user?id={((ActScene)ctx.Scene).Act.DefendantId})");
                if (((ActScene)ctx.Scene).Act.VictimId is null)
                    sb.AppendLine($"{Resources.Presentation.VictimBtnLbl}: {Resources.Presentation.Absent}");
                else
                    sb.AppendLine($"[Жертва](tg://user?id={((ActScene)ctx.Scene).Act.VictimId})");
                sb.AppendLine($"{Resources.Presentation.MagistrateBtnLbl}: {Resources.Presentation.WillBeAssigned}");
                sb.AppendLine();
                sb.AppendLine(Resources.Presentation.NewActSceneSetActApproveAct);

                var chatId = ctx.Update.CallbackQuery?.Message?.Chat.Id ?? ctx.Update!.Message!.Chat.Id;
                
                await ctx.Client.EditMessageTextAsync(chatId, scene.MessageId,
                        sb.ToString(),
                        replyMarkup: yesNoKeyboard.Keyboard,
                        parseMode: ParseMode.MarkdownV2,
                        cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                return true;
            }).On(UpdateType.CallbackQuery, async(ctx) =>
            {
                if (ctx.Update.CallbackQuery!.Data == "1")
                {
                    await ctx.Client.EditMessageTextAsync(ctx.Update.CallbackQuery.Message!.Chat.Id, scene.MessageId,
                            String.Format(Presentation.Resources.Presentation.ActSuccessfullyCreated, Guid.NewGuid()),
                            cancellationToken: cancellationToken)
                        .ConfigureAwait(false);
                    
                    // TODO: Save to database
                    
                    return true;
                } 

                await ctx.Client.EditMessageTextAsync(ctx.Update.CallbackQuery.Message!.Chat.Id, scene.MessageId,
                        String.Format(Presentation.Resources.Presentation.ActDoesNotCreated, Guid.NewGuid()),
                        cancellationToken: cancellationToken)
                    .ConfigureAwait(false);
                
                return true;
            });

        return scene;
    }
}