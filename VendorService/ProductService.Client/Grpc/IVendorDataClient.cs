namespace ProductService.Client.Grpc
{
    using ProductService.Data.Models;

    public interface IVendorDataClient
    {
        Task<IEnumerable<Vendor>> ReturnAllVendors();
    }
}
