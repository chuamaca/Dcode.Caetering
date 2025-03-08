using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net;
using System.Net.Http.Headers;

namespace Dcode.Caetering.Application.HttpServices
{
    public class HttpInterceptorService : DelegatingHandler
    {
        private readonly NavigationManager _navigationManager;
        private readonly IJSRuntime _jsRuntime;

        public HttpInterceptorService(NavigationManager navigationManager, IJSRuntime jsRuntime)
        {
            _navigationManager = navigationManager;
            _jsRuntime = jsRuntime;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Si el usuario no está autenticado, limpiar token y redirigir al login
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
                _navigationManager.NavigateTo("/login", true);
            }

            return response;
        }
    }
}
