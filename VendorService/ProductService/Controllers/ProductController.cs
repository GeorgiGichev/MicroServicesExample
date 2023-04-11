namespace ProductService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ProductService.Services.Data.Product;
    using ProductService.Services.DTOModels.VendorModels;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService vendorService;

        public ProductController(IProductService vendorService)
        {
            this.vendorService = vendorService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var vendors = this.vendorService.GetAll<ProductReadModel>();

            return Ok(vendors);
        }

        [HttpGet("{id}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var vendor = this.vendorService.GetById<ProductReadModel>(id);
            if (vendor is null)
            {
                return NotFound();
            }

            return Ok(vendor);
        }

        [HttpPost]
        public IActionResult Create(ProductCreateModel model)
        {
            var vendor = this.vendorService
                .Create<ProductCreateModel, ProductReadModel>(model);

            return CreatedAtRoute(nameof(GetById), new { Id = vendor.Id }, vendor);
        }
    }
}
