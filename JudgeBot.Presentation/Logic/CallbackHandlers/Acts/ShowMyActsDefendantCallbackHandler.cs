using System.Text;
using JudgeBot.Infrastructure.Database.Repositories;
using JudgeBot.Presentation.Logic.CallbackHandlers.Bases;
using JudgeBot.Presentation.Models;
using JudgeBot.Presentation.Models.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using User = JudgeBot.Core.Models.User;

namespace JudgeBot.Presentation.Logic.CallbackHandlers.Acts;

public class ShowMyActsDefendantCallbackHandler(IServiceScope scope) : BasePersonalCallbackHandler, ITelegramCallbackHandler
{
    public int HandlerId => 2;

    protected override async Task ExecuteCoreAsync(Update update, TelegramBotClient client, User user, CallbackData callbackData,
        CancellationToken cancellationToken)
    {
        var actRepository = scope.ServiceProvider.GetRequiredService<ActRepository>();
        var acts = await actRepository.GetUsersActsAsync(update.CallbackQuery!.From.Id, act => act.DefendantId);

        var sb = new StringBuilder(Resources.Presentation.ActListDefendantDescription);
        sb.AppendLine();

        var i = 1;
        foreach (var act in acts)
        {
            sb.AppendLine($"{i}. {act.Name}");
        }
    }
}