using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace OsuSharp.Test
{
    public class Program
    {
        public static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        public static async Task MainAsync()
        {
            var client = new OsuClient(new OsuClientConfiguration
            {
                ClientId = 646,
                ClientSecret = "seekret",
                LoggingLevel = LogLevel.Trace
            });

            await client.GetOrUpdateAccessTokenAsync();

            await Task.Delay(-1);
        }
    }
}