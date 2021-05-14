using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OsuSharp.Interfaces;
using OsuSharp.Models;
using OsuSharp.Net;
using OsuSharp.Net.Serialization;

namespace OsuSharp.Extensions
{
    public static class ServicesExtensions
    {
        public static IHostBuilder ConfigureOsuSharp(
            this IHostBuilder builder,
            Action<OsuSharpOptions> configureOsuSharp)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (configureOsuSharp == null)
            {
                throw new ArgumentNullException(nameof(configureOsuSharp));
            }

            builder.ConfigureServices((_, services) =>
            {
                var options = new OsuSharpOptions();
                configureOsuSharp.Invoke(options);
                
                services.AddOsuSharp(options);
            });

            return builder;
        }
        
        public static IHostBuilder ConfigureOsuSharp(
            this IHostBuilder builder,
            Action<HostBuilderContext, OsuSharpOptions> configureOsuSharp)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (configureOsuSharp == null)
            {
                throw new ArgumentNullException(nameof(configureOsuSharp));
            }

            builder.ConfigureServices((context, services) =>
            {
                var options = new OsuSharpOptions();
                configureOsuSharp.Invoke(context, options);
                
                services.AddOsuSharp(options);
            });

            return builder;
        }

        public static IServiceCollection AddOsuSharp(
            this IServiceCollection services,
            Action<OsuSharpOptions> configureOsuSharp)
        {
            if (configureOsuSharp == null)
            {
                throw new ArgumentNullException(nameof(configureOsuSharp));
            }
            
            var options = new OsuSharpOptions();
            configureOsuSharp.Invoke(options);
                
            return services.AddOsuSharp(options);
        }

        public static IServiceCollection AddOsuSharp(
            this IServiceCollection services,
            OsuSharpOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            
            if (options.Value.Configuration is null)
            {
                throw new InvalidOperationException("The client configuration must not be null.");
            }

            services.AddSingleton(options.Value.Configuration);
            services.AddSingleton(options.Value.JsonSerializer ?? DefaultJsonSerializer.Instance);

            if (options.Value.RequestHandler is not null)
            {
                services.AddSingleton(options.Value.RequestHandler);
            }
            else
            {
                services.AddSingleton<IRequestHandler, DefaultRequestHandler>();
            }

            return services.AddSingleton<IOsuClient, OsuClient>();
        }
    }
}