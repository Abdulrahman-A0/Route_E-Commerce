using Domain.Contracts;
using Domain.Entities.Order;
using Domain.Entities.Products;
using OnionAPI.Middlewares;

namespace OnionAPI.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> SeedDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();

            await seeder.SeedDataAsync<ProductBrand>("..\\Infrastructure\\Presistence\\Data\\DataSeed\\brands.json");
            await seeder.SeedDataAsync<ProductType>("..\\Infrastructure\\Presistence\\Data\\DataSeed\\types.json");
            await seeder.SeedDataAsync<Product>("..\\Infrastructure\\Presistence\\Data\\DataSeed\\products.json");
            await seeder.SeedDataAsync<DeliveryMethod>("..\\Infrastructure\\Presistence\\Data\\DataSeed\\delivery.json");
            await seeder.SeedIdentityDataAsync();

            return app;
        }

        public static WebApplication UseExceptionHandlingMiddlewares(this WebApplication app)
        {
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            return app;
        }

        public static WebApplication UseSwaggerMiddlewares(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
