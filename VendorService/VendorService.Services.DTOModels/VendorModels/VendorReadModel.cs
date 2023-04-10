namespace VendorService.Services.DTOModels.VendorModels
{
    using VendorService.Data.Models;
    using VendorService.Services.Mapping;

    public class VendorReadModel : IMapFrom<Vendor>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }
    }
}
