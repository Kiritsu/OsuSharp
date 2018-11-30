using System;
using System.Threading.Tasks;
using OsuSharp.Endpoints;
using OsuSharp.Enums;
using OsuSharp.Interfaces;

namespace OsuSharp.Analyzer
{
    public sealed class UserAnalyzer : Analyzer<long, User>
    {
        public UserAnalyzer(IOsuApi api) : base(api)
        {
        }

        /// <summary>
        ///     Fired when an User has been updated.
        /// </summary>
        public override event EventHandler<User> EntityUpdated;

        /// <summary>
        ///     Updates the User associated with the given key.
        /// </summary>
        /// <param name="key">Key associated with the User.</param>
        /// <returns></returns>
        public override async Task<User> UpdateEntityAsync(long key)
        {
            if (!_entities.TryGetValue(key, out var usr))
            {
                throw new InvalidOperationException($"The user with key {key} is not being cached yet.");
            }

            Api.Logger.LogMessage(LoggingLevel.Info, "UserAnalyzer", $"User with key {key} is being updated.", DateTime.Now);

            var user = await Api.GetUserByIdAsync(key, usr.GameMode);

            _entities.TryUpdate(key, user, _entities[key]);
            EntityUpdated?.Invoke(this, _entities[key]);

            return user;
        }

        /// <summary>
        ///     Adds an User to analyze.
        /// </summary>
        /// <param name="user"></param>
        public void AddEntity(User user) 
            => AddEntity(user.Userid, user);

        /// <summary>
        ///     Updates the User by specifying the User object to update.
        /// </summary>
        /// <param name="user">User object to update.</param>
        /// <returns></returns>
        public Task<User> UpdateEntityAsync(User user)
            => UpdateEntityAsync(user.Userid);
    }
}
