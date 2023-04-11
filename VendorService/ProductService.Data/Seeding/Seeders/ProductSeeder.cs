namespace ProductService.Data.Seeding.Seeders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ProductService.Data.Models;

    internal class ProductSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Vendors.Any())
            {
                return;
            }

            var vendors = new List<Product>()
            {
                new Product
                {
                    Brand = "Coca-Cola",
                    Type = "soft-drink",
                    Volume = "2L",
                    Price= 0.5M,
                },
                new Product
                {
                    Brand = "Gillette",
                    Type = "safety razor",
                    Volume = "1 piece",
                    Price= 10,
                },
                new Product
                {
                    Brand = "Sony PlayStation",
                    Type = "Console PlayStation 5",
                    Volume = "1 piece",
                    Price= 1099,
                },
            };
            await dbContext.Vendors.AddRangeAsync(vendors);
            await dbContext.SaveChangesAsync();
        }
    }
}
