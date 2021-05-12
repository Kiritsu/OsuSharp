using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class AccessTokenResponseJsonModel : JsonModel
    {
        [JsonProperty("token_type")]
        public string TokenType { get; internal set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; internal set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; internal set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; internal set; }
    }
}