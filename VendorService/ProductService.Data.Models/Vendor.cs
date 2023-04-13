using ProductService.Data.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace ProductService.Data.Models
{
    public class Vendor : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Email { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
