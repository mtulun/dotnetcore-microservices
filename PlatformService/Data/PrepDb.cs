using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;

namespace PlatformService.Data
{
    // This class using for controlling and testing data..
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app,bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                 SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            if (isProd)
            {
                System.Console.WriteLine("--> Attempt to apply migration..");
                try
                {
                     context.Database.Migrate();
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"--> Couldn't run migrations : {ex.Message}");
                }
            }
        // When app starts up i want to some mock data, just so that we have to work with.
            if (!context.Platforms.Any())
            {
                System.Console.WriteLine("--> Seeding Data");

                context.Platforms.AddRange(
                    new Platform(){Name="Dotnet",Publisher="Microsoft",Cost="Free"},
                    new Platform(){Name="Sql Server Express",Publisher="Microsoft",Cost="Free"},
                    new Platform(){Name="Kubernetes",Publisher="Cloud Native Computing Foundation",Cost="Free"}
                );
                context.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("--> We already have data");
            }
        }
    }
}