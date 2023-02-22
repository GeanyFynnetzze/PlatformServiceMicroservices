using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if (!context.Platforms.Any())
            {
                Console.WriteLine("No DATA");

                context.Platforms.AddRange(
                    new Platform() { Name = "DOT NET", Publisher = "Microsoft", Price = 0 },
                    new Platform() { Name = "SQL SERVER EXPRESS", Publisher = "Microsoft", Price = 50 },
                    new Platform() { Name = "Kubernetes", Publisher = "Cloud Native Foundation", Price = 100 }
                    );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("THERE IS DATA");
            }
        }
    }
}
