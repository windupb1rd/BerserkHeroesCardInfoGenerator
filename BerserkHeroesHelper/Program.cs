using ConsoleClient;
using Core.Application.Abstractions;
using Core.Application.UseCases;
using Infrastructure.SpreadSheets;
using Infrastructure.SQLite;
using Infrastructure.SQLite.DbContexts;
using Infrastructure.TelegramBot;
using Infrastructure.TelegramBot.Abstractions;
using Infrastructure.TelegramBot.Options;
using Infrastructure.WebApiClient;
using Infrastructure.WebApiClient.Abstractions;
using Infrastructure.WebApiClient.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
                    .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
                    .AddJsonFile("appsettings.json");
            })
            .ConfigureServices((hostContext, services) =>
            {
                IConfiguration configuration = hostContext.Configuration;
                services.Configure<WebApiOptions>(configuration.GetRequiredSection("WebApi"));
                services.Configure<TelegramBotOptions>(configuration.GetRequiredSection("TelegramBotClient"));

                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlite(configuration.GetConnectionString("SQLite")));

                services.AddTransient<IWebApiClient, WebApiClient>();
                services.AddTransient<IContentStringDeserializer, JsonStringDeserializer>();
                services.AddTransient<IImageUrlComposer, ImageUrlComposer>();
                services.AddTransient<IContentDownloader>(provider =>
                {
                    IContentDownloader service = new ContentDownloader();
                    service = new ContentDownloaderLogger(service);
                    return new ContentDownloaderRequestResender(service);
                });
                services.AddTransient<ICardsStorageCreator, SpreadSheetStorageCreator>();
                //services.AddTransient<ICardsStorageCreator, SqliteStorageCreator>();
                services.AddTransient<SaveCardsUseCase>();
                services.AddSingleton<TelegramBotClient>();

                services.AddHostedService<MainHostedService>();
            })
            .Build()
            .Run();
    }
}