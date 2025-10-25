using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using Presistence.Data.Contexts;
using Presistence.Repositories;
using StackExchange.Redis;

namespace OnionAPI.Extensions
{
    public static class InfrastructureServicesExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("cs"));
            });

            services.AddScoped<IDataSeeder, DataSeeder>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection"!));
            });
            services.AddScoped<IBasketRepository, BasketRepository>();
            return services;
        }
    }
}
