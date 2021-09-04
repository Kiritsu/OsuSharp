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
                _client.UpdateAccessToken("redacted", "redacted", 86394);

                var replay = await _client.GetReplayAsync(3803330915, GameMode.Osu, stoppingToken);
                var replayMoveData = replay.GetReplayMoveData();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured");
            }
        }
    }
}