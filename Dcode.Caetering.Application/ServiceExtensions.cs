namespace Dcode.Caetering.Application
{
    using Dcode.Caetering.Application.HttpServices;
    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.Extensions.DependencyInjection;
    using System;
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
                client.BaseAddress = new Uri(configuration["ApiExternal:Url"]!) ?? throw new ArgumentNullException("ApiExternal:Url");
                client.DefaultRequestHeaders.Add("Accept", "application/json");

            });
            return services;
        }
    }
}
