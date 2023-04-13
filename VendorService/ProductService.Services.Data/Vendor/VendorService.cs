namespace ProductService.Services.Data.Vendor
{
    using global::ProductService.Data.Common.Repositories;
    using global::ProductService.Services.Mapping;
    using global::ProductService.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class VendorService : IVendorService
    {
        private readonly IDeletableEntityRepository<Vendor> vendorRepo;

        public VendorService(IDeletableEntityRepository<Vendor> vendorRepo)
        {
            this.vendorRepo = vendorRepo;
        }

        public async Task<K> Create<T, K>(T model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var entity = model.To<Vendor>();

            await vendorRepo.AddAsync(entity);
            await vendorRepo.SaveChangesAsync();

            return entity.To<K>();
        }

        public async Task<bool> Exists(int id)
        {
            return await this.vendorRepo
                .All()
                .AnyAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            return await this.vendorRepo
                .All()
                .To<T>()
                .ToListAsync();
        }

        public async Task<T> GetById<T>(int id)
        {
            return await this.vendorRepo
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .SingleOrDefaultAsync();

        }
    }
}
