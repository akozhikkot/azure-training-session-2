using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;

var connectionString = "<service-bus connection string>";
var requestQueue = "<requestQueueName>";
var responseQueue = "<responseQueueName>";

var serviceBusClient = new ServiceBusClient(connectionString);

var sender = serviceBusClient.CreateSender(requestQueue);

var responseSessionId = Guid.NewGuid().ToString();

var requestMessage = new ServiceBusMessage("This is a sample message");
requestMessage.ReplyToSessionId = responseSessionId;

await sender.SendMessageAsync(requestMessage);

var responseSession = await serviceBusClient.AcceptSessionAsync(
    responseQueue, responseSessionId);

var responseMessage = await responseSession.ReceiveMessageAsync();

if(responseMessage != null)
{
    Console.WriteLine("Received Response - {0}", responseMessage.Body.ToString());

    await responseSession.CloseAsync();
}

Console.ReadLine();