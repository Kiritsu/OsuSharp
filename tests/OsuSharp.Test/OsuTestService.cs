using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OsuSharp.Builders;
using OsuSharp.Domain;
using OsuSharp.Interfaces;

namespace OsuSharp.Test
{
    public class OsuTestService : BackgroundService
    {
        private readonly ILogger<OsuTestService> _logger;
        private readonly IOsuClient _client;

        public OsuTestService(ILogger<OsuTestService> logger, IOsuClient client)
        {
            _logger = logger;
            _client = client;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var backgrounds = await _client.GetSeasonalBackgroundsAsync(stoppingToken);

                var builder = new BeatmapsetsLookupBuilder()
                    .WithGameMode(GameMode.Taiko)
                    .WithCategory(BeatmapsetCategory.Ranked)
                    .WithConvertedBeatmaps();

                var score = await _client.GetScoreAsync(3848835411, GameMode.Osu, stoppingToken);
                Console.WriteLine(score);

                await foreach (var beatmapset in _client.EnumerateBeatmapsetsAsync(builder, BeatmapSorting.Ranked_Desc, stoppingToken))
                {
                    _logger.LogInformation("Beatmapset pulled: {Id} {Title} {UserId} ({Count} beatmaps)", 
                        beatmapset.Id, beatmapset.Title, beatmapset.UserId, beatmapset.Beatmaps?.Count);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured");
            }
        }
    }
}