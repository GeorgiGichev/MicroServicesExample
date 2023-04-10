namespace VendorService.Services.Data.Vendor
{
    using VendorService.Services.Mapping;
    using VendorService.Data.Models;
    using VendorService.Data.Common.Repositories;

    public class VendoreService : IVendorService
    {
        private readonly IDeletableEntityRepository<Vendor> vendorRepo;

        public VendoreService(IDeletableEntityRepository<Vendor> vendorRepo)
        {
            this.vendorRepo = vendorRepo;
        }

        public K Create<T, K>(T model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var entity = model.To<Vendor>();

            this.vendorRepo.AddAsync(entity);
            this.vendorRepo.SaveChanges();

            return entity.To<K>();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.vendorRepo
                .All()
                .To<T>()
                .ToList();
        }

        public T GetById<T>(int id)
        {
            return this.vendorRepo
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .SingleOrDefault();

        }
    }
}
