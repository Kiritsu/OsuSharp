using System;
using System.Threading.Tasks;
using OsuSharp.Endpoints;
using OsuSharp.Interfaces;

namespace OsuSharp.Analyzer
{
    public sealed class UserAnalyzer : Analyzer<long, User>
    {
        public UserAnalyzer(IOsuApi api) : base(api)
        {
        }

        public override event EventHandler<User> EntityUpdated;

        public override async Task<User> UpdateValueAsync(long key)
        {
            if (!_entities.TryGetValue(key, out var usr))
            {
                throw new InvalidOperationException($"The user with key {key} is not being cached yet.");
            }

            var user = await Api.GetUserByIdAsync(key, usr.GameMode);

            _entities.TryUpdate(key, user, _entities[key]);
            EntityUpdated?.Invoke(this, _entities[key]);

            return user;
        }
    }
}
