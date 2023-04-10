namespace VendorService.Services.DTOModels.VendorModels
{
    using System.ComponentModel.DataAnnotations;

    using VendorService.Data.Models;
    using VendorService.Services.Mapping;

    public class VendorCreateModel : IMapTo<Vendor>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
