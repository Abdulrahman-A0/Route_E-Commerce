using Service;
using Service.Implementations;
using ServiceAbstraction.Contracts;
using Shared.Common;

namespace OnionAPI.Extensions
{
    public static class CoreServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(cfg => { }, typeof(AssemblyReference).Assembly);
            services.AddScoped<IServiceManager, ServiceManager>();
            services.Configure<JwtOptions>(configuration.GetSection("JWT"));
            return services;
        }
    }
}
