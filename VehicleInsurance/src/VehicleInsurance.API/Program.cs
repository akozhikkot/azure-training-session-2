using Azure.Messaging.ServiceBus;
using VehicleInsurance.API.Infrastructure;
using VehicleInsurance.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddScoped<IUnderwritingService, UnderwritingService>();
builder.Services.AddSingleton<IMessageSender>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var serviceBusConnectionString =
        configuration.GetConnectionString("MessageBus");
    var serviceBusClient = 
        new ServiceBusClient(serviceBusConnectionString);
    return new MessageSender(serviceBusClient);
});

var app = builder.Build();
 
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();