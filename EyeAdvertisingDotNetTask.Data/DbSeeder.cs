using EyeAdvertisingDotNetTask.Data.DbEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeAdvertisingDotNetTask.Data
{
    public static class DbSeeder
    {
        public static IHost SeedDb(this IHost webHost)
        {
            using var scope = webHost.Services.CreateScope();
            try
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                try
                {
                    context.Database.Migrate();
                }
                catch
                {
                    // Ignore
                }

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                userManager.SeedAdmin().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return webHost;
        }

        public static async Task SeedAdmin(this UserManager<User> userManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var user = new User
            {
                Email = "admin@test.com",
                UserName = "admin@test.com",
                FirstName = "EyeAdvertising",
                LastName = "Admin",
                IsActive = true,
                PhoneNumberConfirmed = true,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, "User2024$$");
        }

    }
}
