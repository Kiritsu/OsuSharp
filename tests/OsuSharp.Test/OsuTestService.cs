using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OsuSharp.Domain;
using OsuSharp.Interfaces;
using OsuSharp.Legacy;

namespace OsuSharp.Test;

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
            var bmAttribute = await _client.GetBeatmapAttributes(217611, null, GameMode.Taiko, token: stoppingToken);
            _logger.LogInformation("Star rating: {Stars}, {Stamina}", bmAttribute.Attributes.StarRating, bmAttribute.Attributes.StaminaDifficulty);
            
            var user = await _legacyClient.GetUserByUsernameAsync("Evolia", Legacy.Enums.GameMode.Standard, stoppingToken);
            _logger.LogInformation("User id for Evolia: {Id}", user.UserId);
            
            var userScores = await _client.GetUserBeatmapScoresAsync(2324562, 13193514, GameMode.Osu, stoppingToken);
            foreach (var score in userScores.Scores)
            {
                _logger.LogInformation("PPs: {PP} (pfc: {Perfect})", score.PerformancePoints, score.Perfect);
            }

            var userCurrent = await _client.GetUserAsync("Evolia", GameMode.Taiko, stoppingToken);
            _logger.LogInformation("{Name} {Id} {Level}", userCurrent.Username, userCurrent.Id, userCurrent.Statistics.UserLevel.Current);
            _logger.LogInformation("Statistics for Evolia: {300} {100} {50} {Miss}", 
                userCurrent.Statistics.Count300, userCurrent.Statistics.Count100, userCurrent.Statistics.Count50, userCurrent.Statistics.CountMiss);
            
            await foreach (var bm in _client.EnumerateBeatmapsetsAsync(new BeatmapsetsLookupBuilder().WithCategory(BeatmapsetCategory.Graveyard), BeatmapSorting.Updated_Desc))
            {
                _logger.LogInformation("{Name} {Author} {Creator}", bm.Title, bm.Artist, bm.User?.Username ?? bm.UserId.ToString());
            }

            var i = 0;
            await foreach (var beatmap in _client.EnumerateBeatmapsetsAsync(token: stoppingToken))
            {
                i++;
                _logger.LogInformation("{Name} {Author} {Creator}", beatmap.Title, beatmap.Artist, beatmap.User?.Username ?? beatmap.UserId.ToString());

                if (i == 100)
                {
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occured");
        }
    }
}