using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace VehicleInsurance.Functions
{
    public class BlobItemHandler
    {
        private readonly ILogger _logger;

        public BlobItemHandler(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<BlobItemHandler>();
        }

        [Function("ProcessBlob")]
        public void Run(
            [BlobTrigger("data/{name}", Connection = "BlobConnection")]
            string myBlob, string name)
        {
            _logger.LogInformation("New blob with name added - {name}", name);
            _logger.LogInformation("Blob content - {blobContent}", myBlob);
        }
    }
}