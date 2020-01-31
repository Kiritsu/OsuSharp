using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OsuSharp.Analyzer
{
    public sealed class BeatmapAnalyzer : BaseAnalyzer<long, Beatmap>
    {
        public BeatmapAnalyzer(OsuClient client) : base(client)
        {
        }

        /// <summary>
        ///     Updates the current beatmap by its id. If it's not being tracked, throws an exception.
        /// </summary>
        /// <param name="userId">Id of the beatmap</param>
        /// <param name="token">Cancellation Token to cancel the current requests.</param>
        /// <returns></returns>
        public override async Task<Beatmap> UpdateEntityAsync(long beatmapId, CancellationToken token = default)
        {
            if (!Entities.TryGetValue(beatmapId, out var beatmap))
            {
                throw new InvalidOperationException($"The beatmap with key {beatmapId} is not being tracked yet.");
            }

            var newBeatmap = await _client.GetBeatmapByIdAsync(beatmapId, token).ConfigureAwait(false);

            var equality = beatmap.ApproachRate == newBeatmap.ApproachRate
                && beatmap.ApprovedDate == newBeatmap.ApprovedDate
                && beatmap.Artist == newBeatmap.Artist
                && beatmap.Author == newBeatmap.Author
                && beatmap.Bpm == newBeatmap.Bpm
                && beatmap.CircleSize == newBeatmap.CircleSize
                && beatmap.Difficulty == newBeatmap.Difficulty
                && beatmap.StarRating == newBeatmap.StarRating
                && beatmap.FailCount == newBeatmap.FailCount
                && beatmap.FavoriteCount == newBeatmap.FavoriteCount
                && beatmap.GameMode == newBeatmap.GameMode
                && beatmap.Genre == newBeatmap.Genre
                && beatmap.HitLength == newBeatmap.HitLength
                && beatmap.HpDrain == newBeatmap.HpDrain
                && beatmap.Language == newBeatmap.Language
                && beatmap.MaxCombo == newBeatmap.MaxCombo
                && beatmap.OverallDifficulty == newBeatmap.OverallDifficulty
                && beatmap.PassCount == newBeatmap.PassCount
                && beatmap.PlayCount == newBeatmap.PlayCount
                && beatmap.Source == newBeatmap.Source
                && beatmap.State == newBeatmap.State
                && string.Join("||", beatmap.Tags) == string.Join("||", newBeatmap.Tags)
                && beatmap.Title == newBeatmap.Title
                && beatmap.TotalLength == newBeatmap.TotalLength;

            _entities[beatmapId] = newBeatmap;

            if (!equality)
            {
                EntityUpdated?.Invoke(new EntityUpdateEventArgs<Beatmap>(_client, beatmap, newBeatmap));
            }

            return newBeatmap;
        }
    }
}
