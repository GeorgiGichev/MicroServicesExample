namespace VendorService.Services.Data.Vendor
{
    using System.Threading.Tasks;

    using VendorService.Services.Mapping;
    using VendorService.Data.Models;
    using VendorService.Data.Common.Repositories;
    using Microsoft.EntityFrameworkCore;

    public class VendoreService : IVendorService
    {
        private readonly IDeletableEntityRepository<Vendor> vendorRepo;

        public VendoreService(IDeletableEntityRepository<Vendor> vendorRepo)
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

            await this.vendorRepo.AddAsync(entity);
            await this.vendorRepo.SaveChangesAsync();

            return entity.To<K>();
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
