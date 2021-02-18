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
        public const string Beatmapsets = "/beatmapsets";
        public const string Me = "/me";
        public const string Kudosu = "/kudosu";
        public const string Recent = "/recent_activity";

        public const string CurrentEndpoint = Api + Me;
        public const string UserEndpoint = Api + Users;
        public const string UserKudosuEndpoint = Api + Users + "/{0}" + Kudosu;
        public const string UserRecentEndpoint = Api + Users + "/{0}" + Recent;
        public const string UserBeatmapsetsEndpoint = Api + Users + "/{0}" + Beatmapsets + "/{1}";
        public const string CurrentTokensEndpoint = Oauth + Tokens + Current;
        public const string TokenEndpoint = Oauth + Token;
    }
}