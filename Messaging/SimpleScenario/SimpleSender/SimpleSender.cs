using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSender
{
    internal class SimpleSender
    {
        private readonly IConfiguration _configuration;
        private readonly ServiceBusClient _client;

        public SimpleSender(
            IConfiguration configuration, 
            ServiceBusClient client)
        {
            _configuration = configuration;
            _client = client;
        }

        public async Task SendMessage(string message)
        {
            ServiceBusSender sender = _client.CreateSender(_configuration.GetValue<string>("QueueName"));
                     
            var sbmessage = new ServiceBusMessage(message)
            {
                Subject = "Test Message"
            };
            
            sbmessage.ApplicationProperties.Add("MessageId", Guid.NewGuid().ToString());
            sbmessage.ApplicationProperties.Add("OrderId", Guid.NewGuid().ToString());
            
            await sender.SendMessageAsync(sbmessage);
            
            await sender.CloseAsync();
            
            Console.WriteLine($"Sent message: {message}");
        }
    }
}
