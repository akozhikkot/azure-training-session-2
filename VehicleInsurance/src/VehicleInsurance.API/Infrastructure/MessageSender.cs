using Azure.Messaging.ServiceBus;
using System.Text.Json;

namespace VehicleInsurance.API.Infrastructure
{
    public class MessageSender : IMessageSender
    {
        private readonly ServiceBusClient _serviceBusClient;
      
        public MessageSender(ServiceBusClient serviceBusClient)
        {
            _serviceBusClient = serviceBusClient;
        }
        
        public async Task Send<T>(T message, string queueName)
        {
            var sender = _serviceBusClient.CreateSender(queueName);
            var messageText = new ServiceBusMessage(
                JsonSerializer.Serialize(message,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                }));
            await sender.SendMessageAsync(messageText);
        }
    }
}
