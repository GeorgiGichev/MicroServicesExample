namespace ProductService.Client.Grpc
{
    using global::Grpc.Net.Client;
    using Microsoft.Extensions.Configuration;
    using ProductService.Data.Models;
    using VendorService;

    public class VendorDataClient : IVendorDataClient
    {
        private readonly IConfiguration configuration;

        public VendorDataClient(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IEnumerable<Vendor>> ReturnAllVendors()
        {
            Console.WriteLine($"--> Calling GRPC Service {this.configuration["GrpcVendor"]}");
            var channel = GrpcChannel.ForAddress(this.configuration["GrpcVendor"]);
            var client = new GrpcVendor.GrpcVendorClient(channel);
            var request = new GetAllRequest();

            try
            {
                var vendors = new List<Vendor>();
                var reply = client.GetAll(request);
                foreach (var vendor in reply.Vendor)
                {
                    vendors.Add(new Vendor
                    {
                        ExternalId = vendor.VendorId,
                        Name = vendor.Name,
                    });
                }

                return vendors;
            }
            catch (Exception e)
            {
                Console.WriteLine($"--> Could not call GRPC Server {e.Message}");
                return null;
            }
        }
    }
}
