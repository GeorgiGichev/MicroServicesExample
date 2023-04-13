using VendorService.Services.DTOModels.VendorModels;

namespace VendorService.Client.HttpClient
{
    public interface IProductDataClient
    {
        Task SendVendorToProduct(VendorReadModel vendor);
    }
}
