namespace Dcode.Caetering.Application.HttpServices
{
    public class EventoHttpService(IHttpClientFactory httpClientFactory)
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("CAETERINGAPI");

        public async Task<Response<IEnumerable<Evento>>> GetEventoById(int eventoId)
        {
            return await ExecuteAsyncUtil.ExecuteAsync(() => GetInternalEventoById(eventoId));
        }

        private async Task<Response<IEnumerable<Evento>>> GetInternalEventoById(int eventoId)
        {
            var responses = await _httpClient.GetFromJsonAsync<Response<IEnumerable<Evento>>>($"api/Evento/get-by-id/{eventoId}");
            return responses ?? new Response<IEnumerable<Evento>> { NoData = true, Success = false, Message = DATARESPONSE.NO_DATA };
        }

        public async Task<Response<IEnumerable<Evento>>> GetEventoAsync()
        {
            return await ExecuteAsyncUtil.ExecuteAsync(() => GetInternalAllEvento());
        }

        private async Task<Response<IEnumerable<Evento>>> GetInternalAllEvento()
        {
            var responses = await _httpClient.GetFromJsonAsync<Response<IEnumerable<Evento>>>($"api/v1/evento/getall");
            return responses ?? new Response<IEnumerable<Evento>> { NoData = true, Success = false, Message = DATARESPONSE.NO_DATA };
        }
    }
}
