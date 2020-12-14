namespace OsuSharp
{
    internal static class Endpoints
    {
        public const string Domain = "https://osu.ppy.sh";
        public const string Oauth = "/oauth";
        public const string Token = "/token";
        public const string Tokens = "/tokens";
        public const string Current = "/current";
        public const string Api = "/api/v2";
        public const string Users = "/users";
        public const string Me = "/me";
        public const string Kudosu = "/kudosu";

        public const string CurrentEndpoint = Domain + Api + Me;
        public const string UserEndpoint = Domain + Api + Users;
        public const string CurrentTokensEndpoint = Domain + Oauth + Tokens + Current;
        public const string TokenEndpoint = Domain + Oauth + Token;
    }
}