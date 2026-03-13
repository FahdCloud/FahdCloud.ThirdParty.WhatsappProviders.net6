using System.Reflection;
using FahdCloud.ThirdParty.WhatsappProviders.Interfaces._00_Shared;
using FahdCloud.ThirdParty.WhatsappProviders.Interfaces.Services;
using FahdCloud.ThirdParty.WhatsappProviders.Services;
using FahdCloud.ThirdParty.WhatsappProviders.Services._00_Shared;
using Microsoft.Extensions.DependencyInjection;

namespace FahdCloud.ThirdParty.WhatsappProviders.Extensions
{
    public static class WhatsappServiceCollectionExtensions
    {
        public static IServiceCollection AddWhatsappProviders(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services, "The IServiceCollection instance cannot be null. Provide a valid service collection to register Paymob services.");

            services.AddMemoryCache();

            // Register HttpClient for HTTP communication
            services.AddHttpClient();

            // Register Scrutor services
            services.AddDiServicesConfig();

            return services;
        }

        private static IServiceCollection AddDiServicesConfig(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            // Register Shared services
            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<IHttpClientService, HttpClientService>();

            // Register Payment services
            services.AddScoped<IUltramsgService, UltramsgService>();
            services.AddScoped<IWABotMasterService, WABotMasterService>();
            services.AddScoped<IWASenderService, WASenderService>();
            services.AddScoped<IWhats360Service, Whats360Service>();
            services.AddScoped<IWhats360V2Service, Whats360V2Service>();
            services.AddScoped<IWhats360CrmService, Whats360CrmService>();
            services.AddScoped<IWhats360ProService, Whats360ProService>();
            services.AddScoped<IElwhatsService, ElwhatsService>();

            return services;
        }
    }
}