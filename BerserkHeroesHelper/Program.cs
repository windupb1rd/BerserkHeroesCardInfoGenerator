using ConsoleClient;
using Core.Application.Abstractions;
using Core.Application.Services;
using Core.Application.UseCases;
using DocumentFormat.OpenXml.Wordprocessing;
using Infrastructure.SpreadSheets;
using Infrastructure.SQLite;
using Infrastructure.SQLite.DbContexts;
using Infrastructure.SQLite.Repositories;
using Infrastructure.TelegramBot;
using Infrastructure.TelegramBot.Abstractions;
using Infrastructure.TelegramBot.Handlers;
using Infrastructure.TelegramBot.Options;
using Infrastructure.Vk;
using Infrastructure.Vk.Abstractions;
using Infrastructure.Vk.Options;
using Infrastructure.WebApiClient;
using Infrastructure.WebApiClient.Abstractions;
using Infrastructure.WebApiClient.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Reflection;
using Telegram.Bot;

internal class Program
{
    private async static Task Main(string[] args)
    {
        Host.CreateDefaultBuilder()
            .ConfigureHostConfiguration(configuration => configuration.AddCommandLine(args))
            .ConfigureAppConfiguration(configBuilder =>
            {
                configBuilder
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile("appsettings.local.json", optional: true);
                    
            })
            .ConfigureServices((hostContext, services) =>
            {
                IConfiguration configuration = hostContext.Configuration;
                services.Configure<WebApiOptions>(configuration.GetRequiredSection("WebApi"));
                services.Configure<TelegramBotOptions>(configuration.GetRequiredSection("TelegramBotService"));
                services.Configure<VkApplicationClientOptions>(configuration.GetRequiredSection("VkApplicationClient"));

                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlite(configuration.GetConnectionString("SQLite")));

                services.AddHttpClient();
                services.AddTransient<IWebApiClient, WebApiClient>();
                services.AddTransient<IContentStringDeserializer, JsonStringDeserializer>();
                services.AddTransient<IImageUrlComposer, ImageUrlComposer>();
                services.AddTransient<ITermRepository, TermRepository>();
                services.AddTransient<IAuctionPostInfoRepository, AuctionPostInfoRepository>();
                services.AddTransient<IContentDownloader>(serviceProvider =>
                {
                    var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
                    IContentDownloader service = new ContentDownloader(httpClientFactory.CreateClient());
                    service = new ContentDownloaderLogger(service);
                    return new ContentDownloaderRequestResender(service);
                });
                services.AddTransient<IContentDownloaderProvider>(serviceProvider =>
                {
                    var func = () => serviceProvider.GetRequiredService<IContentDownloader>();
                    var service = new ContentDownloaderProvider(func);
                    return service;
                });
                services.AddTransient<ICardsStorageCreator, SpreadSheetStorageCreator>();
                services.AddTransient<ICardsStorageCreator, SqliteStorageCreator>();
                services.AddTransient<CardsInfoUpdater>();
                services.AddTransient<SaveCardsUseCase>();
                services.AddTransient<IBotClientMessenger, BotClientMessenger>();
                services.AddSingleton<VkApplicationClient>();
                services.AddSingleton<OnMessageHandler>();
                services.AddSingleton<OnUpdateHandler>();
                services.AddSingleton<OnErrorHandler>();
                services.AddSingleton<TelegramBotClient>(serviceProvider =>
                {
                    using var cts = new CancellationTokenSource();
                    var options = serviceProvider.GetRequiredService<IOptions<TelegramBotOptions>>().Value;
                    var bot = new TelegramBotClient(options.Token, cancellationToken: cts.Token);
                    return bot;
                });
                services.AddSingleton<TelegramBotService>();
                services.AddHostedService<MainHostedService>();
            })
            .Build()
            .Run();
    }
}