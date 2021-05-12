using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class UserProfileBannerJsonModel
    {
        [JsonProperty("id")]
        public long Id { get; internal set; }

        [JsonProperty("tournament_id")]
        public long TournamentId { get; internal set; }

        [JsonProperty("image")]
        public string Image { get; internal set; }

        internal UserProfileBannerJsonModel()
        {
        }
    }
}