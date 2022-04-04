using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class UserProfileBannerJsonModel : JsonModel
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("tournament_id")]
    public long TournamentId { get; set; }

    [JsonProperty("image")]
    public string Image { get; set; } = null!;
}