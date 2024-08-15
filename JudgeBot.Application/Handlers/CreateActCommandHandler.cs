using JudgeBot.Application.Commands;
using JudgeBot.Application.Localization;
using JudgeBot.Application.Responses;
using JudgeBot.Application.Utils;
using JudgeBot.Core.Enums;
using JudgeBot.Core.Extensions;
using JudgeBot.Core.Models;
using JudgeBot.Infrastructure.Database.Repositories;
using MediatR;

namespace JudgeBot.Application.Handlers;

public class CreateActCommandHandler (ActRepository actRepository, UserRepository userRepository) : IRequestHandler<CreateActCommand, CreateActCommandResponse>
{
    public async Task<CreateActCommandResponse> Handle(CreateActCommand request, CancellationToken cancellationToken)
    {
        if (request.Name.Length is > 50 or < 5)
        {
            return new CreateActCommandResponse { Message = String.Format(JudgeBotResources.InvalidLengthGeneric, nameof(request.Name), 5, 50) };
        }

        if (TextUtils.IsAlphaNumericSpecialString(request.Name))
        {
            return new CreateActCommandResponse
                { Message = String.Format(JudgeBotResources.BadStringContent, nameof(request.Name)) };
        }

        if (request.Description.Length > 3000)
        {
            return new CreateActCommandResponse { Message = String.Format(JudgeBotResources.TooLongGeneric, nameof(request.Description), 3001) };
        }
        
        if (TextUtils.IsAlphaNumericSpecialString(request.Description))
        {
            return new CreateActCommandResponse
                { Message = String.Format(JudgeBotResources.BadStringContent, nameof(request.Description)) };
        }

        if (request.AccuserId == request.DefendantId)
        {
            return new CreateActCommandResponse { Message = JudgeBotResources.AccuserCantBeDefendant };
        }

        if (request.VictimId == request.DefendantId)
        {
            return new CreateActCommandResponse { Message = JudgeBotResources.VictimCantBeDefendant };
        }

        var magistrateId = await userRepository.AllocMagistrateAsync(cancellationToken);
        if (magistrateId is null)
        {
            return new CreateActCommandResponse { Message = JudgeBotResources.CantAllocMagistrate };
        }

        var act = new Act
        {
            Name = request.Name,
            Description = request.Description,
            ChatId = request.ChatId,
            DefendantId = request.DefendantId,
            VictimId = request.VictimId,
            AccuserId = request.AccuserId,
            StatusUid = StatusEnum.Created.GetGuid()!.Value,
            CreatedAt = DateTime.UtcNow,
            EditedAt = DateTime.UtcNow,
            CreateUserId = request.CreateUserId,
            EditUserId = request.CreateUserId,
            MagistrateId = magistrateId
        };

        await actRepository.CreateActAsync(act, cancellationToken);

        return new CreateActCommandResponse {Act = act};
    }
}