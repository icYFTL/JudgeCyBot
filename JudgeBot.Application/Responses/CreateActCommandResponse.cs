using JudgeBot.Core.Models;

namespace JudgeBot.Application.Responses;

public class CreateActCommandResponse
{
    public Act? Act { get; init; }
    public string? Message { get; init; }
}