namespace OsuSharp;

internal static class Endpoints
{
    public const string Oauth = "/oauth";
    public const string Token = "/token";
    public const string Tokens = "/tokens";
    public const string Current = "/current";
    public const string Api = "/api/v2";
    public const string Users = "/users";
    public const string Beatmapsets = "/beatmapsets";
    public const string Beatmaps = "/beatmaps";
    public const string Scores = "/scores";
    public const string Me = "/me";
    public const string Kudosu = "/kudosu";
    public const string Recent = "/recent_activity";
    public const string Lookup = "/lookup";
    public const string Search = "/search";
    public const string Download = "/download";
    public const string SeasonalBackgrounds = "/seasonal-backgrounds";
    public const string Rankings = "/rankings";
    public const string Matches = "/matches";

    public const string CurrentEndpoint = Api + Me;
    public const string UserEndpoint = Api + Users;
    public const string UserKudosuEndpoint = Api + Users + "/{0}" + Kudosu;
    public const string UserRecentEndpoint = Api + Users + "/{0}" + Recent;
    public const string UserBeatmapsetsEndpoint = Api + Users + "/{0}" + Beatmapsets + "/{1}";
    public const string UserScoresEndpoint = Api + Users + "/{0}" + Scores + "/{1}";
    public const string BeatmapsEndpoint = Api + Beatmaps;
    public const string BeatmapsBeatmapEndpoint = Api + Beatmaps + "/{0}";
    public const string BeatmapsBeatmapAttributesEndpoint = Api + Beatmaps + "/{0}/attributes";
    public const string BeatmapsScoresEndpoint = Api + Beatmaps + "/{0}" + Scores;
    public const string BeatmapsUserScoreEndpoint = Api + Beatmaps + "/{0}" + Scores + Users + "/{1}";
    public const string BeatmapsUserScoresEndpoint = Api + Beatmaps + "/{0}" + Scores + Users + "/{1}/all";
    public const string BeatmapsetsEndpoint = Api + Beatmapsets + "/{0}";
    public const string BeatmapsLookupEndpoint = Api + Beatmaps + Lookup;
    public const string BeatmapsetsLookupEndpoint = Api + Beatmapsets + Lookup;
    public const string BeatmapsetsSearchEndpoint = Api + Beatmapsets + Search;
    public const string SeasonalBackgroundsEndpoint = Api + SeasonalBackgrounds;
    public const string ScoresEndpoint = Api + Scores + "/{0}" + "/{1}";
    public const string ScoresDownloadEndpoint = Api + Scores + "/{0}" + "/{1}" + Download;
    public const string CurrentTokensEndpoint = Oauth + Tokens + Current;
    public const string TokenEndpoint = Oauth + Token;
    public const string RankingsEndpoint = Api + Rankings + "/{0}" + "/{1}";
    public const string MatchesHistoryEndpoint = Api + Matches;
    public const string MatchesEndpoint = Api + Matches + "/{0}";
}