namespace ProductService.Services.DTOModels.VendorModels
{
    using ProductService.Data.Models;
    using ProductService.Services.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class VendorReadModel : IMapFrom<Vendor>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ExternalId { get; set; }
    }
}
