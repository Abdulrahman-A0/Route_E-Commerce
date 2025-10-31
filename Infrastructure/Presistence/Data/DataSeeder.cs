using Domain.Contracts;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
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
    public class DataSeeder(AppDbContext context
        , UserManager<User> _userManager
        , RoleManager<IdentityRole> _roleManager) : IDataSeeder
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

        public async Task SeedIdentityDataAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!_userManager.Users.Any())
                {
                    var adminUser = new User
                    {
                        DisplayName = "Admin",
                        UserName = "Admin",
                        Email = "Admin@gmail.com",
                        PhoneNumber = "01024568725"
                    };
                    var superAdminUser = new User
                    {
                        DisplayName = "SuperAdmin",
                        UserName = "SuperAdmin",
                        Email = "SuperAdmin@gmail.com",
                        PhoneNumber = "01004578943"
                    };

                    await _userManager.CreateAsync(adminUser, "P@ssw0rd");
                    await _userManager.CreateAsync(superAdminUser, "P@ssw0rd");

                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                    await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
