namespace VendorService.Client.AsyncDataServices
{
    using Microsoft.Extensions.Configuration;
    using RabbitMQ.Client;
    using System.Text;
    using System.Text.Json;
    using VendorService.Services.DTOModels.VendorModels;

    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration configuration;
        private readonly IConnection connection;
        private readonly IModel channel;

        public MessageBusClient(IConfiguration configuration)
        {
            this.configuration = configuration;
            var factory = new ConnectionFactory() 
            { 
                HostName = this.configuration["RabbitMQHost"], 
                Port = int.Parse(this.configuration["RabbitMQPort"]) 
            };

            try
            {
                this.connection = factory.CreateConnection();
                this.channel = this.connection.CreateModel();

                this.channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                this.connection.ConnectionShutdown += this.RabbitMQ_ConnectionShutdown;

                Console.WriteLine("--> Connected to MessageBus!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"--> Could not connect to the Message Bus: {e.Message}");
            }
        }

        public void PublishNewVendor(VendorPublishedModel model)
        {
            var message = JsonSerializer.Serialize(model);

            if (this.connection.IsOpen)
            {
                Console.WriteLine("--> RaabitMQ connection is open, sending message...");
                this.SendMessage(message);
            }
            else
            {
                Console.WriteLine("--> RaabitMQ connection is closed, not sending.");
            }
        }

        public void Dispose()
        {
            Console.WriteLine("--> MessageBus disposed.");
            if (this.channel.IsOpen)
            {
                this.channel.Close();
                this.connection.Close();
            }
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> RaabitMQ connection shutdown.");
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            this.channel.BasicPublish(
                exchange: "trigger",
                routingKey: "",
                basicProperties: null,
                body: body);

            Console.WriteLine($"--> We have sent {message}");
        }
    }
}
