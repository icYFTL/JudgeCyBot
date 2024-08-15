using StackExchange.Redis;

namespace JudgeBot.Presentation.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConnectionString = configuration.GetConnectionString("Redis")!;
        services.AddTransient<IConnectionMultiplexer>(provider => ConnectionMultiplexer.Connect(redisConnectionString));

        return services;
    }
}