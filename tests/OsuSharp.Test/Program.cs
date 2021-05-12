using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using OsuSharp.Domain;
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

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((_, configuration) => configuration.WriteTo.Console())
                .ConfigureLogging(x => x.AddFilter<EventLogLoggerProvider>(level => level >= LogLevel.Trace))
                .ConfigureServices(x =>
                {
                    x.AddOsuSharp(config =>
                        config.Configuration = new OsuClientConfiguration
                        {
                            ClientSecret = "lul",
                            ClientId = 646
                        });

                    x.AddHostedService<OsuTestService>();
                });
    }
}