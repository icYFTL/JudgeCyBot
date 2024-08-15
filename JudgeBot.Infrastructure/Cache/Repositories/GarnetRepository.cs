using StackExchange.Redis;

namespace JudgeBot.Infrastructure.Cache.Repositories;

public class GarnetRepository(ConnectionMultiplexer connection)
{
    private readonly ConnectionMultiplexer _connection = connection;
    private readonly IDatabase _database = connection.GetDatabase();

    public async Task<bool> HasKeyAsync(string key)
    {
        return await _database.KeyExistsAsync(key);
    }

    public async Task AddKey(string key, string value, TimeSpan? ts = null)
    {
        ts ??= TimeSpan.FromMinutes(15);
        await _database.StringSetAsync(key, value, ts);
    }

    public async Task Delete(string key)
    {
        await _database.KeyDeleteAsync(key);
    }
}