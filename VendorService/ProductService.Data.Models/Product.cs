namespace ProductService.Data.Models
{
    using ProductService.Data.Common.Models;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product : BaseDeletableModel<int>
    {
        [Required]
        public string Brand { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Volume { get; set; }

        [Required]
        public decimal Price { get; set; }

        [ForeignKey("Vendor")]
        public int VendorId { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}
