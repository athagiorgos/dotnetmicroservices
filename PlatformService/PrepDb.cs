using PlatformService.Data;
using PlatformService.Models;

namespace PlatformService;

public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
    }

    private static void SeedData(AppDbContext? context)
    {
        if (context is not null && !context.Platforms.Any())
        {
            Console.WriteLine("=--> Seeding data...");
            context.Platforms.AddRange(
                new Platform()
                {
                    Name = "DotNet",
                    Publisher = "Microsoft",
                    Cost = "Free"
                },
                new Platform()
                {
                    Name = "SqlServer",
                    Publisher = "Microsoft",
                    Cost = "Free"
                },
                new Platform()
                {
                    Name = "Kubernetes",
                    Publisher = "Cloud Native Computing Foundation",
                    Cost = "Free"
                });

            context.SaveChanges();
            return;
        }
        
        Console.WriteLine("=--> We already have data");
    }
}