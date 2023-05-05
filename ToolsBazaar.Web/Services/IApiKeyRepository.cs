using AspNetCore.Authentication.ApiKey;

namespace ToolsBazaar.Web.Services
{
    public interface IApiKeyRepository
    {
        Task<IApiKey> GetApiKeyAsync(string key);
    }
}
