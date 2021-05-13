using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using OsuSharp.Extensions;
using Serilog;

namespace OsuSharp.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog((_, configuration) => configuration.WriteTo.Console().MinimumLevel.Debug())
                .ConfigureServices(x =>
                {
                    x.AddOsuSharp(config =>
                        config.Configuration = new OsuClientConfiguration
                        {
                            ClientSecret = "4dXzkUIxByiutR9klBbn7TDwRULlmN7XNQHNaBir",
                            ClientId = 646
                        });

                    x.AddHostedService<OsuTestService>();
                });
        }
    }
}