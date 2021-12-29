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
                try
                {
                    IUser user = await _client.GetUserAsync("sakamata1");
                    IReadOnlyList<IScore> bestScores = await _client.GetUserScoresAsync(user.Id, ScoreType.Firsts, limit: 10);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex);
                    // exception thrown
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured");
            }
        }
    }
}