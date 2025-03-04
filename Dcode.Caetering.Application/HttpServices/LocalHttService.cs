namespace Dcode.Caetering.Application.HttpServices
{
    public class LocalHttService(IHttpClientFactory httpClientFactory)
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("CAETERINGAPI");

        public async Task<Response<IEnumerable<LocalDto>>> GetLocalById(int localId)
        {
            return await ExecuteAsyncUtil.ExecuteAsync(() => GetInternalLocalById(localId));
        }

        public async Task<Response<IEnumerable<LocalDto>>> GetInternalLocalById(int localId)
        {
            var responses = await _httpClient.GetFromJsonAsync<Response<IEnumerable<LocalDto>>>($"api/v1/local/get-by-id/{localId}");
            return responses ?? new Response<IEnumerable<LocalDto>> { NoData = true, Success = false, Message = DATARESPONSE.NO_DATA };
        }

        public async Task<Response<IEnumerable<LocalDto>>> GetLocalAsync()
        {
            return await ExecuteAsyncUtil.ExecuteAsync(() => GetInternalAllLocal());
        }

        public async Task<Response<IEnumerable<LocalDto>>> GetInternalAllLocal()
        {
            try
            {
                var responses = await _httpClient.GetFromJsonAsync<Response<IEnumerable<LocalDto>>>($"api/v1/local/get-all");
                return responses ?? new Response<IEnumerable<LocalDto>> { NoData = true, Success = false, Message = DATARESPONSE.NO_DATA };
            }
            catch (Exception ex)
            {

                return new Response<IEnumerable<LocalDto>>
                {
                    NoData = true,
                    Success = false,
                    Message = ex.Message.ToString()

                };
            }
        }

    }
}
