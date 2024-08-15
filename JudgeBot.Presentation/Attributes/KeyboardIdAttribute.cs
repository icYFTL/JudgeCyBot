namespace JudgeBot.Presentation.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
public class KeyboardIdAttribute : Attribute
{
    public int Id { get; init; }
}