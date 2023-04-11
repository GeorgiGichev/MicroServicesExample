

namespace ProductService.Services.Data.Product
{
    using global::ProductService.Data.Common.Repositories;
    using global::ProductService.Services.Mapping;
    using global::ProductService.Data.Models;

    public class ProductService : IProductService
    {
        private readonly IDeletableEntityRepository<Product> productRepo;

        public ProductService(IDeletableEntityRepository<Product> productRepo)
        {
            this.productRepo = productRepo;
        }

        public K Create<T, K>(T model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var entity = model.To<Product>();

            this.productRepo.AddAsync(entity);
            this.productRepo.SaveChanges();

            return entity.To<K>();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.productRepo
                .All()
                .To<T>()
                .ToList();
        }

        public T GetById<T>(int id)
        {
            return this.productRepo
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .SingleOrDefault();

        }
    }
}
