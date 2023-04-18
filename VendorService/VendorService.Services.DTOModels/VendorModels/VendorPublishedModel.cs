namespace VendorService.Services.DTOModels.VendorModels
{
    using VendorService.Data.Models;
    using VendorService.Services.Mapping;

    public class VendorPublishedModel : IMapFrom<Vendor>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Event { get; set; }
    }
}
