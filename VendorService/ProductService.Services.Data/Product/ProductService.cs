

namespace ProductService.Services.Data.Product
{
    using global::ProductService.Data.Common.Repositories;
    using global::ProductService.Services.Mapping;
    using global::ProductService.Data.Models;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class ProductService : IProductService
    {
        private readonly IDeletableEntityRepository<Product> productRepo;

        public ProductService(IDeletableEntityRepository<Product> productRepo)
        {
            this.productRepo = productRepo;
        }

        public async Task<K> Create<T, K>(T model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var entity = model.To<Product>();

            await this.productRepo.AddAsync(entity);
            await this.productRepo.SaveChangesAsync();

            return entity.To<K>();
        }

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            return await this.productRepo
                .All()
                .To<T>()
                .ToListAsync();
        }

        public async Task<T> GetById<T>(int id)
        {
            return await this.productRepo
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .SingleOrDefaultAsync();
        }
    }
}
