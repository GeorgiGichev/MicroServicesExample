namespace ProductService.Services.DTOModels.VendorModels
{
    using ProductService.Data.Models;
    using ProductService.Services.Mapping;

    public class ProductReadModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Type { get; set; }

        public string Volume { get; set; }

        public decimal Price { get; set; }

        public int VendorId { get; set; }
    }
}
