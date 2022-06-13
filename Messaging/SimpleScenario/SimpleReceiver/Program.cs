using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddSingleton<SimpleReceiver.SimpleReceiver>((sp) =>
        {
            var connectionString =
                sp.GetRequiredService<IConfiguration>().GetConnectionString("ServiceBusConnectionString");
            var sender = new SimpleReceiver.SimpleReceiver(
                sp.GetRequiredService<IConfiguration>(),
                new Azure.Messaging.ServiceBus.ServiceBusClient(connectionString));
            return sender;
        });
    })
    .Build();
 
var sender = host.Services.GetRequiredService<SimpleReceiver.SimpleReceiver>();
await sender.ReceiveMessages();

Console.ReadLine();