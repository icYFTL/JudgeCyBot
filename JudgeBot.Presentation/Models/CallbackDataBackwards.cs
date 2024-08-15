using System.Text.Json.Serialization;

namespace JudgeBot.Presentation.Models;

public class CallbackDataBackwards
{
    [JsonPropertyName("i")]
    public int PrevKeyboardId { get; init; }
}