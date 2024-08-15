namespace JudgeBot.Core.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class MapGuidAttribute(string guid) : Attribute
{
    public Guid Uid { get; init; } = new (guid);
}