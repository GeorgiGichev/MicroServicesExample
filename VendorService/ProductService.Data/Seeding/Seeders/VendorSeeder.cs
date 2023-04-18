namespace ProductService.Data.Seeding.Seeders
{
    using ProductService.Client.Grpc;

    internal class VendorSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Vendors.Any())
            {
                return;
            }

            var grpcClient = (VendorDataClient)serviceProvider.GetService(typeof(IVendorDataClient));
            var vendors = await grpcClient.ReturnAllVendors();

            if (vendors is not null)
            {
                await dbContext.Vendors.AddRangeAsync(vendors);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
