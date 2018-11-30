using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OsuSharp.Analyzer.Entities;
using OsuSharp.Endpoints;
using OsuSharp.Enums;
using OsuSharp.Interfaces;

namespace OsuSharp.Analyzer
{
    public sealed class BestScoreAnalyzer : Analyzer<long, List<UserBest>>
    {
        private readonly int _limit;

        public BestScoreAnalyzer(IOsuApi api, int limit = 100) : base(api)
        {
            _limit = limit;
        }

        public override event EventHandler<UpdateEventArgs<List<UserBest>>> EntityUpdated;

        public override async Task<List<UserBest>> UpdateEntityAsync(long key)
        {
            if (!_entities.TryGetValue(key, out var old))
            {
                throw new InvalidOperationException($"The scores for the user {key} are not being cached yet.");
            }

            Api.Logger.LogMessage(LoggingLevel.Debug, "UserAnalyzer", $"Scores for user {key} are being updated.", DateTime.Now);

            var scores = await Api.GetUserBestByUseridAsync(key, _entities[key][0].GameMode, _limit);

            RemoveEntity(key);
            AddEntity(key, scores);

            EntityUpdated?.Invoke(this, new UpdateEventArgs<List<UserBest>>(old, scores, Api));

            return scores;
        }
    }
}
