namespace ProductService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ProductService.Services.Data.Product;
    using ProductService.Services.DTOModels.VendorModels;

    [Route("api/p/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await this.productService.GetAll<ProductReadModel>();

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

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateModel model)
        {
            var product = await this.productService
                .Create<ProductCreateModel, ProductReadModel>(model);

            return CreatedAtRoute(nameof(GetById), new { Id = product.Id }, product);
        }
    }
}
