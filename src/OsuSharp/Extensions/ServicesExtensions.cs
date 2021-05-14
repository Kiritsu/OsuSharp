using System;
using Microsoft.Extensions.DependencyInjection;
using OsuSharp.Interfaces;
using OsuSharp.Net;
using OsuSharp.Net.Serialization;

namespace OsuSharp.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddOsuSharp(
            this IServiceCollection services,
            Action<OsuSharpConfiguration> configurationAction)
        {
            var config = new OsuSharpConfiguration();
            configurationAction.Invoke(config);

            if (config.Configuration is null)
            {
                throw new ArgumentNullException(nameof(config.Configuration));
            }

            config.JsonSerializer ??= DefaultJsonSerializer.Instance;

            services.AddSingleton(config.Configuration);
            services.AddSingleton(config.JsonSerializer);

            if (config.RequestHandler is not null)
            {
                services.AddSingleton(config.RequestHandler);
            }
            else
            {
                services.AddSingleton<IRequestHandler, DefaultRequestHandler>();
            }

            return services.AddSingleton<IOsuClient, OsuClient>();
        }
    }

    public class OsuSharpConfiguration
    {
        public OsuClientConfiguration Configuration { get; set; }
        public IJsonSerializer JsonSerializer { get; set; }
        public IRequestHandler RequestHandler { get; set; }
    }
}