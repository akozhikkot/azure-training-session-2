using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddSingleton<SimpleSender.SimpleSender>((sp) =>
        {
            var connectionString = 
                sp.GetRequiredService<IConfiguration>().GetConnectionString("ServiceBusConnectionString");
            var sender = new SimpleSender.SimpleSender(
                sp.GetRequiredService<IConfiguration>(),
                new Azure.Messaging.ServiceBus.ServiceBusClient(connectionString));
            return sender;
        });
    })
    .Build();

Console.WriteLine("Message to send");
var message = Console.ReadLine() ?? "No Input";
var sender = host.Services.GetRequiredService<SimpleSender.SimpleSender>();
await sender.SendMessage(message);
Console.ReadLine();