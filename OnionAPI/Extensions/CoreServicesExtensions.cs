using Service;
using Service.Implementations;
using ServiceAbstraction.Contracts;

namespace OnionAPI.Extensions
{
    public static class CoreServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { }, typeof(AssemblyReference).Assembly);
            services.AddScoped<IServiceManager, ServiceManager>();
            return services;
        }
    }
}
