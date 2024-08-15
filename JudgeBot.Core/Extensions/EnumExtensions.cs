using System.Reflection;
using JudgeBot.Core.Attributes;

namespace JudgeBot.Core.Extensions;

public static class EnumExtensions
{
    public static Guid? GetGuid(this Enum value)
    {
         var attribute =  value.GetType().GetField(value.ToString())?.GetCustomAttribute(typeof(MapGuidAttribute));
         return (attribute as MapGuidAttribute)?.Uid;
    }
}