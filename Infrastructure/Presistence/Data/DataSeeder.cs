using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistence.Data
{
    public class DataSeeder(AppDbContext context) : IDataSeeder
    {
        public async Task SeedDataAsync<T>(string seedingFilePath) where T : class
        {
            if ((await context.Database.GetPendingMigrationsAsync()).Any())
            {
                await context.Database.MigrateAsync();
            }

            var dbSet = context.Set<T>();

            if (!dbSet.Any())
            {
                var jsonFile = File.OpenRead(seedingFilePath);
                var jsonData = await JsonSerializer.DeserializeAsync<IEnumerable<T>>(jsonFile);

                if (jsonData.Any())
                {
                    await dbSet.AddRangeAsync(jsonData);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
