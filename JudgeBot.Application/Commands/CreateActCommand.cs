using JudgeBot.Application.Responses;
using MediatR;

namespace JudgeBot.Application.Commands;

public class CreateActCommand : IRequest<CreateActCommandResponse>
{
    /// <summary>
    ///  Имя дела
    /// </summary>
    public string Name { get; init; } = null!;
    
    /// <summary>
    ///  Описание дела
    /// </summary>
    public string Description { get; init; } = null!;
    
    /// <summary>
    ///  ID чата
    /// </summary>
    public long ChatId { get; set; }
    
    /// <summary>
    ///  ID обвиняемого
    /// </summary>
    public long DefendantId { get; set; }
    
    /// <summary>
    ///  ID жертвы
    /// </summary>
    public long? VictimId { get; set; }
    
    /// <summary>
    ///  ID обвинителя
    /// </summary>
    public long AccuserId { get; set; }
    
    /// <summary>
    ///  Кто создает дело
    /// </summary>
    public long CreateUserId { get; set; }
}