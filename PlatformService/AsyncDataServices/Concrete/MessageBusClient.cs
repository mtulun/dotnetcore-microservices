using System;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using PlatformService.AsyncDataServices.Abstract;
using PlatformService.Dtos;
using RabbitMQ.Client;

namespace PlatformService.AsyncDataServices.Concrete
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration configuration;
        private readonly IConnection connection;
        private readonly IModel channel;

        public MessageBusClient(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = configuration["RabbitMQHost"],
                Port = int.Parse(configuration["RabbitMQPort"])
            };
            try
            {
                connection = factory.CreateConnection();
                channel = connection.CreateModel();

                channel.ExchangeDeclare(
                    exchange: "trigger",
                    type: ExchangeType.Fanout
                    );
                connection.ConnectionShutdown += RabbitMQConnectionShutdown;

                System.Console.WriteLine("--> Connected the message bus");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"--> Can't connect to the message bus: {ex.Message}");
            }

        }
        private void RabbitMQConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            System.Console.WriteLine("--> RabbitMQ connection shutdown...");
        }
        public void PublishNewPlatform(PlatformPublishedDto platformPublishedDto)
        {
            var message = JsonSerializer.Serialize(platformPublishedDto);
            
            if (connection.IsOpen)
            {
                System.Console.WriteLine("--> RabbitMQ connected, sending messages...");
                // TODO: Create sendmessage method in here...
            }
            else
            {
                System.Console.WriteLine("--> RabbitMQ not connected...");
            }
        }
    }
}