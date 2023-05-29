using Microsoft.EntityFrameworkCore;
using PizzaTown.Data;
using PizzaTown.Data.Models;

namespace PizzaTown.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var serviceProvider = scopedServices.ServiceProvider;

            var dbContext = serviceProvider
                .GetRequiredService<ApplicationDbContext>();

            MigrateDatabase(dbContext);
            SeedCategories(dbContext);

            return app;
        }

        private static void MigrateDatabase(ApplicationDbContext dbContext)
        {
            dbContext.Database.Migrate();
        }

        private static void SeedCategories(ApplicationDbContext dbContext)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            dbContext.Categories.AddRange(new[]
            {
                new Category {Id = new Guid(), Name = "Pizza"},
                new Category {Id = new Guid(), Name = "Burger"},
                new Category {Id = new Guid(), Name = "Drinks"},
                new Category {Id = new Guid(), Name = "Pasta"},
                new Category {Id = new Guid(), Name = "Desserts"}
            });

            dbContext.SaveChanges();
        }
    }
}