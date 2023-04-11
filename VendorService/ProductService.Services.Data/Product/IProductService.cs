namespace ProductService.Services.Data.Product
{
    public interface IProductService
    {
        T GetById<T>(int id);

        IEnumerable<T> GetAll<T>();

        K Create<T, K>(T model);

    }
}
