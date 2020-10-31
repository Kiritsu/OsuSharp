using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    internal sealed class AccessTokenResponse
    {
        [JsonProperty("token_type")]
        public string TokenType { get; }
        
        [JsonProperty("expires_in")]
        public long ExpiresIn { get; }
        
        [JsonProperty("access_token")]
        public string AccessToken { get; }
        
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; }
    }
}