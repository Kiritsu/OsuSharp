using System.Collections.Generic;
using System.Threading;
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
        /// Separator used between each mod.
        /// </summary>
        string ModsSeparator { get; }

        /// <summary>
        /// ApiKey from Osu!Api
        /// </summary>
        string ApiKey { get; }

        /// <summary>
        /// OsuSharp logger.
        /// </summary>
        IOsuSharpLogger Logger { get; }

        /// <summary>
        /// Method that returns a <see cref="Beatmap"/>. It requires a valid BeatmapId.
        /// </summary>
        /// <param name="beatmapId">Id of the beatmap.</param>
        /// <param name="bmType">Type of the beatmap. Beatmapset or difficulty.</param>
        /// <param name="gameMode">Gamemode of the beatmap. Standard, Taiko, Catch or Mania.</param>
        /// <returns><see cref="Beatmap"/></returns>
        Beatmap GetBeatmap(ulong beatmapId, BeatmapType bmType = BeatmapType.ByDifficulty, GameMode gameMode = GameMode.Standard);

        /// <summary>
        /// Method that returns a <see cref="Beatmap"/>. It requires a valid BeatmapId.
        /// </summary>
        /// <param name="beatmapId">Id of the beatmap.</param>
        /// <param name="bmType">Type of the beatmap. Beatmapset or difficulty.</param>
        /// <param name="gameMode">Gamemode of the beatmap. Standard, Taiko, Catch or Mania.</param>
        /// <returns><see cref="Beatmap"/></returns>
        Task<Beatmap> GetBeatmapAsync(ulong beatmapId, BeatmapType bmType = BeatmapType.ByDifficulty, GameMode gameMode = GameMode.Standard);

        /// <summary>
        /// Method that returns a <see cref="Beatmap"/>. It requires a valid BeatmapId.
        /// </summary>
        /// <param name="beatmapId">Id of the beatmap.</param>
        /// <param name="bmType">Type of the beatmap. Beatmapset or difficulty.</param>
        /// <param name="gameMode">Gamemode of the beatmap. Standard, Taiko, Catch or Mania.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="Beatmap"/></returns>
        Task<Beatmap> GetBeatmapAsync(ulong beatmapId, BeatmapType bmType, GameMode gameMode, CancellationToken cancellationToken);

        /// <summary>
        /// Method that returns a list of <see cref="Beatmap"/> by the given creator's nickname.
        /// </summary>
        /// <param name="username">Author's nickname of the beatmap.</param>
        /// <param name="gameMode">Gamemode of the beatmap. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default and maximum : 500.</param>
        /// <returns><see cref="List{T}"/></returns>
        List<Beatmap> GetBeatmapsByCreator(string username, GameMode gameMode = GameMode.Standard, int limit = 500);

        /// <summary>
        /// Method that returns a list of <see cref="Beatmap"/> by the given creator's nickname.
        /// </summary>
        /// <param name="username">Author's nickname of the beatmap.</param>
        /// <param name="gameMode">Gamemode of the beatmap. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default and maximum : 500.</param>
        /// <returns><see cref="List{T}"/></returns>
        Task<List<Beatmap>> GetBeatmapsByCreatorAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 500);

        /// <summary>
        /// Method that returns a list of <see cref="Beatmap"/> by the given creator's nickname.
        /// </summary>
        /// <param name="username">Author's nickname of the beatmap.</param>
        /// <param name="gameMode">Gamemode of the beatmap. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default and maximum : 500.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="List{T}"/></returns>
        Task<List<Beatmap>> GetBeatmapsByCreatorAsync(string username, GameMode gameMode, int limit, CancellationToken cancellationToken);

        /// <summary>
        /// Method that returns a list of <see cref="Beatmap"/> by the given beatmapset id.
        /// </summary>
        /// <param name="id">Id of the beatmapset.</param>
        /// <param name="bmType">Type of the beatmap. ByBeatmap is required.</param>
        /// <param name="gameMode">Gamemode of the beatmap. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the ouput. Default and maximum : 500.</param>
        /// <returns><see cref="List{T}"/></returns>
        List<Beatmap> GetBeatmaps(ulong id, BeatmapType bmType = BeatmapType.ByBeatmap, GameMode gameMode = GameMode.Standard, int limit = 500);

        /// <summary>
        /// Method that returns a list of <see cref="Beatmap"/> by the given beatmapset id.
        /// </summary>
        /// <param name="id">Id of the beatmapset.</param>
        /// <param name="bmType">Type of the beatmap. ByBeatmap is required.</param>
        /// <param name="gameMode">Gamemode of the beatmap. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the ouput. Default and maximum : 500.</param>
        /// <returns><see cref="List{T}"/></returns>
        Task<List<Beatmap>> GetBeatmapsAsync(ulong id, BeatmapType bmType = BeatmapType.ByBeatmap, GameMode gameMode = GameMode.Standard, int limit = 500);

        /// <summary>
        /// Method that returns a list of <see cref="Beatmap"/> by the given beatmapset id.
        /// </summary>
        /// <param name="id">Id of the beatmapset.</param>
        /// <param name="bmType">Type of the beatmap. ByBeatmap is required.</param>
        /// <param name="gameMode">Gamemode of the beatmap. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the ouput. Default and maximum : 500.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="List{T}"/></returns>
        Task<List<Beatmap>> GetBeatmapsAsync(ulong id, BeatmapType bmType, GameMode gameMode, int limit, CancellationToken cancellationToken);

        /// <summary>
        /// Method that returns a list of lasts uploaded <see cref="Beatmap"/>.
        /// </summary>
        /// <param name="limit">Limit of the output. Default and maximum : 500.</param>
        /// <returns><see cref="List{T}"/></returns>
        List<Beatmap> GetLastBeatmaps(int limit = 500);

        /// <summary>
        /// Method that returns a list of lasts uploaded <see cref="Beatmap"/>.
        /// </summary>
        /// <param name="limit">Limit of the output. Default and maximum : 500.</param>
        /// <returns><see cref="List{T}"/></returns>
        Task<List<Beatmap>> GetLastBeatmapsAsync(int limit = 500);

        /// <summary>
        /// Method that returns a list of lasts uploaded <see cref="Beatmap"/>.
        /// </summary>
        /// <param name="limit">Limit of the output. Default and maximum : 500.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="List{T}"/></returns>
        Task<List<Beatmap>> GetLastBeatmapsAsync(int limit, CancellationToken cancellationToken);

        /// <summary>
        /// Method that returns a <see cref="User"/> by the given Username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the user. Standard, Taiko, Catch or Mania.</param>
        /// <returns><see cref="User"/></returns>
        User GetUserByName(string username, GameMode gameMode = GameMode.Standard);

        /// <summary>
        /// Method that returns a <see cref="User"/> by the given Username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the user. Standard, Taiko, Catch or Mania.</param>
        /// <returns><see cref="Task{User}"/></returns>
        Task<User> GetUserByNameAsync(string username, GameMode gameMode = GameMode.Standard);

        /// <summary>
        /// Method that returns a <see cref="User"/> by the given Username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the user. Standard, Taiko, Catch or Mania.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="User"/></returns>
        Task<User> GetUserByNameAsync(string username, GameMode gameMode, CancellationToken cancellationToken);

        /// <summary>
        /// Method that returns a <see cref="User"/> by the given Userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the user. Standard, Taiko, Catch or Mania.</param>
        /// <returns><see cref="User"/></returns>
        User GetUserById(ulong userid, GameMode gameMode = GameMode.Standard);

        /// <summary>
        /// Method that returns a <see cref="User"/> by the given Userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the user. Standard, Taiko, Catch or Mania.</param>
        /// <returns><see cref="User"/></returns>
        Task<User> GetUserByIdAsync(ulong userid, GameMode gameMode = GameMode.Standard);

        /// <summary>
        /// Method that returns a <see cref="User"/> by the given Userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the user. Standard, Taiko, Catch or Mania.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="User"/></returns>
        Task<User> GetUserByIdAsync(ulong userid, GameMode gameMode, CancellationToken cancellationToken);

        /// <summary>
        /// Method that returns a <see cref="Score"/> by the given beatmapid and username.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <returns><see cref="Score"/></returns>
        Score GetScoreByUsername(ulong beatmapid, string username, GameMode gameMode = GameMode.Standard);

        /// <summary>
        /// Method that returns a <see cref="Score"/> by the given beatmapid and username.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <returns><see cref="Score"/></returns>
        Task<Score> GetScoreByUsernameAsync(ulong beatmapid, string username, GameMode gameMode = GameMode.Standard);

        /// <summary>
        /// Method that returns a <see cref="Score"/> by the given beatmapid and username.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="Score"/></returns>
        Task<Score> GetScoreByUsernameAsync(ulong beatmapid, string username, GameMode gameMode, CancellationToken cancellationToken);

        /// <summary>
        /// Method that returns a <see cref="Score"/> by the given beatmapid and userid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <returns><see cref="Score"/></returns>
        Score GetScoreByUserid(ulong beatmapid, ulong userid, GameMode gameMode = GameMode.Standard);

        /// <summary>
        /// Method that returns a <see cref="Score"/> by the given beatmapid and userid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <returns><see cref="Score"/></returns>
        Task<Score> GetScoreByUseridAsync(ulong beatmapid, ulong userid, GameMode gameMode = GameMode.Standard);

        /// <summary>
        /// Method that returns a <see cref="Score"/> by the given beatmapid and userid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="Score"/></returns>
        Task<Score> GetScoreByUseridAsync(ulong beatmapid, ulong userid, GameMode gameMode, CancellationToken cancellationToken);

        /// <summary>
        /// Method that returns a list of <see cref="Score"/> by the given beatmapid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 50, minimum : 1, maximum : 100</param>
        /// <returns><see cref="Score"/></returns>
        List<Score> GetScores(ulong beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50);

        /// <summary>
        /// Method that returns a list of <see cref="Score"/> by the given beatmapid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 50, minimum : 1, maximum : 100</param>
        /// <returns><see cref="Score"/></returns>
        Task<List<Score>> GetScoresAsync(ulong beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50);

        /// <summary>
        /// Method that returns a list of <see cref="Score"/> by the given beatmapid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 50, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="Score"/></returns>
        Task<List<Score>> GetScoresAsync(ulong beatmapid, GameMode gameMode, int limit, CancellationToken cancellationToken);

        /// <summary>
        /// Method that returns a list of <see cref="BeatmapScores"/> by the given beatmapid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 50, minimum : 1, maximum : 100</param>
        /// <returns><see cref="BeatmapScores"/></returns>
        BeatmapScores GetScoresAndBeatmap(ulong beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50);

        /// <summary>
        /// Method that returns a list of <see cref="BeatmapScores"/> by the given beatmapid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 50, minimum : 1, maximum : 100</param>
        /// <returns><see cref="BeatmapScores"/></returns>
        Task<BeatmapScores> GetScoresAndBeatmapAsync(ulong beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50);

        /// <summary>
        /// Method that returns a list of <see cref="BeatmapScores"/> by the given beatmapid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 50, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="BeatmapScores"/></returns>
        Task<BeatmapScores> GetScoresAndBeatmapAsync(ulong beatmapid, GameMode gameMode, int limit, CancellationToken cancellationToken);

        /// <summary>
        /// Method that returns a list of <see cref="BeatmapScoresUsers"/> by the given beatmapid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 50, minimum : 1, maximum : 100</param>
        /// <returns><see cref="BeatmapScoresUsers"/></returns>
        BeatmapScoresUsers GetScoresWithUsersAndBeatmap(ulong beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50);

        /// <summary>
        /// Method that returns a list of <see cref="BeatmapScoresUsers"/> by the given beatmapid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 50, minimum : 1, maximum : 100</param>
        /// <returns><see cref="BeatmapScoresUsers"/></returns>
        Task<BeatmapScoresUsers> GetScoresWithUsersAndBeatmapAsync(ulong beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50);

        /// <summary>
        /// Method that returns a list of <see cref="BeatmapScoresUsers"/> by the given beatmapid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 50, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="BeatmapScoresUsers"/></returns>
        Task<BeatmapScoresUsers> GetScoresWithUsersAndBeatmapAsync(ulong beatmapid, GameMode gameMode, int limit, CancellationToken cancellationToken);

        /// <summary>
        /// Method that returns a list of <see cref="UserBest"/> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns><see cref="List{T}"/></returns>
        List<UserBest> GetUserBestByUsername(string username, GameMode gameMode = GameMode.Standard, int limit = 10);

        /// <summary>
        /// Method that returns a list of <see cref="UserBest"/> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns><see cref="List{T}"/></returns>
        Task<List<UserBest>> GetUserBestByUsernameAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 10);

        /// <summary>
        /// Method that returns a list of <see cref="UserBest"/> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="List{T}"/></returns>
        Task<List<UserBest>> GetUserBestByUsernameAsync(string username, GameMode gameMode, int limit, CancellationToken cancellationToken);

        /// <summary>
        /// Method that returns a list of <see cref="UserBestBeatmap"/> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns><see cref="List{T}"/></returns>
        List<UserBestBeatmap> GetUserBestAndBeatmapByUsername(string username, GameMode gameMode = GameMode.Standard, int limit = 10);

        /// <summary>
        /// Method that returns a list of <see cref="UserBestBeatmap"/> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns><see cref="List{T}"/></returns>
        Task<List<UserBestBeatmap>> GetUserBestAndBeatmapByUsernameAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 10);

        /// <summary>
        /// Method that returns a list of <see cref="UserBestBeatmap"/> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="List{T}"/></returns>
        Task<List<UserBestBeatmap>> GetUserBestAndBeatmapByUsernameAsync(string username, GameMode gameMode, int limit, CancellationToken cancellationToken);

        /// <summary>
        /// Method that returns a list of <see cref="UserBest"/> by the given userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns><see cref="List{T}"/></returns>
        List<UserBest> GetUserBestByUserid(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10);

        /// <summary>
        /// Method that returns a list of <see cref="UserBest"/> by the given userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns><see cref="List{T}"/></returns>
        Task<List<UserBest>> GetUserBestByUseridAsync(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10);

        /// <summary>
        /// Method that returns a list of <see cref="UserBest"/> by the given userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="List{T}"/></returns>
        Task<List<UserBest>> GetUserBestByUseridAsync(ulong userid, GameMode gameMode, int limit, CancellationToken cancellationToken);

        /// <summary>
        /// Method that returns a list of <see cref="UserBestBeatmap"/> by the given username.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns><see cref="List{T}"/></returns>
        List<UserBestBeatmap> GetUserBestAndBeatmapByUserid(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10);

        /// <summary>
        /// Method that returns a list of <see cref="UserBestBeatmap"/> by the given username.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns><see cref="List{T}"/></returns>
        Task<List<UserBestBeatmap>> GetUserBestAndBeatmapByUseridAsync(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10);

        /// <summary>
        /// Method that returns a list of <see cref="UserBestBeatmap"/> by the given username.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="List{T}"/></returns>
        Task<List<UserBestBeatmap>> GetUserBestAndBeatmapByUseridAsync(ulong userid, GameMode gameMode, int limit, CancellationToken cancellationToken);

        /// <summary>
        /// Method that returns a list of <see cref="UserRecent"/> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns><see cref="List{T}"/></returns>
        List<UserRecent> GetUserRecentByUsername(string username, GameMode gameMode = GameMode.Standard, int limit = 10);
    
        /// <summary>
        /// Method that returns a list of <see cref="UserRecent"/> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns><see cref="List{T}"/></returns>
        Task<List<UserRecent>> GetUserRecentByUsernameAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 10);

        /// <summary>
        /// Method that returns a list of <see cref="UserRecent"/> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="List{T}"/></returns>
        Task<List<UserRecent>> GetUserRecentByUsernameAsync(string username, GameMode gameMode, int limit, CancellationToken cancellationToken);

        /// <summary>
        /// Method that returns a list of <see cref="UserRecentBeatmap"/> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns><see cref="List{T}"/></returns>
        List<UserRecentBeatmap> GetUserRecentAndBeatmapByUsername(string username, GameMode gameMode = GameMode.Standard, int limit = 10);

        /// <summary>
        /// Method that returns a list of <see cref="UserRecentBeatmap"/> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns><see cref="List{T}"/></returns>
        Task<List<UserRecentBeatmap>> GetUserRecentAndBeatmapByUsernameAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 10);

        /// <summary>
        /// Method that returns a list of <see cref="UserRecentBeatmap"/> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="List{T}"/></returns>
        Task<List<UserRecentBeatmap>> GetUserRecentAndBeatmapByUsernameAsync(string username, GameMode gameMode, int limit, CancellationToken cancellationToken);

        /// <summary>
        /// Method that returns a list of <see cref="UserRecent"/> by the given userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns><see cref="List{T}"/></returns>
        List<UserRecent> GetUserRecentByUserid(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10);

        /// <summary>
        /// Method that returns a list of <see cref="UserRecent"/> by the given userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns><see cref="List{T}"/></returns>
        Task<List<UserRecent>> GetUserRecentByUseridAsync(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10);

        /// <summary>
        /// Method that returns a list of <see cref="UserRecent"/> by the given userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="List{T}"/></returns>
        Task<List<UserRecent>> GetUserRecentByUseridAsync(ulong userid, GameMode gameMode, int limit, CancellationToken cancellationToken);

        /// <summary>
        /// Method that returns a list of <see cref="UserRecentBeatmap"/> by the given userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns><see cref="List{T}"/></returns>
        List<UserRecentBeatmap> GetUserRecentAndBeatmapByUserid(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10);

        /// <summary>
        /// Method that returns a list of <see cref="UserRecentBeatmap"/> by the given userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns><see cref="List{T}"/></returns>
        Task<List<UserRecentBeatmap>> GetUserRecentAndBeatmapByUseridAsync(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10);

        /// <summary>
        /// Method that returns a list of <see cref="UserRecentBeatmap"/> by the given userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="List{T}"/></returns>
        Task<List<UserRecentBeatmap>> GetUserRecentAndBeatmapByUseridAsync(ulong userid, GameMode gameMode, int limit, CancellationToken cancellationToken);

        /// <summary>
        /// Method that returns a <see cref="Matchs"/> by the given matchid.
        /// </summary>
        /// <param name="matchid">Id of the match.</param>
        /// <returns><see cref="Matchs"/></returns>
        Matchs GetMatch(ulong matchid);

        /// <summary>
        /// Method that returns a <see cref="Matchs"/> by the given matchid.
        /// </summary>
        /// <param name="matchid">Id of the match.</param>
        /// <returns><see cref="Matchs"/></returns>
        Task<Matchs> GetMatchAsync(ulong matchid);

        /// <summary>
        /// Method that returns a <see cref="Matchs"/> by the given matchid.
        /// </summary>
        /// <param name="matchid">Id of the match.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="Matchs"/></returns>
        Task<Matchs> GetMatchAsync(ulong matchid, CancellationToken cancellationToken);

        /// <summary>
        /// Method that returns a <see cref="Replay"/> by the given beatmapid and username.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <returns><see cref="Replay"/></returns>
        Replay GetReplayByUsername(ulong beatmapid, string username, GameMode gameMode = GameMode.Standard);

        /// <summary>
        /// Method that returns a <see cref="Replay"/> by the given beatmapid and username.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <returns><see cref="Replay"/></returns>
        Task<Replay> GetReplayByUsernameAsync(ulong beatmapid, string username, GameMode gameMode = GameMode.Standard);

        /// <summary>
        /// Method that returns a <see cref="Replay"/> by the given beatmapid and username.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="Replay"/></returns>
        Task<Replay> GetReplayByUsernameAsync(ulong beatmapid, string username, GameMode gameMode, CancellationToken cancellationToken);

        /// <summary>
        /// Method that returns a <see cref="Replay"/> by the given beatmapid and userid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <returns><see cref="Replay"/></returns>
        Replay GetReplayByUserid(ulong beatmapid, ulong userid, GameMode gameMode = GameMode.Standard);

        /// <summary>
        /// Method that returns a <see cref="Replay"/> by the given beatmapid and userid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <returns><see cref="Replay"/></returns>
        Task<Replay> GetReplayByUseridAsync(ulong beatmapid, ulong userid, GameMode gameMode = GameMode.Standard);

        /// <summary>
        /// Method that returns a <see cref="Replay"/> by the given beatmapid and userid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="Replay"/></returns>
        Task<Replay> GetReplayByUseridAsync(ulong beatmapid, ulong userid, GameMode gameMode, CancellationToken cancellationToken);
    }
}