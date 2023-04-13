namespace VendorService.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using VendorService.Client.AsyncDataServices;
    using VendorService.Client.HttpClient;
    using VendorService.Services.Data.Vendor;
    using VendorService.Services.DTOModels.VendorModels;

    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorService vendorService;
        private readonly IProductDataClient productDataClient;
        private readonly IMessageBusClient messageBusClient;

        public VendorController(
            IVendorService vendorService,
            IProductDataClient productDataClient,
            IMessageBusClient messageBusClient)
        {
            this.vendorService = vendorService;
            this.productDataClient = productDataClient;
            this.messageBusClient = messageBusClient;
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
                .Create<VendorCreateModel, VendorPublishedModel>(model);

            //Send Sync Message
            //try
            //{
            //    await this.productDataClient.SendVendorToProduct(vendor);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine($"--> Sending vendors data failed {e.Message}");
            //}

            //Send Async Message
            try
            {
                vendor.Event = "Vendor_Published";
                this.messageBusClient.PublishNewVendor(vendor);
            }
            catch (Exception e)
            {
                Console.WriteLine($"--> Sending asynchronously failed {e.Message}");
            }

            return CreatedAtRoute(nameof(GetById), new { Id = vendor.Id }, vendor);
        }
    }
}
