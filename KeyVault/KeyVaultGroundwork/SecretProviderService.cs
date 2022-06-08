using Azure;
using Azure.Security.KeyVault.Secrets;

namespace KeyVaultGroundwork
{
    public class SecretProviderService
    {
        private readonly SecretClient _secretClient;

        public SecretProviderService(SecretClient secretClient)
        {
            _secretClient = secretClient;
        }

        public async Task<List<SecretInfo>> GetAllSecretsAsync(CancellationToken cancellationToken)
        {
            var secrestsInfo = new List<SecretInfo>();

            AsyncPageable<SecretProperties> secretProperties =
                _secretClient.GetPropertiesOfSecretsAsync(cancellationToken);

            await foreach (SecretProperties properties in secretProperties)
            {
                secrestsInfo.Add(new SecretInfo(
                    properties.Id.ToString(),
                    properties.Version,
                    properties.Name));
            }

            return secrestsInfo;
        }

        public async Task<SecretValue> GetSecretAsync(
            string secretName, CancellationToken cancellationToken)
        {
            KeyVaultSecret secret = 
                await _secretClient.GetSecretAsync(secretName, cancellationToken: cancellationToken);
            return new SecretValue(secret.Id.ToString(), secret.Value);
        }
    }

    public class SecretInfo
    {
        public string Identifier { get; }
        public string Version { get; }
        public string Name { get; }

        public SecretInfo(string identifier, string version, string name)
        {
            Identifier = identifier;
            Version = version;
            Name = name;
        }
    }

    public class SecretValue
    {
        public SecretValue(string identifier, string value)
        {
            Identifier = identifier;
            Value = value;
        }

        public string Identifier { get; }
        public string Value { get; }
    }
}