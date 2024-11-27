using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Infrastructure.WebApiClient.Options;
using Infrastructure.WebApiClient;

internal class Program
{
    private async static Task Main(string[] args)
    {
        Console.WriteLine("Start building...");

        // тестовый блок
        var c = new WebApiClient("https://berserkdeck.ru/dev/api/cards");
        var t = await c.GetCards();
        // --------------------------

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

                //services.AddTransient<IWebApiClient, WebApiClient>();
            })
            .Build()
            .Run();
    }
}