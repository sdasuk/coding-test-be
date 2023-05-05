using AspNetCore.Authentication.ApiKey;

namespace ToolsBazaar.Web.Services
{
    public class ApiKeyProvider : IApiKeyProvider
    {
        private readonly ILogger<IApiKeyProvider> _logger;
        private readonly IApiKeyRepository _apiKeyRepository;

        public ApiKeyProvider(ILogger<IApiKeyProvider> logger, IApiKeyRepository apiKeyRepository)
        {
            _logger = logger;
            _apiKeyRepository = apiKeyRepository;
        }

        public async Task<IApiKey> ProvideAsync(string key)
        {
            try
            {
                return null;
            }
            catch (System.Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw;
            }
        }
    }
}
