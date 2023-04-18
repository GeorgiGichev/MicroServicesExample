namespace ProductService.DTOModels.VendorModels
{
    using AutoMapper;
    using ProductService.Data.Models;
    using ProductService.Services.DTOModels.VendorModels;
    using ProductService.Services.Mapping;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class VendorPublishedModel : IMapTo<VendorCreateModel>, IHaveCustomMappings
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Event { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<VendorPublishedModel, VendorCreateModel>()
                .ForMember(dest => dest.ExternalId, opt =>
                    opt.MapFrom(src => src.Id));
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Id: {this.Id}")
              .AppendLine($"Name: {this.Name}")
              .AppendLine($"Event: {this.Event}");
            return sb.ToString();
        }
    }
}
