using System.Collections.Generic;
using System.Threading.Tasks;
using OsuSharp.BeatmapsEndpoint;
using OsuSharp.Entities;
using OsuSharp.MatchEndpoint;
using OsuSharp.Misc;
using OsuSharp.ReplayEndpoint;
using OsuSharp.ScoreEndpoint;
using OsuSharp.UserBestEndpoint;
using OsuSharp.UserEndpoint;
using OsuSharp.UserRecentEndpoint;

namespace OsuSharp
{
    public interface IOsuApi
    {
        /// <summary>
        /// ApiKey from Osu!Api
        /// </summary>
        string ApiKey { get; }

        /// <summary>
        /// Method that returns a <see cref="Beatmap"/>. It requires a valid BeatmapId.
        /// </summary>
        /// <param name="beatmapId">Id of the beatmap.</param>
        /// <param name="bmType">Type of the beatmap. Beatmapset or difficulty.</param>
        /// <param name="gameMode">Gamemode of the beatmap. Standard, Taiko, Catch or Mania.</param>
        /// <returns></returns>
        Task<Beatmap> GetBeatmapAsync(ulong beatmapId, BeatmapType bmType = BeatmapType.ByDifficulty, GameMode gameMode = GameMode.Standard);

        /// <summary>
        /// Method that returns a list of <see cref="Beatmap"/> by the given creator's nickname.
        /// </summary>
        /// <param name="username">Author's nickname of the beatmap.</param>
        /// <param name="gameMode">Gamemode of the beatmap. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default and maximum : 500.</param>
        /// <returns></returns>
        Task<List<Beatmap>> GetBeatmapsAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 500);

        /// <summary>
        /// Method that returns a list of <see cref="Beatmap"/> by the given beatmapset id.
        /// </summary>
        /// <param name="id">Id of the beatmapset.</param>
        /// <param name="bmType">Type of the beatmap. ByBeatmap is required.</param>
        /// <param name="gameMode">Gamemode of the beatmap. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the ouput. Default and maximum : 500.</param>
        /// <returns></returns>
        Task<List<Beatmap>> GetBeatmapsAsync(ulong id, BeatmapType bmType = BeatmapType.ByBeatmap, GameMode gameMode = GameMode.Standard, int limit = 500);

        /// <summary>
        /// Method that returns a list of lasts uploaded <see cref="Beatmap"/>.
        /// </summary>
        /// <param name="limit">Limit of the output. Default and maximum : 500.</param>
        /// <returns></returns>
        Task<List<Beatmap>> GetLastBeatmapsAsync(int limit = 500);

        /// <summary>
        /// Method that returns a <see cref="User"/> by the given Username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the user. Standard, Taiko, Catch or Mania.</param>
        /// <returns></returns>
        Task<User> GetUserByNameAsync(string username, GameMode gameMode = GameMode.Standard);

        /// <summary>
        /// Method that returns a <see cref="User"/> by the given Userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the user. Standard, Taiko, Catch or Mania.</param>
        /// <returns></returns>
        Task<User> GetUserByIdAsync(ulong userid, GameMode gameMode = GameMode.Standard);

        /// <summary>
        /// Method that returns a <see cref="Score"/> by the given beatmapid and username.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <returns></returns>
        Task<Score> GetScoreByUsernameAsync(ulong beatmapid, string username, GameMode gameMode = GameMode.Standard);

        /// <summary>
        /// Method that returns a <see cref="Score"/> by the given beatmapid and userid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <returns></returns>
        Task<Score> GetScoreByUseridAsync(ulong beatmapid, ulong userid, GameMode gameMode = GameMode.Standard);

        /// <summary>
        /// Method that returns a list of <see cref="Score"/> by the given beatmapid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 50, minimum : 1, maximum : 100</param>
        /// <returns></returns>
        Task<List<Score>> GetScoresAsync(ulong beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50);

        /// <summary>
        /// Method that returns a list of <see cref="BeatmapScores"/> by the given beatmapid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 50, minimum : 1, maximum : 100</param>
        /// <returns></returns>
        Task<BeatmapScores> GetScoresAndBeatmapAsync(ulong beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50);

        /// <summary>
        /// Method that returns a list of <see cref="BeatmapScoresUsers"/> by the given beatmapid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 50, minimum : 1, maximum : 100</param>
        /// <returns></returns>
        Task<BeatmapScoresUsers> GetScoresWithUsersAndBeatmapAsync(ulong beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50);

        /// <summary>
        /// Method that returns a list of <see cref="UserBest"/> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns></returns>
        Task<List<UserBest>> GetUserBestByUsernameAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 10);

        /// <summary>
        /// Method that returns a list of <see cref="UserBestBeatmap"/> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns></returns>
        Task<List<UserBestBeatmap>> GetUserBestAndBeatmapByUsernameAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 10);

        /// <summary>
        /// Method that returns a list of <see cref="UserBest"/> by the given userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns></returns>
        Task<List<UserBest>> GetUserBestByUseridAsync(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10);

        /// <summary>
        /// Method that returns a list of <see cref="UserBestBeatmap"/> by the given username.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns></returns>
        Task<List<UserBestBeatmap>> GetUserBestAndBeatmapByUseridAsync(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10);

        /// <summary>
        /// Method that returns a list of <see cref="UserRecent"/> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns></returns>
        Task<List<UserRecent>> GetUserRecentByUsernameAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 10);

        /// <summary>
        /// Method that returns a list of <see cref="UserRecentBeatmap"/> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns></returns>
        Task<List<UserRecentBeatmap>> GetUserRecentAndBeatmapByUsernameAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 10);

        /// <summary>
        /// Method that returns a list of <see cref="UserRecent"/> by the given userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns></returns>
        Task<List<UserRecent>> GetUserRecentByUseridAsync(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10);

        /// <summary>
        /// Method that returns a list of <see cref="UserRecentBeatmap"/> by the given userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns></returns>
        Task<List<UserRecentBeatmap>> GetUserRecentAndBeatmapByUseridAsync(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10);

        /// <summary>
        /// Method that returns a <see cref="Matchs"/> by the given matchid.
        /// </summary>
        /// <param name="matchid">Id of the match.</param>
        /// <returns></returns>
        Task<Matchs> GetMatchAsync(ulong matchid);

        /// <summary>
        /// Method that returns a <see cref="Replay"/> by the given beatmapid and username.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <returns></returns>
        Task<Replay> GetReplayByUsernameAsync(ulong beatmapid, string username, GameMode gameMode = GameMode.Standard);

        /// <summary>
        /// Method that returns a <see cref="Replay"/> by the given beatmapid and userid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <returns></returns>
        Task<Replay> GetReplayByUseridAsync(ulong beatmapid, ulong userid, GameMode gameMode = GameMode.Standard);
    }
}