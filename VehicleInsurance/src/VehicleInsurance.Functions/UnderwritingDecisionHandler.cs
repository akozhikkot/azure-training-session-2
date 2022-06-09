using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace VehicleInsurance.Functions
{
    public class UnderwritingDecisionHandler
    {
        private readonly ILogger _logger;

        public UnderwritingDecisionHandler(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<UnderwritingDecisionHandler>();
        }

        [Function("ProcessUnderwritingDecision")]
        [BlobOutput("requests/{rand-guid}-output.json", Connection = "BlobConnection")]
        public string Run(
            [ServiceBusTrigger(
            "%UnderwritingDecisionQueue%",
            Connection = "MessageBus")] string myQueueItem)
        {
            _logger.LogInformation("Received message -: {message}", myQueueItem);
            return myQueueItem;
        }
    }
}