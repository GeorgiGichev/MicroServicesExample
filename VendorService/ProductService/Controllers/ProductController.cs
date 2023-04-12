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
        public IActionResult GetAll()
        {
            var products = this.productService.GetAll<ProductReadModel>();

            return Ok(products);
        }

        [HttpGet("{id}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var product = this.productService.GetById<ProductReadModel>(id);
            if (product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create(ProductCreateModel model)
        {
            var product = this.productService
                .Create<ProductCreateModel, ProductReadModel>(model);

            return CreatedAtRoute(nameof(GetById), new { Id = product.Id }, product);
        }
    }
}
