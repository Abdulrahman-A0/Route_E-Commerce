using Domain.Contracts;
using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using Presistence.Data.Contexts;
using Presistence.Repositories;
using Service;
using Service.Implementations;
using ServiceAbstraction.Contracts;

namespace OnionAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
            });

            builder.Services.AddScoped<IDataSeeder, DataSeeder>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(cfg => { }, typeof(AssemblyReference).Assembly);
            builder.Services.AddScoped<IServiceManager, ServiceManager>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();

                await seeder.SeedDataAsync<ProductBrand>("..\\Infrastructure\\Presistence\\Data\\DataSeed\\brands.json");
                await seeder.SeedDataAsync<ProductType>("..\\Infrastructure\\Presistence\\Data\\DataSeed\\types.json");
                await seeder.SeedDataAsync<Product>("..\\Infrastructure\\Presistence\\Data\\DataSeed\\products.json");
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
