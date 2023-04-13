namespace ProductService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ProductService.Services.Data.Product;
    using ProductService.Services.Data.Vendor;
    using ProductService.Services.DTOModels.VendorModels;

    [Route("api/p/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IVendorService vendorService;

        public ProductController(IProductService productService, IVendorService vendorService)
        {
            this.productService = productService;
            this.vendorService = vendorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await this.productService.GetAll<ProductReadModel>();

            return Ok(products);
        }

        [HttpGet]
        [Route("by-vendors/{vendorId}")]
        public async Task<IActionResult> GetAllByVendor(int vendorId)
        {
            if (!await this.vendorService.Exists(vendorId))
            {
                return NotFound();
            }
            var products = await this.productService.GetAllByVendor<ProductReadModel>(vendorId);

            return Ok(products);
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await this.productService.GetById<ProductReadModel>(id);
            if (product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet]
        [Route("by-vendor/{vendorId}/{productId}")]
        public async Task<IActionResult> GetByIdAndVendor(int vendorId, int productId)
        {
            if (!await this.vendorService.Exists(vendorId))
            {
                return NotFound();
            }

            var product = await this.productService.GetProduct<ProductReadModel>(vendorId, productId);

            if (product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateModel model)
        {
            var product = await this.productService
                .Create<ProductCreateModel, ProductReadModel>(model);

            return CreatedAtRoute(nameof(GetById), new { Id = product.Id }, product);
        }
    }
}
