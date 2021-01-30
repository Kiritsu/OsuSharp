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
                ClientSecret = "dG1XaduFuZsktaZrTt58dN3iFvNT6WsOFv6LVMZc",
                LoggingLevel = LogLevel.Trace
            });

            var user = await client.GetUserAsync("Evolia");
            Console.WriteLine(user.Username + " has default game mode " + user.GameMode);
        }
    }
}