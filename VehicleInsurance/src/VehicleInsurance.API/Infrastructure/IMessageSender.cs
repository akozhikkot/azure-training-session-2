
namespace VehicleInsurance.API.Infrastructure
{
    public interface IMessageSender
    {
        Task Send<T>(T message, string queueName);
    }
}