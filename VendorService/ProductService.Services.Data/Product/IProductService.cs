using System.Threading.Tasks;

namespace ProductService.Services.Data.Product
{
    public interface IProductService
    {
        Task<T> GetById<T>(int id);

        Task<IEnumerable<T>> GetAll<T>();

        Task<K> Create<T, K>(T model);

        Task<IEnumerable<T>> GetAllByVendor<T>(int vendorId);

        Task<T> GetProduct<T>(int vendorId, int productId);
    }
}
