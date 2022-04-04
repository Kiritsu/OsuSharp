using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class AccessTokenResponseJsonModel : JsonModel
{
    [JsonProperty("token_type")] 
    public string TokenType { get; set; } = null!;

    [JsonProperty("expires_in")]
    public long ExpiresIn { get; set; }

    [JsonProperty("access_token")]
    public string AccessToken { get; set; } = null!;

    [JsonProperty("refresh_token")]
    public string RefreshToken { get; set; } = null!;
}