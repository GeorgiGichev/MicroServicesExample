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
                    Address = "Str. Racho Petkov 8, Sofia 1766, Bulgaria",
                    Email= "pacbg@cchellenic.com",
                },
                new Vendor
                {
                    Name = "Procter & Gamble Bulgaria",
                    Address = "Str. Bulevard Bulgaria 69, Sofia 1404, Bulgaria",
                    Email= "pg.reception@kavenorbico.bg",
                },
                new Vendor
                {
                    Name = "Sony Bulgaria",
                    Address = "Str. Bussines park Sofia, Sofia 1766, Bulgaria",
                    Email= "sonybg@sony.com",
                },
            };
            await dbContext.Vendors.AddRangeAsync(vendors);
            await dbContext.SaveChangesAsync();
        }
    }
}
