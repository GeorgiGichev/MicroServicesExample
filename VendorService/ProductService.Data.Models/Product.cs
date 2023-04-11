namespace ProductService.Data.Models
{
    using ProductService.Data.Common.Models;

    public class Product : BaseDeletableModel<int>
    {
        public string Brand { get; set; }

        public string Type { get; set; }

        public string Volume { get; set; }

        public decimal Price { get; set; }
    }
}
