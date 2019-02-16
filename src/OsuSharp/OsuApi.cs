using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace OsuSharp
{
    public sealed class OsuApi
    {
        private OsuSharpConfiguration OsuSharpConfiguration { get; }
        
        private RateLimiter RateLimiter { get; }

        /// <summary>
        ///     Represents the logger used to send log messages to the client.
        /// </summary>
        public OsuSharpLogger Logger { get; }

        /// <summary>
        ///     Initializes a new instance of <see cref="OsuApi"/> with the given configuration and the default configuration for the rate limiter.
        /// </summary>
        /// <param name="osuSharpConfiguration">Configuration to use for this instance.</param>
        public OsuApi(OsuSharpConfiguration osuSharpConfiguration) : this(osuSharpConfiguration, new RateLimiterConfiguration())
        {

        }

        /// <summary>
        ///     Initializes a new instance of <see cref="OsuApi"/> with the given configuration and the one for the rate limiter.
        /// </summary>
        /// <param name="osuSharpConfiguration">Configuration to use for this instance.</param>
        /// <param name="rateLimiterConfiguration">Rate limiting configuration.</param>
        public OsuApi(OsuSharpConfiguration osuSharpConfiguration, RateLimiterConfiguration rateLimiterConfiguration)
        {
            OsuSharpConfiguration = osuSharpConfiguration;
            RateLimiter = new RateLimiter(rateLimiterConfiguration);

            if (string.IsNullOrWhiteSpace(OsuSharpConfiguration.ApiKey))
            {
                throw new OsuSharpException("The given api key is not valid.");
            }

            if (OsuSharpConfiguration.Client is null)
            {
                OsuSharpConfiguration.Client = new HttpClient();
            }

            Logger = new OsuSharpLogger();
        }

        private async Task<string> RequestAsync(string url, CancellationToken token = default)
        {
            await RateLimiter.HandleAsync(token);
            RateLimiter.IncrementRequestCount();

            var response = await OsuSharpConfiguration.Client.GetAsync(url, token).ConfigureAwait(false);
            var message = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return message;
            }

            throw new OsuSharpException(message);
        }
    }
}
