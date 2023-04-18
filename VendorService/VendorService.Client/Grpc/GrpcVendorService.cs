using Grpc.Core;
using VendorService.Services.Data.Vendor;
using VendorService.Services.DTOModels.VendorModels;

namespace VendorService.Client.Grpc
{
    public class GrpcVendorService : GrpcVendor.GrpcVendorBase
    {
        private readonly IVendorService vendorService;

        public GrpcVendorService(IVendorService vendorService)
        {
            this.vendorService = vendorService;
        }

        public override async Task<VendorResponse> GetAll(GetAllRequest request, ServerCallContext context)
        {
            var response = new VendorResponse();
            var vendors = await this.vendorService.GetAll<VendorReadModel>();

            foreach (var vendor in vendors)
            {
                var grpcModel = new GrpcVendorModel
                {
                    VendorId = vendor.Id,
                    Name = vendor.Name,
                };

                response.Vendor.Add(grpcModel);
            }

            return response;
        }
    }
}
