namespace Dcode.Caetering.Application
{
    using Dcode.Caetering.Application.HttpServices;
    using System.Net.Http.Headers;
    public static class ServiceExtensions
    {

        public static IServiceCollection AddServiceCollectionApp(this IServiceCollection services)
        {
            services.AddMapperConfiguration();

            services.AddScoped<LocalHttpService>();
            services.AddScoped<EventoHttpService>();
            services.AddScoped<AuthHttpService>();
            return services;
        }

        public static IServiceCollection AddServicesApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("CAETERINGAPI", async client =>
            {

                //llama al servicio de autenticacion para obtener el token
                var authHttpService = services.BuildServiceProvider().GetRequiredService<AuthHttpService>();
                var authDto = new AuthDto
                {
                    UserName ="admin",
                    Password = "admin"
                };

                var response = await authHttpService.LoginAsync(authDto);
                var token = response.Data.Token;

                client.BaseAddress = new Uri(configuration["ApiExternal:Url"]) ?? throw new ArgumentNullException("ApiExternal:Url");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", configuration["ApiExternal:Token"]);

            });

            return services;
        }
    }
}
