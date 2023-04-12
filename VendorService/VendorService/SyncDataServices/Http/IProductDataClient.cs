using VendorService.Services.DTOModels.VendorModels;

namespace VendorService.SyncDataServices.Http
{
    public interface IProductDataClient
    {
        Task SendVendorToProduct(VendorReadModel vendor);
    }
}
