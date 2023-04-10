using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorService.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using VendorService.Data.Common.Models;

    public class Vendor : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Email { get; set; }

    }
}
