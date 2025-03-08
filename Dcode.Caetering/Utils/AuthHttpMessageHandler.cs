using System.Net.Http.Headers;

namespace Dcode.Caetering.Utils
{
    public class AuthHttpMessageHandler(ITokenService tokenService) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var token = await tokenService.GetTokenAsync();

                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    Console.WriteLine(" Token agregado a la solicitud.");
                }
                else
                {
                    Console.WriteLine(" No se encontró token.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error en AuthHttpMessageHandler: {ex.Message}");
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
