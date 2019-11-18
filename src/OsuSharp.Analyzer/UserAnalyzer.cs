using System;
using System.Threading;
using System.Threading.Tasks;

namespace OsuSharp.Analyzer
{
    public sealed class UserAnalyzer : BaseAnalyzer<long, User>
    {
        public UserAnalyzer(OsuClient client) : base(client)
        {
        }

        /// <summary>
        ///     Updates the current user by its id. If it's not being tracked, throws an exception because this method lacks of the GameMode.
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <param name="token">Cancellation Token to cancel the current requests.</param>
        /// <returns></returns>
        public override async Task<User> UpdateEntityAsync(long userId, CancellationToken token = default)
        {
            if (!Entities.TryGetValue(userId, out var user))
            {
                throw new InvalidOperationException($"The user with key {userId} is not being tracked yet.");
            }

            var newUser = await _client.GetUserByUserIdAsync(userId, user.GameMode).ConfigureAwait(false);

            var equality = user.UserId == newUser.UserId
                && user.Username == newUser.Username
                && user.Count50 == newUser.Count50
                && user.Count100 == newUser.Count100
                && user.Count300 == newUser.Count300
                && user.PlayCount == newUser.PlayCount
                && user.RankedScore == newUser.RankedScore
                && user.Score == newUser.Score
                && user.Rank == newUser.Rank
                && user.Level == newUser.Level
                && user.PerformancePoints == newUser.PerformancePoints
                && user.Accuracy == newUser.Accuracy
                && user.CountSS == newUser.CountSS
                && user.CountSSH == newUser.CountSSH
                && user.CountS == newUser.CountS
                && user.CountSH == newUser.CountSH
                && user.CountA == newUser.CountA
                && user.CountryRank == newUser.CountryRank
                && user.GameMode == newUser.GameMode;

            _entities[userId] = newUser;

            if (!equality)
            {
                EntityUpdated?.Invoke(new EntityUpdateEventArgs<User>(_client, user, newUser));
            }

            return newUser;
        }
    }
}
