using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
                            ClientSecret = "",
                            ClientId = 646
                        });

                    x.AddHostedService<OsuTestService>();
                });
        }
    }
}