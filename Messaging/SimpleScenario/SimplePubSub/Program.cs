// See https://aka.ms/new-console-template for more information
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;

var connectionString = "<service-bus connection string>";
var topicName = "<topic Name>";

var serviceBusAdminClient = new ServiceBusAdministrationClient(connectionString);

Console.WriteLine("Enter your name:");
var name = Console.ReadLine();

if (!await serviceBusAdminClient.TopicExistsAsync(topicName))
{
     await serviceBusAdminClient.CreateTopicAsync(topicName);
}

if (!await serviceBusAdminClient.SubscriptionExistsAsync(topicName, name))
{
    var options = new CreateSubscriptionOptions(topicName, name)
    {
        AutoDeleteOnIdle = TimeSpan.FromMinutes(15)        
    };

    SqlRuleFilter filter = new SqlRuleFilter("From != '" + name + "'");
    var ruleOptions = new CreateRuleOptions("excludeSelfMessages", filter);
    await serviceBusAdminClient.CreateSubscriptionAsync(options, ruleOptions);
    
    // await serviceBusAdminClient.CreateSubscriptionAsync(options);
}

var serviceBusClient = new ServiceBusClient(connectionString);
var sender = serviceBusClient.CreateSender(topicName);

var processor = serviceBusClient.CreateProcessor(topicName, name);
processor.ProcessMessageAsync += Processor_ProcessMessageAsync;
processor.ProcessErrorAsync += Processor_ProcessErrorAsync;
await processor.StartProcessingAsync();

var hello = GetMessage($"{name} joined", name);
await sender.SendMessageAsync(hello);

while (true)
{
    var text = Console.ReadLine();
    if (text == "exit")
    {
        break;
    }

    var textMessage = GetMessage($"{name} > {text}", name); 
    await sender.SendMessageAsync(textMessage);
}

var message = GetMessage($"{name} leaving", name); 
await sender.SendMessageAsync(message);

await processor.StopProcessingAsync();
await processor.CloseAsync();
await sender.CloseAsync();

Task Processor_ProcessErrorAsync(ProcessErrorEventArgs arg)
{
    throw new NotImplementedException();
}

static ServiceBusMessage GetMessage(string message, string from)
{
    var serviceBusMessage = new ServiceBusMessage(message);
    serviceBusMessage.ApplicationProperties["From"] = from;
    return serviceBusMessage;
}

static async Task Processor_ProcessMessageAsync(ProcessMessageEventArgs arg)
{
    Console.WriteLine(arg.Message.Body.ToString());
    await arg.CompleteMessageAsync(arg.Message);
}