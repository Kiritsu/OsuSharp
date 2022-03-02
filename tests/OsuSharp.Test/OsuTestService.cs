using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
                var userScores = await _client.GetUserBeatmapScoresAsync(2324562, 13193514, GameMode.Osu, stoppingToken);
                foreach (var score in userScores.Scores)
                {
                    Console.WriteLine($"PPs: {score.PerformancePoints} (pfc: {score.Perfect})");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occured");
            }
        }
    }
}