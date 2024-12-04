﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Infrastructure.WebApiClient.Options;
using Infrastructure.WebApiClient;
using Core.Application.UseCases;
using Infrastructure.SQLite.Options;
using Core.Application.Abstractions;
using Infrastructure.SpreadSheets;
using ConsoleClient;
using Infrastructure.WebApiClient.Abstractions;

internal class Program
{
    private async static Task Main(string[] args)
    {
        Host.CreateDefaultBuilder()
            .ConfigureHostConfiguration(configuration => configuration.AddCommandLine(args))
            .ConfigureAppConfiguration(configBuilder =>
            {
                configBuilder
                    // без этой строки пытается взять файл из папки бинов
                    .SetBasePath("C:\\Users\\alex\\source\\repos\\BerserkHeroesCardInfoGenerator\\BerserkHeroesCardInfoGenerator")
                    .AddJsonFile("appsettings.json");
            })
            .ConfigureServices((hostContext, services) =>
            {
                IConfiguration configuration = hostContext.Configuration;
                services.Configure<WebApiOptions>(configuration.GetRequiredSection("WebApi"));
                services.Configure<SqliteOptions>(configuration.GetRequiredSection("ConnectionStrings"));

                services.AddTransient<IWebApiClient, WebApiClient>();
                services.AddTransient<IContentStringDeserializer, JsonStringDeserializer>();

                services.AddTransient<IContentDownloader>(provider =>
                {
                    IContentDownloader service = new ContentDownloader();
                    service = new ContentDownloaderLogger(service);
                    return new ContentDownloaderRequestResender(service);
                });

                services.AddTransient<ICardsStorageCreator, SpreadSheetStorageCreator>();
                services.AddTransient<SaveCardsUseCase>();

                services.AddHostedService<MainHostedService>();
            })
            .Build()
            .Run();
    }
}