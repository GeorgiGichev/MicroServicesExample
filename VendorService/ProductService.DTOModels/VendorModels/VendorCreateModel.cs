namespace ProductService.Services.DTOModels.VendorModels
{
    using System.ComponentModel.DataAnnotations;

    using ProductService.Data.Models;
    using ProductService.Services.Mapping;

    public class VendorCreateModel : IMapTo<Vendor>
    {
        [Required]
        public string Name { get; set; }


        [Required]
        public int ExternalId { get; set; }
    }
}
