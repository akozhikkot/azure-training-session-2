using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleReceiver
{
    internal class SimpleReceiver
    {
        private readonly IConfiguration _configuration;
        private readonly ServiceBusClient _client;

        public SimpleReceiver(
            IConfiguration configuration, 
            ServiceBusClient client)
        {
            _configuration = configuration;
            _client = client;
        }

        public async Task ReceiveMessages()
        {
            var queueName = _configuration.GetValue<string>("QueueName");
            var processor = _client.CreateProcessor(queueName, new ServiceBusProcessorOptions
            {
                AutoCompleteMessages = false,
                MaxConcurrentCalls = 1,
                MaxAutoLockRenewalDuration = TimeSpan.FromMinutes(10)
            });

            processor.ProcessMessageAsync += Processor_ProcessMessageAsync;
            processor.ProcessErrorAsync += Processor_ProcessErrorAsync;

            await processor.StartProcessingAsync();

            //var receiver = _client.CreateReceiver();

            //while (true)
            //{
            //    var message = await receiver.ReceiveMessageAsync();

            //    if (message != null)
            //    {
            //        Console.WriteLine($"Received message: {message.Body}");
            //        await receiver.CompleteMessageAsync(message);
            //    }
            //    else
            //    {
            //        Console.WriteLine("All messages received");
            //        break;
            //    }
            //} 

            //await receiver.CloseAsync();
        }

        private Task Processor_ProcessErrorAsync(ProcessErrorEventArgs arg)
        {
            throw new NotImplementedException();
        }

        private async Task Processor_ProcessMessageAsync(ProcessMessageEventArgs arg)
        {
            Console.WriteLine("Received : {0}", arg.Message.Body.ToString());

            await arg.CompleteMessageAsync(arg.Message);
            //await arg.DeferMessageAsync(arg.Message);
            //await arg.DeadLetterMessageAsync(arg.Message);
            //await arg.AbandonMessageAsync(arg.Message);
            
        }
    }
}
