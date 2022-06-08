using Microsoft.AspNetCore.Mvc;

namespace KeyVaultGroundwork.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KeyVaultController : ControllerBase
    {
        private readonly SecretProviderService _secretsProvider;
        private readonly IConfiguration _configuration;
        
        public KeyVaultController(
            SecretProviderService secretsProvider,
            IConfiguration configuration)
        {
            _secretsProvider = secretsProvider;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellation = default)
        {
            var allSecrests = await _secretsProvider.GetAllSecretsAsync(cancellation);
            return Ok(allSecrests);
        }

        [HttpGet("detail")]
        public async Task<IActionResult> GetById([FromQuery] string id, CancellationToken cancellation = default)
        {
            var secretById = await _secretsProvider.GetSecretAsync(id, cancellation);
            return Ok(secretById);
        }

        [HttpGet("config")]
        public IActionResult GetFromConfig()
        {
            var connectionString = _configuration.GetConnectionString("DataStore");
            return Ok(connectionString);
        }
    }
}