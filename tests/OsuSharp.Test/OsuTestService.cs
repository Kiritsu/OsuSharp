using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OsuSharp.Domain;
using OsuSharp.Interfaces;
using OsuSharp.Legacy;

namespace OsuSharp.Test
{
    public class OsuTestService : BackgroundService
    {
        private readonly ILogger<OsuTestService> _logger;
        private readonly IOsuClient _client;
        private readonly LegacyOsuClient _legacyClient;

        public OsuTestService(ILogger<OsuTestService> logger, IOsuClient client, LegacyOsuClient legacyClient)
        {
            _logger = logger;
            _client = client;
            _legacyClient = legacyClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var user = await _legacyClient.GetUserByUsernameAsync("Evolia", Legacy.Enums.GameMode.Standard, stoppingToken);
                _logger.LogInformation("User id for Evolia: {Id}", user.UserId);

                var userScores = await _client.GetUserBeatmapScoresAsync(2324562, 13193514, GameMode.Osu, stoppingToken);
                foreach (var score in userScores.Scores)
                {
                    _logger.LogInformation("PPs: {PP} (pfc: {Perfect})", score.PerformancePoints, score.Perfect);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occured");
            }
        }
    }
}