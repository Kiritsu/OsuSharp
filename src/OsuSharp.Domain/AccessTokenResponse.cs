using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public sealed class AccessTokenResponse : IAccessTokenResponse
    {
        public string TokenType { get; internal set; }

        public long ExpiresIn { get; internal set; }

        public string AccessToken { get; internal set; }

        public string RefreshToken { get; internal set; }

        internal AccessTokenResponse()
        {

        }
    }
}