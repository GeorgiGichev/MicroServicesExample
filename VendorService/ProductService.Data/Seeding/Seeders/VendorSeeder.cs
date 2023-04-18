using ProductService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Data.Seeding.Seeders
{
    internal class VendorSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Vendors.Any())
            {
                return;
            }

            var vendors = new List<Vendor>()
            {
                new Vendor
                {
                    Name = "Coca-Cola Helenik Bottling Bulgaria",
                    ExternalId = 1,
                },
                new Vendor
                {
                    Name = "Procter & Gamble Bulgaria",
                    ExternalId = 2,
                },
                new Vendor
                {
                    Name = "Sony Bulgaria",
                    ExternalId = 3,
                },
            };
            await dbContext.Vendors.AddRangeAsync(vendors);
            await dbContext.SaveChangesAsync();
        }
    }
}
