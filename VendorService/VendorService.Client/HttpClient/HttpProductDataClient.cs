namespace VendorService.Client.HttpClient
{
    using Microsoft.Extensions.Configuration;
    using System.Text;
    using System.Text.Json;
    
    using VendorService.Services.DTOModels.VendorModels;

    public class HttpProductDataClient : IProductDataClient
    {
        private readonly System.Net.Http.HttpClient httpClient;
        private readonly IConfiguration config;

        public HttpProductDataClient(System.Net.Http.HttpClient httpClient, IConfiguration config)
        {
            this.httpClient = httpClient;
            this.config = config;
        }

        public async Task SendVendorToProduct(VendorReadModel vendor)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(vendor),
                Encoding.UTF8,
                "application/json");

            var response = await this.httpClient.PostAsync(config["ProductApiURL"], httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to ProductApi was OK");
            }
            else
            {
                Console.WriteLine("--> Sync request failed");
            }
        }
    }
}
