using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using TelegramBot.Scenes.Logic;

namespace TelegramBot.Scenes.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddScenes(this IServiceCollection collection)
    {
        if (collection.All(serviceDescriptor => serviceDescriptor.ServiceType != typeof(TelegramBotClient)))
        {
            throw new Exception("TelegramBotClient not found in IServiceCollection, so we can't start");
        }

        collection.AddSingleton<ScenesManager>(); 
    }
}