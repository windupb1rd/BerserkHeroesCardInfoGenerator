using ConsoleClient;
using Core.Application.Abstractions;
using Core.Application.Services;
using Core.Application.UseCases;
using Infrastructure.SpreadSheets;
using Infrastructure.SQLite;
using Infrastructure.SQLite.DbContexts;
using Infrastructure.SQLite.Repositories;
using Infrastructure.TelegramBot;
using Infrastructure.TelegramBot.Abstractions;
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
                services.Configure<TelegramBotOptions>(configuration.GetRequiredSection("TelegramBotClient"));
                services.Configure<VkApplicationClientOptions>(configuration.GetRequiredSection("VkApplicationClient"));

                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlite(configuration.GetConnectionString("SQLite")));

                services.AddHttpClient();
                services.AddTransient<IWebApiClient, WebApiClient>();
                services.AddTransient<IContentStringDeserializer, JsonStringDeserializer>();
                services.AddTransient<IImageUrlComposer, ImageUrlComposer>();
                services.AddTransient<ITermRepository, TermRepository>();
                services.AddTransient<IAuctionPostInfoRepository, AuctionPostInfoRepository>();
                services.AddTransient<IContentDownloader>(provider =>
                {
                    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                    IContentDownloader service = new ContentDownloader(httpClientFactory);
                    service = new ContentDownloaderLogger(service);
                    return new ContentDownloaderRequestResender(service);
                });
                services.AddTransient<ICardsStorageCreator, SpreadSheetStorageCreator>();
                services.AddTransient<ICardsStorageCreator, SqliteStorageCreator>();
                services.AddTransient<SaveCardsUseCase>();
                services.AddSingleton<VkApplicationClient>();
                services.AddSingleton<TelegramBotClient>();

                services.AddHostedService<MainHostedService>();
            })
            .Build()
            .Run();
    }
}