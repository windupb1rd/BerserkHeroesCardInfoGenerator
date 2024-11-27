using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.SQLite
{
    public static class StartupExtensions
    {
        public static IServiceCollection RegisterSQLite(this IServiceCollection services)
        {
            
           // как будто тут другой метод Configure
           //services.Configure<>();

            return services;
        }
    }
}
