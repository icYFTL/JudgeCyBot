namespace TelegramBot.Scenes.Models.Fundamentals;

public readonly struct UserInfo(long id, long chatId)
{
    public long Id { get; init; } = id;
    public long ChatId { get; init; } = chatId;
}