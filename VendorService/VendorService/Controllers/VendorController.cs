namespace VendorService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using VendorService.Client.HttpClient;
    using VendorService.Services.Data.Vendor;
    using VendorService.Services.DTOModels.VendorModels;

    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorService vendorService;
        private readonly IProductDataClient productDataClient;

        public VendorController(IVendorService vendorService, IProductDataClient productDataClient)
        {
            this.vendorService = vendorService;
            this.productDataClient = productDataClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vendors = await this.vendorService.GetAll<VendorReadModel>();

            return Ok(vendors);
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var vendor = await this.vendorService.GetById<VendorReadModel>(id);
            if (vendor is null)
            {
                return NotFound();
            }

            return Ok(vendor);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VendorCreateModel model)
        {
            var vendor = await this.vendorService
                .Create<VendorCreateModel, VendorReadModel>(model);

            try
            {
                await this.productDataClient.SendVendorToProduct(vendor);
            }
            catch (Exception e)
            {
                Console.WriteLine($"--> Sending vendors data failed {e.Message}");
            }

            return CreatedAtRoute(nameof(GetById), new { Id = vendor.Id }, vendor);
        }
    }
}
