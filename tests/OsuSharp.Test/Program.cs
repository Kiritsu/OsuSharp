using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OsuSharp.Extensions;
using OsuSharp.Models;
using Serilog;

namespace OsuSharp.Test
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(x =>
                {
                    var path = Environment.GetEnvironmentVariable("OSUSHARP_OPTIONS_PATH") ?? "options.json";
                    x.AddJsonFile(path);
                })
                .UseSerilog((_, configuration) => configuration.WriteTo.Console().MinimumLevel.Debug())
                .ConfigureOsuSharp((ctx, options) =>
                {
                    var clientConfiguration = new OsuClientConfiguration();
                    ctx.Configuration.GetSection("OsuSharpOptions").Bind(clientConfiguration);
                    options.Configuration = clientConfiguration;
                })
                .ConfigureServices((_, services) => services.AddHostedService<OsuTestService>());
        }
    }
}