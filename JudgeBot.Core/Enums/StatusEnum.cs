using JudgeBot.Core.Attributes;

namespace JudgeBot.Core.Enums;

/// <summary>
/// TODO: Make t6 generation?
/// </summary>
public enum StatusEnum
{
    [MapGuid("00000000-0000-0000-0000-000000000001")]
    Created = 1,
    [MapGuid("00000000-0000-0000-0000-000000000002")]
    InProgress = 2,
    [MapGuid("00000000-0000-0000-0000-000000000003")]
    Declined = 3,
    [MapGuid("00000000-0000-0000-0000-000000000004")]
    Closed = 4
}