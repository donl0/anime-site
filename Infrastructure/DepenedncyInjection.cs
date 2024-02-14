using Application.Interfaces;
using Infrastructure.Client;
using Infrastructure.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DepenedncyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration["DbConnection"];

            services.AddDbContext<IAnimeDbContext, AnimeDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            var shikiUrl = configuration["ShikiUrl"];

            services.AddHttpClient<IAnimeClient, AnimeClient>(client =>
            {
                client.BaseAddress = new System.Uri(shikiUrl);
            });


			return services;
        }
    }
}
