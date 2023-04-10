namespace VendorService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using VendorService.Services.Data.Vendor;
    using VendorService.Services.DTOModels.VendorModels;

    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorService vendorService;

        public VendorController(IVendorService vendorService)
        {
            this.vendorService = vendorService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var vendors = this.vendorService.GetAll<VendorReadModel>();

            return Ok(vendors);
        }

        [HttpGet("{id}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var vendor = this.vendorService.GetById<VendorReadModel>(id);
            if (vendor is null)
            {
                return NotFound();
            }

            return Ok(vendor);
        }

        [HttpPost]
        public IActionResult Create(VendorCreateModel model)
        {
            var vendor = this.vendorService
                .Create<VendorCreateModel, VendorReadModel>(model);

            return CreatedAtRoute(nameof(GetById), new { Id = vendor.Id }, vendor);
        }
    }
}
