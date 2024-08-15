using System.Text.Json.Serialization;

namespace JudgeBot.Presentation.Models;

public class CallbackData
{
    [JsonPropertyName("h")]
    public int HandlerId { get; init; }

    [JsonPropertyName("p")]
    public CallbackDataBackwards? PrevKeyboard { get; set; } = new CallbackDataBackwards
    {
        PrevKeyboardId = 1
    };
    
    [JsonPropertyName("c")]
    public string CustomData { get; init; } = String.Empty; 
}