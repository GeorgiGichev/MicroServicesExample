using ProductService.Data.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace ProductService.Data.Models
{
    public class Vendor : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int ExternalId { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
