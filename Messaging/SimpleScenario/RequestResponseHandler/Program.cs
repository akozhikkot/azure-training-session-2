using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;

var connectionString = "<service-bus connection string>";
var requestQueue = "<requestQueueName>";
var responseQueue = "<responseQueueName>";

var client = new ServiceBusClient(connectionString);
var sender = client.CreateSender(responseQueue);
var processor = client.CreateProcessor(requestQueue, new ServiceBusProcessorOptions
{
    AutoCompleteMessages = false,
    MaxConcurrentCalls = 1,
    MaxAutoLockRenewalDuration = TimeSpan.FromMinutes(10)
});

processor.ProcessMessageAsync += Processor_ProcessMessageAsync;
processor.ProcessErrorAsync += Processor_ProcessErrorAsync;

await processor.StartProcessingAsync();

Console.ReadLine();

async Task Processor_ProcessErrorAsync(ProcessErrorEventArgs arg)
{ 
}

async Task Processor_ProcessMessageAsync(ProcessMessageEventArgs arg)
{
    Console.WriteLine("Processing Message : {0}", arg.Message.Body.ToString());

    var responseMessage = new ServiceBusMessage($"Response for - {arg.Message.Body.ToString()}");
    responseMessage.SessionId = arg.Message.ReplyToSessionId;

    await sender.SendMessageAsync(responseMessage);

    await arg.CompleteMessageAsync(arg.Message);
}