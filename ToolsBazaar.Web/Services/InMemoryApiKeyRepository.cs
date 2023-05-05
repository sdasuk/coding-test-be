using AspNetCore.Authentication.ApiKey;

namespace ToolsBazaar.Web.Services
{
    public class InMemoryApiKeyRepository : IApiKeyRepository
    {
        private List<IApiKey> _cache = new List<IApiKey>
        {
            new ApiKey("Key1", "Admin"),
            new ApiKey("Key2", "User"),
        };

        public Task<IApiKey> GetApiKeyAsync(string key)
        {
            var apiKey = _cache.FirstOrDefault(k => k.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(apiKey);
        }
    }
}
