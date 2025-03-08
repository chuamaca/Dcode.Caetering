using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace Dcode.Caetering.Application.HttpServices
{
    public class AuthHttpService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("CAETERINGAPI");
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private const string TokenKey = "authToken";

        public async Task<Response<AuthDto>> LoginAsync(AuthDto auth)
        {
            var response = await _httpClient.GetFromJsonAsync<Response<AuthDto>>(
                $"api/v1/user/get-by-username-password/{auth.UserName}/{auth.Password}");

            if (response?.Success == true && !response.NoData)
            {
                await SetTokenAsync(response.Data!.Token);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Data.Token);
            }

            return response ?? new Response<AuthDto> { NoData = true, Success = false, Message = "Error de autenticación" };
        }

        public Task Logout()
        {
            RemoveTokenCookie();
            _httpClient.DefaultRequestHeaders.Authorization = null;

            return Task.CompletedTask; // Devuelve un Task para permitir await
        }

        public bool IsAuthenticated()
        {
            var token = GetTokenFromCookie();
            return !string.IsNullOrEmpty(token);
        }
        public Task SetTokenAsync(string token)
        {
            if (_httpContextAccessor.HttpContext == null)
                return Task.CompletedTask; // Evita NullReferenceException

            _httpContextAccessor.HttpContext.Response.Cookies.Append(TokenKey, token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1)
            });

            return Task.CompletedTask;
        }

        //private void SetTokenCookie(string token)
        //{
        //    try
        //    {
        //        var options = new CookieOptions
        //        {
        //            HttpOnly = true,
        //            Secure = true,
        //            SameSite = SameSiteMode.Strict,
        //            Expires = DateTime.UtcNow.AddHours(1)
        //        };
        //        _httpContextAccessor.HttpContext.Response.Cookies.Append(TokenKey, token, options);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);

        //    }

        //}

        private void RemoveTokenCookie()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(TokenKey);
        }

        private string GetTokenFromCookie()
        {
            _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(TokenKey, out var token);
            return token;
        }
    }
}
