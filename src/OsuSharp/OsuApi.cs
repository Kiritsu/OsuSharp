using Newtonsoft.Json;
using OsuSharp.Entities;
using OsuSharp.Enums;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace OsuSharp
{
    public sealed class OsuApi
    {
        private const string Root = "https://osu.ppy.sh/api";

        private const string Beatmaps = "/get_beatmaps";

        private const string Scores = "/api/get_scores";

        private const string User = "/get_user";
        private const string UserBest = "/api/get_user_best";
        private const string UserRecent = "/api/get_user_recent";

        private const string Match = "/api/get_match";
        private const string Replay = "/api/get_replay";

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

        #region Beatmap

        /// <summary>
        ///     Gets lasts beatmaps depending on the given limit. Default and maximum to 500.
        /// </summary>
        /// <param name="limit">Limit amount of beatmap.</param>
        /// <param name="token">Cancellation token used to cancel the current request.</param>
        /// <returns><see cref="IReadOnlyList{Beatmap}"/></returns>
        public async Task<IReadOnlyList<Beatmap>> GetBeatmapsAsync(int limit = 500, CancellationToken token = default)
        {
            var url = $"{Root}{Beatmaps}?k={OsuSharpConfiguration.ApiKey}&limit={limit}";
            var request = await RequestAsync(url, token).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<IReadOnlyList<Beatmap>>(request);
        }

        /// <summary>
        ///     Gets lasts beatmaps published since the given <see cref="DateTimeOffset"/> depending on the specified limit. Default and maximum to 500.
        /// </summary>
        /// <param name="since">Date and time reference.</param>
        /// <param name="limit">Limit amount of beatmap.</param>
        /// <param name="token">Cancellation token used to cancel the current request.</param>
        /// <returns></returns>
        public async Task<IReadOnlyList<Beatmap>> GetBeatmapsAsync(DateTimeOffset since, int limit = 500, CancellationToken token = default)
        {
            var url = $"{Root}{Beatmaps}?k={OsuSharpConfiguration.ApiKey}&limit={limit}&since={since:yyyy-MM-dd HH:mm:ss}";
            var request = await RequestAsync(url, token).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<IReadOnlyList<Beatmap>>(request);
        }

        /// <summary>
        ///     Gets lasts beatmaps depending on the specified limit, the game mode and if we include converted beatmaps. Default and maximum to 500 for the limit.
        /// </summary>
        /// <param name="limit">Limit amount of beatmap.</param>
        /// <param name="token">Cancellation token used to cancel the current request.</param>
        /// <returns><see cref="IReadOnlyList{Beatmap}"/></returns>
        public async Task<IReadOnlyList<Beatmap>> GetBeatmapsAsync(GameMode gameMode, bool includeConvertedBeatmaps = true, int limit = 500, CancellationToken token = default)
        {
            var url = $"{Root}{Beatmaps}?k={OsuSharpConfiguration.ApiKey}&limit={limit}&m={gameMode}&a={(includeConvertedBeatmaps ? 1 : 0)}";
            var request = await RequestAsync(url, token).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<IReadOnlyList<Beatmap>>(request);
        }

        /// <summary>
        ///     Gets lasts beatmaps published since the given <see cref="DateTimeOffset"/> depending on the specified limit, the game mode and if we include converted beatmaps. Default and maximum to 500 for the limit.
        /// </summary>
        /// <param name="since">Date and time reference.</param>
        /// <param name="limit">Limit amount of beatmap.</param>
        /// <param name="token">Cancellation token used to cancel the current request.</param>
        /// <returns></returns>
        public async Task<IReadOnlyList<Beatmap>> GetBeatmapsAsync(GameMode gameMode, DateTimeOffset since, bool includeConvertedBeatmaps = true, int limit = 500, CancellationToken token = default)
        {
            var url = $"{Root}{Beatmaps}?k={OsuSharpConfiguration.ApiKey}&limit={limit}&since={since:yyyy-MM-dd HH:mm:ss}&m={gameMode}&a={(includeConvertedBeatmaps ? 1 : 0)}";
            var request = await RequestAsync(url, token).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<IReadOnlyList<Beatmap>>(request);
        }

        /// <summary>
        ///     Gets the beatmap corresponding to the given hash.
        /// </summary>
        /// <param name="hash">Hash of a replay.</param>
        /// <param name="token">Cancellation token used to cancel the current request.</param>
        /// <returns></returns>
        public async Task<Beatmap> GetBeatmapByHashAsync(string hash, CancellationToken token = default)
        {
            var url = $"{Root}{Beatmaps}?k={OsuSharpConfiguration.ApiKey}&h={hash}";
            var request = await RequestAsync(url, token).ConfigureAwait(false);

            var data = JsonConvert.DeserializeObject<IReadOnlyList<Beatmap>>(request);
            if (data.Count > 0)
            {
                return data[0];
            }

            return null;
        }

        #endregion

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
