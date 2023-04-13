using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorService.Data.Models;
using VendorService.Services.Mapping;

namespace VendorService.Services.DTOModels.VendorModels
{
    public class VendorPublishedModel : IMapFrom<Vendor>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Event { get; set; }
    }
}
