using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Dcode.Caetering.Utils
{
    public class TokenService : ITokenService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string TokenKey = "authToken";

        public TokenService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<string> GetTokenAsync()
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString(TokenKey);
            return Task.FromResult(token ?? string.Empty);
        }

        public Task SetTokenAsync(string token)
        {
            _httpContextAccessor.HttpContext?.Session.SetString(TokenKey, token);
            return Task.CompletedTask;
        }

        public Task RemoveTokenAsync()
        {
            _httpContextAccessor.HttpContext?.Session.Remove(TokenKey);
            return Task.CompletedTask;
        }
    }
}
