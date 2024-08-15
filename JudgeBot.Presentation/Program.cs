using JudgeBot.Infrastructure.Database;
using JudgeBot.Infrastructure.Database.Repositories;
using JudgeBot.Presentation;
using Telegram.Bot;
using TelegramBot.Scenes.Extensions;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<TelegramBotClient>(new TelegramBotClient(builder.Configuration["Telegram:Token"]!));
builder.Services.AddScenes();
builder.Services.AddTransient<ApplicationContext>();
builder.Services.AddTransient<UserRepository>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();