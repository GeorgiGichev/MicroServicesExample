namespace VendorService.Services.Data.Vendor
{
    public interface IVendorService
    {
        T GetById<T>(int id);

        IEnumerable<T> GetAll<T>();

        K Create<T, K>(T model);

    }
}
