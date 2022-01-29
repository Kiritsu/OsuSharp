using System;
using System.Linq;
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
        /// <summary>
        /// Configures the OsuSharp environment within the <see cref="IHostBuilder"/>.
        /// </summary>
        /// <param name="builder">Instance of the <see cref="IHostBuilder"/>.</param>
        /// <param name="configureOsuSharp">Action holding the <see cref="OsuSharpOptions"/> values.</param>
        /// <exception cref="ArgumentNullException">Thrown when one of the required arguments were null.</exception>
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

        /// <summary>
        /// Configures the OsuSharp environment within the <see cref="IHostBuilder"/>.
        /// </summary>
        /// <param name="builder">Instance of the <see cref="IHostBuilder"/>.</param>
        /// <param name="configureOsuSharp">Action holding the <see cref="OsuSharpOptions"/> and the <see cref="HostBuilderContext"/> values.</param>
        /// <exception cref="ArgumentNullException">Thrown when one of the required arguments were null.</exception>
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

        /// <summary>
        /// Adds the OsuSharp environment to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">Instance of the <see cref="IServiceCollection"/>.</param>
        /// <param name="configureOsuSharp">Action holding the <see cref="OsuSharpOptions"/> values.</param>
        /// <exception cref="ArgumentNullException">Thrown when one of the required arguments were null.</exception>
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

        /// <summary>
        /// Adds the OsuSharp environment to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">Instance of the <see cref="IServiceCollection"/>.</param>
        /// <param name="options">Instance of the <see cref="OsuSharpOptions"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when one of the required arguments were null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the client configuration was null.</exception>
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

            if (!services.Any(x => x.ServiceType == typeof(IJsonSerializer)))
            {
                services.AddSingleton(options.Value.JsonSerializer ?? DefaultJsonSerializer.Instance);
            }

            if (options.Value.RequestHandler is not null)
            {
                services.AddSingleton(options.Value.RequestHandler);
            }
            else if (!services.Any(x => x.ServiceType == typeof(IRequestHandler)))
            {
                services.AddSingleton<IRequestHandler, DefaultRequestHandler>();
            }

            return services.AddSingleton<IOsuClient, OsuClient>();
        }

        /// <summary>
        /// Adds the default <see cref="IRequestHandler"/> implementation to the OsuSharp environment.
        /// </summary>
        /// <param name="services">Instance of the <see cref="IServiceCollection"/>.</param>
        public static IServiceCollection AddDefaultRequestHandler(
            this IServiceCollection services)
        {
            return services.AddSingleton<IRequestHandler, DefaultRequestHandler>();
        }

        /// <summary>
        /// Adds the default <see cref="IJsonSerializer"/> implementation to the OsuSharp environment.
        /// </summary>
        /// <param name="services">Instance of the <see cref="IServiceCollection"/>.</param>
        public static IServiceCollection AddDefaultSerializer(
            this IServiceCollection services)
        {
            return services.AddSingleton(DefaultJsonSerializer.Instance);
        }
    }
}