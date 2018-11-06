using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OsuSharp.Endpoints;
using OsuSharp.Entities;
using OsuSharp.Enums;

namespace OsuSharp.Interfaces
{
    public interface IOsuApi
    {
        /// <summary>
        ///     ApiKey from Osu!Api
        /// </summary>
        string ApiKey { get; }

        /// <summary>
        ///     Separator used between each mod.
        /// </summary>
        string ModsSeparator { get; set; }

        /// <summary>
        ///     OsuSharp logger.
        /// </summary>
        IOsuSharpLogger Logger { get; }

        /// <summary>
        ///     Method that returns a <see cref="Beatmap" />. It requires a valid BeatmapId.
        /// </summary>
        /// <param name="beatmapId">Id of the beatmap.</param>
        /// <param name="bmType">Type of the beatmap. Beatmapset or difficulty.</param>
        /// <param name="gameMode">Gamemode of the beatmap. Standard, Taiko, Catch or Mania.</param>
        /// <returns>
        ///     <see cref="Beatmap" />
        /// </returns>
        Beatmap GetBeatmap(long beatmapId, BeatmapType bmType = BeatmapType.ByDifficulty,
            GameMode gameMode = GameMode.Standard);

        /// <summary>
        ///     Method that returns a <see cref="Beatmap" />. It requires a valid BeatmapId.
        /// </summary>
        /// <param name="beatmapId">Id of the beatmap.</param>
        /// <param name="bmType">Type of the beatmap. Beatmapset or difficulty.</param>
        /// <param name="gameMode">Gamemode of the beatmap. Standard, Taiko, Catch or Mania.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="Beatmap" />
        /// </returns>
        Task<Beatmap> GetBeatmapAsync(long beatmapId, BeatmapType bmType = BeatmapType.ByDifficulty, GameMode gameMode = GameMode.Standard,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Method that returns a list of <see cref="Beatmap" /> by the given creator's nickname.
        /// </summary>
        /// <param name="username">Author's nickname of the beatmap.</param>
        /// <param name="gameMode">Gamemode of the beatmap. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default and maximum : 500.</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        List<Beatmap> GetBeatmapsByCreator(string username, GameMode gameMode = GameMode.Standard,
            int limit = 500);

        /// <summary>
        ///     Method that returns a list of <see cref="Beatmap" /> by the given creator's nickname.
        /// </summary>
        /// <param name="username">Author's nickname of the beatmap.</param>
        /// <param name="gameMode">Gamemode of the beatmap. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default and maximum : 500.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        Task<List<Beatmap>> GetBeatmapsByCreatorAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 500,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Method that returns a list of <see cref="Beatmap" /> by the given beatmapset id.
        /// </summary>
        /// <param name="id">Id of the beatmapset.</param>
        /// <param name="bmType">Type of the beatmap. ByBeatmap is required.</param>
        /// <param name="gameMode">Gamemode of the beatmap. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the ouput. Default and maximum : 500.</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        List<Beatmap> GetBeatmaps(long id, BeatmapType bmType = BeatmapType.ByBeatmap,
            GameMode gameMode = GameMode.Standard, int limit = 500);

        /// <summary>
        ///     Method that returns a list of <see cref="Beatmap" /> by the given beatmapset id.
        /// </summary>
        /// <param name="id">Id of the beatmapset.</param>
        /// <param name="bmType">Type of the beatmap. ByBeatmap is required.</param>
        /// <param name="gameMode">Gamemode of the beatmap. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the ouput. Default and maximum : 500.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        Task<List<Beatmap>> GetBeatmapsAsync(long id, BeatmapType bmType = BeatmapType.ByBeatmap, GameMode gameMode = GameMode.Standard, int limit = 500,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Method that returns a list of lasts uploaded <see cref="Beatmap" />.
        /// </summary>
        /// <param name="limit">Limit of the output. Default and maximum : 500.</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        List<Beatmap> GetLastBeatmaps(int limit = 500);

        /// <summary>
        ///     Method that returns a list of lasts uploaded <see cref="Beatmap" />.
        /// </summary>
        /// <param name="limit">Limit of the output. Default and maximum : 500.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        Task<List<Beatmap>> GetLastBeatmapsAsync(int limit, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Method that returns a <see cref="User" /> by the given Username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the user. Standard, Taiko, Catch or Mania.</param>
        /// <returns>
        ///     <see cref="User" />
        /// </returns>
        User GetUserByName(string username, GameMode gameMode = GameMode.Standard);

        /// <summary>
        ///     Method that returns a <see cref="User" /> by the given Username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the user. Standard, Taiko, Catch or Mania.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="User" />
        /// </returns>
        Task<User> GetUserByNameAsync(string username, GameMode gameMode = GameMode.Standard,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Method that returns a <see cref="User" /> by the given Userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the user. Standard, Taiko, Catch or Mania.</param>
        /// <returns>
        ///     <see cref="User" />
        /// </returns>
        User GetUserById(long userid, GameMode gameMode = GameMode.Standard);

        /// <summary>
        ///     Method that returns a <see cref="User" /> by the given Userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the user. Standard, Taiko, Catch or Mania.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="User" />
        /// </returns>
        Task<User> GetUserByIdAsync(long userid, GameMode gameMode = GameMode.Standard, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Method that returns a <see cref="Score" /> by the given beatmapid and username.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <returns>
        ///     <see cref="Score" />
        /// </returns>
        Score GetScoreByUsername(long beatmapid, string username, GameMode gameMode = GameMode.Standard);

        /// <summary>
        ///     Method that returns a <see cref="Score" /> by the given beatmapid and username.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="Score" />
        /// </returns>
        Task<Score> GetScoreByUsernameAsync(long beatmapid, string username, GameMode gameMode = GameMode.Standard,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Method that returns a <see cref="Score" /> by the given beatmapid and userid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <returns>
        ///     <see cref="Score" />
        /// </returns>
        Score GetScoreByUserid(long beatmapid, ulong userid, GameMode gameMode = GameMode.Standard);

        /// <summary>
        ///     Method that returns a <see cref="Score" /> by the given beatmapid and userid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="Score" />
        /// </returns>
        Task<Score> GetScoreByUseridAsync(long beatmapid, long userid, GameMode gameMode = GameMode.Standard,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Method that returns a list of <see cref="Score" /> by the given beatmapid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 50, minimum : 1, maximum : 100</param>
        /// <returns>
        ///     <see cref="Score" />
        /// </returns>
        List<Score> GetScores(long beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50);

        /// <summary>
        ///     Method that returns a list of <see cref="Score" /> by the given beatmapid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 50, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="Score" />
        /// </returns>
        Task<List<Score>> GetScoresAsync(long beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Method that returns a list of <see cref="BeatmapScores" /> by the given beatmapid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 50, minimum : 1, maximum : 100</param>
        /// <returns>
        ///     <see cref="BeatmapScores" />
        /// </returns>
        BeatmapScores GetScoresAndBeatmap(long beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50);

        /// <summary>
        ///     Method that returns a list of <see cref="BeatmapScores" /> by the given beatmapid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 50, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="BeatmapScores" />
        /// </returns>
        Task<BeatmapScores> GetScoresAndBeatmapAsync(long beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Method that returns a list of <see cref="BeatmapScoresUsers" /> by the given beatmapid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 50, minimum : 1, maximum : 100</param>
        /// <returns>
        ///     <see cref="BeatmapScoresUsers" />
        /// </returns>
        BeatmapScoresUsers GetScoresWithUsersAndBeatmap(long beatmapid, GameMode gameMode = GameMode.Standard,
            int limit = 50);

        /// <summary>
        ///     Method that returns a list of <see cref="BeatmapScoresUsers" /> by the given beatmapid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 50, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="BeatmapScoresUsers" />
        /// </returns>
        Task<BeatmapScoresUsers> GetScoresWithUsersAndBeatmapAsync(long beatmapid, GameMode gameMode = GameMode.Standard,
            int limit = 50, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Method that returns a list of <see cref="UserBest" /> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        List<UserBest> GetUserBestByUsername(string username, GameMode gameMode = GameMode.Standard,
            int limit = 10);

        /// <summary>
        ///     Method that returns a list of <see cref="UserBest" /> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        Task<List<UserBest>> GetUserBestByUsernameAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 10,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Method that returns a list of <see cref="UserBestBeatmap" /> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        List<UserBestBeatmap> GetUserBestAndBeatmapByUsername(string username,
            GameMode gameMode = GameMode.Standard, int limit = 10);

        /// <summary>
        ///     Method that returns a list of <see cref="UserBestBeatmap" /> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        Task<List<UserBestBeatmap>> GetUserBestAndBeatmapByUsernameAsync(string username,
            GameMode gameMode = GameMode.Standard, int limit = 10, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Method that returns a list of <see cref="UserBest" /> by the given userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        List<UserBest> GetUserBestByUserid(long userid, GameMode gameMode = GameMode.Standard, int limit = 10);

        /// <summary>
        ///     Method that returns a list of <see cref="UserBest" /> by the given userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        Task<List<UserBest>> GetUserBestByUseridAsync(long userid, GameMode gameMode = GameMode.Standard, int limit = 10,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Method that returns a list of <see cref="UserBestBeatmap" /> by the given username.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        List<UserBestBeatmap> GetUserBestAndBeatmapByUserid(long userid, GameMode gameMode = GameMode.Standard,
            int limit = 10);

        /// <summary>
        ///     Method that returns a list of <see cref="UserBestBeatmap" /> by the given username.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        Task<List<UserBestBeatmap>> GetUserBestAndBeatmapByUseridAsync(long userid, GameMode gameMode = GameMode.Standard,
            int limit = 10, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Method that returns a list of <see cref="UserRecent" /> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        List<UserRecent> GetUserRecentByUsername(string username, GameMode gameMode = GameMode.Standard,
            int limit = 10);

        /// <summary>
        ///     Method that returns a list of <see cref="UserRecent" /> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        Task<List<UserRecent>> GetUserRecentByUsernameAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 10,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Method that returns a list of <see cref="UserRecentBeatmap" /> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        List<UserRecentBeatmap> GetUserRecentAndBeatmapByUsername(string username,
            GameMode gameMode = GameMode.Standard, int limit = 10);

        /// <summary>
        ///     Method that returns a list of <see cref="UserRecentBeatmap" /> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        Task<List<UserRecentBeatmap>> GetUserRecentAndBeatmapByUsernameAsync(string username,
            GameMode gameMode = GameMode.Standard, int limit = 10, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Method that returns a list of <see cref="UserRecent" /> by the given userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        List<UserRecent> GetUserRecentByUserid(long userid, GameMode gameMode = GameMode.Standard,
            int limit = 10);

        /// <summary>
        ///     Method that returns a list of <see cref="UserRecent" /> by the given userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        Task<List<UserRecent>> GetUserRecentByUseridAsync(long userid, GameMode gameMode = GameMode.Standard, int limit = 10,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Method that returns a list of <see cref="UserRecentBeatmap" /> by the given userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        List<UserRecentBeatmap> GetUserRecentAndBeatmapByUserid(long userid,
            GameMode gameMode = GameMode.Standard, int limit = 10);

        /// <summary>
        ///     Method that returns a list of <see cref="UserRecentBeatmap" /> by the given userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        Task<List<UserRecentBeatmap>> GetUserRecentAndBeatmapByUseridAsync(long userid, GameMode gameMode = GameMode.Standard,
            int limit = 10, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Method that returns a <see cref="Multiplayer" /> by the given matchid.
        /// </summary>
        /// <param name="matchid">Id of the match.</param>
        /// <returns>
        ///     <see cref="Multiplayer" />
        /// </returns>
        Multiplayer GetMatch(long matchid);

        /// <summary>
        ///     Method that returns a <see cref="Multiplayer" /> by the given matchid.
        /// </summary>
        /// <param name="matchid">Id of the match.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="Multiplayer" />
        /// </returns>
        Task<Multiplayer> GetMatchAsync(long matchid, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Method that returns a <see cref="Replay" /> by the given beatmapid and username.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <returns>
        ///     <see cref="Replay" />
        /// </returns>
        Replay GetReplayByUsername(long beatmapid, string username, GameMode gameMode = GameMode.Standard);

        /// <summary>
        ///     Method that returns a <see cref="Replay" /> by the given beatmapid and username.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="Replay" />
        /// </returns>
        Task<Replay> GetReplayByUsernameAsync(long beatmapid, string username, GameMode gameMode = GameMode.Standard,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Method that returns a <see cref="Replay" /> by the given beatmapid and userid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <returns>
        ///     <see cref="Replay" />
        /// </returns>
        Replay GetReplayByUserid(long beatmapid, long userid, GameMode gameMode = GameMode.Standard);

        /// <summary>
        ///     Method that returns a <see cref="Replay" /> by the given beatmapid and userid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="Replay" />
        /// </returns>
        Task<Replay> GetReplayByUseridAsync(long beatmapid, long userid, GameMode gameMode = GameMode.Standard,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}