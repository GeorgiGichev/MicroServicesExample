namespace ProductService.Services.Data.Vendor
{
    using global::ProductService.Data.Common.Repositories;
    using global::ProductService.Services.Mapping;
    using global::ProductService.Data.Models;

    public class VendorService : IVendorService
    {
        private readonly IDeletableEntityRepository<Vendor> vendorRepo;

        public VendorService(IDeletableEntityRepository<Vendor> vendorRepo)
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

            vendorRepo.AddAsync(entity);
            vendorRepo.SaveChanges();

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
