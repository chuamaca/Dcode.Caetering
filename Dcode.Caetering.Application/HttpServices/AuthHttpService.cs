namespace Dcode.Caetering.Application.HttpServices
{
    public class AuthHttpService(IHttpClientFactory httpClientFactory)
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("CAETERINGAPI");
        public async Task<Response<AuthDto>> LoginAsync(AuthDto authDto)
        {
            return await ExecuteAsyncUtil.ExecuteAsync(() => GetInternalLogin(authDto));
        }

        public async Task<Response<AuthDto>> GetInternalLogin(AuthDto auth)
        {

            var response = await _httpClient.GetFromJsonAsync<Response<AuthDto>>($"api/v1/user/get-by-username-password/{auth.UserName}/{auth.Password}");
            return response ?? new Response<AuthDto> { NoData = true, Success = false, Message = DATARESPONSE.NO_DATA };
        }

    }
}
