namespace ProductService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ProductService.Services.Data.Product;
    using ProductService.Services.Data.Vendor;
    using ProductService.Services.DTOModels.VendorModels;

    [Route("api/p/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorService vendorService;

        public VendorController(IVendorService vendorService)
        {
            this.vendorService = vendorService;
        }

        [HttpGet]
        public IActionResult GetAllVendors()
        {
            var vendors = this.vendorService.GetAll<VendorReadModel>();

            return Ok(vendors);
        }

        [HttpGet("{id}", Name = "GetVendorById")]
        public IActionResult GetVendorById(int id)
        {
            var vendor = this.vendorService.GetById<VendorReadModel>(id);
            if (vendor is null)
            {
                return NotFound();
            }

            return Ok(vendor);
        }

        [HttpPost]
        public IActionResult CreateVendor(VendorCreateModel model)
        {
            var vendor = this.vendorService
                .Create<VendorCreateModel, VendorReadModel>(model);

            return CreatedAtRoute(nameof(GetVendorById), new { Id = vendor.Id }, vendor);
        }
    }
}
