using System;
using System.Collections.Generic;
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
                IUser user = await _client.GetUserAsync("sakamata1", token: stoppingToken);
                IReadOnlyList<IScore> bestScores = await _client.GetUserScoresAsync(user.Id, ScoreType.Firsts, limit: 10, token: stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occured");
            }
        }
    }
}