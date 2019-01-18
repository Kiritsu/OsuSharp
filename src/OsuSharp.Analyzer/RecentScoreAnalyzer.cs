using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OsuSharp.Analyzer.Entities;
using OsuSharp.Endpoints;
using OsuSharp.Enums;
using OsuSharp.Interfaces;

namespace OsuSharp.Analyzer
{
    public sealed class RecentScoreAnalyzer : Analyzer<long, List<UserRecent>>
    {
        private readonly int _limit;

        public RecentScoreAnalyzer(IOsuApi api, int limit = 100) : base(api)
        {
            _limit = limit;
        }

        public override event EventHandler<UpdateEventArgs<List<UserRecent>>> EntityUpdated;

        public override async Task<List<UserRecent>> UpdateEntityAsync(long key, CancellationToken token = default)
        {
            if (!_entities.TryGetValue(key, out var old))
            {
                throw new InvalidOperationException($"The scores for the user {key} are not being cached yet.");
            }

            Api.Logger.LogMessage(LoggingLevel.Debug, "UserAnalyzer", $"Scores for user {key} are being updated.", DateTime.Now);

            var scores = await Api.GetUserRecentByUseridAsync(key, _entities[key][0].GameMode, _limit, token);

            RemoveEntity(key);
            AddEntity(key, scores);

            EntityUpdated?.Invoke(this, new UpdateEventArgs<List<UserRecent>>(old, scores, Api));

            return scores;
        }
    }
}
