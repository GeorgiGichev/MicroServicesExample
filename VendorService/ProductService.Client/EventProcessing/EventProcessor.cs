namespace ProductService.Client.EventProcessing
{
    using Microsoft.Extensions.DependencyInjection;
    using ProductService.Client.EventProcessing.Enums;
    using ProductService.Data.Models;
    using ProductService.DTOModels.Event;
    using ProductService.DTOModels.VendorModels;
    using ProductService.Services.Data.Vendor;
    using ProductService.Services.DTOModels.VendorModels;
    using ProductService.Services.Mapping;
    using System.Text.Json;

    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory scopeFactory;

        public EventProcessor(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }
        public async Task ProcessEvent(string message)
        {
            var eventType = this.DetermineEvent(message);

            switch (eventType)
            {
                case EventType.VendorPublished:
                    await this.addVendor(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventModel>(notificationMessage);

            switch (eventType.Event)
            {
                case "Vendor_Published":
                    Console.WriteLine("--> Vendor Published Event Detected");
                    return EventType.VendorPublished;
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

        private async Task addVendor(string vendorPublishedMessage)
        {
            using var scope = scopeFactory.CreateScope();
            var vendorService = scope.ServiceProvider.GetRequiredService<IVendorService>();

            var vendorPublishedDto = JsonSerializer.Deserialize<VendorPublishedModel>(vendorPublishedMessage);
            Console.WriteLine(vendorPublishedDto.ToString());
            var createModel = vendorPublishedDto.To<VendorCreateModel>();
            Console.WriteLine(createModel.ExternalId);

            try
            {
                if (! await vendorService.ExternalVendorExists(createModel.ExternalId))
                {
                    var result = await vendorService.Create<VendorCreateModel, VendorReadModel>(createModel);
                    Console.WriteLine($"--> Vendor {result.Name} is added successfully!");
                }
                else
                {
                    Console.WriteLine("--> Vendor already exists");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"--> Could not add vendor to DB: {e.Message}");
            }
        }
    }
}
