namespace ProductService.Services.DTOModels.VendorModels
{
    using System.ComponentModel.DataAnnotations;

    using ProductService.Data.Models;
    using ProductService.Services.Mapping;

    public class ProductCreateModel : IMapTo<Product>
    {
        [Required]
        public string Brand { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Volume { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
