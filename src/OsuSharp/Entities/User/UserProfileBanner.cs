using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    public sealed class UserProfileBanner
    {
        [JsonProperty("id")]
        public long Id { get; internal set; }
        
        [JsonProperty("tournament_id")]
        public long TournamentId { get; internal set; }
        
        [JsonProperty("image")]
        public string Image { get; internal set; }
        
        internal UserProfileBanner()
        {
            
        }
    }
}