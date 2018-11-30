using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OsuSharp.Endpoints;
using OsuSharp.Entities;
using OsuSharp.Enums;
using OsuSharp.Misc;

namespace OsuSharp.Example
{
    internal class Program
    {
        private static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        public static async Task MainAsync()
        {
            try
            {
                var instance = new OsuApi(new OsuSharpConfiguration
                {
                    ApiKey = File.ReadAllText("token.txt"),
                    ModsSeparator = "|",
                    MaxRequests = 4,
                    TimeInterval = TimeSpan.FromSeconds(8),
                    LogLevel = LoggingLevel.Debug
                });

                // Getting a specific user's replay
                Console.WriteLine("specific replay");
                var bm = await instance.GetBeatmapAsync(936026).ConfigureAwait(false);
                var scr = await instance.GetScoreByUsernameAsync(936026, "filsdelama").ConfigureAwait(false);
                var usr = await instance.GetUserByNameAsync("filsdelama").ConfigureAwait(false);
                var rpl = await instance.GetReplayByUsernameAsync(936026, "filsdelama").ConfigureAwait(false);
                var rp = ReplayFile.CreateReplayFile(rpl, usr, scr, bm);
                var fs = new FileStream("replay-specific.osr", FileMode.OpenOrCreate);
                rp.ToStream(fs);
                fs.Close();

                // ^ can also be done with this:
                // instance.CreateReplayFile("test.osr", rpl, usr, scr, bm);

                // Get a top replay
                Console.WriteLine("top replay");
                var scrs = instance.GetScores(1775286).First();
                var usrs = instance.GetUserById(scrs.Userid);
                var bms = instance.GetBeatmap(1775286);
                var rpls = instance.GetReplayByUserid(1775286, scrs.Userid);
                var replaye = ReplayFile.CreateReplayFile(rpls, usrs, scrs, bms);
                var filestr = new FileStream("replay-top.osr", FileMode.OpenOrCreate);
                replaye.ToStream(filestr);
                filestr.Close();
                Console.WriteLine("top replay done");

                instance.Logger.LogMessageReceived += (sender, args) =>
                    args.Logger.Print(args.Level, args.From, args.Message, args.Time);

                var user = await instance.GetUserByNameAsync("Evolia").ConfigureAwait(false);
                Console.WriteLine($"User {user.Username} with id {user.Userid}\n" +
                                  $" > Current accuracy : {user.Accuracy}\n" +
                                  $" > Total Score : {user.TotalScore}\n" +
                                  $" > Ranked Score : {user.RankedScore}\n" +
                                  $" > Level : {user.Level}\n" +
                                  $" > Performance Points : {user.Pp}\n" +
                                  $" > Play count : {user.PlayCount}");

                var bests = await instance.GetUserBestByUsernameAsync("Evolia", GameMode.Standard, 20).ConfigureAwait(false);
                var cnt = 0;
                foreach (var best in bests)
                {
                    Console.WriteLine($"Top Score {cnt}:");
                    Console.WriteLine($"Accuracy: {best.Accuracy}\nMods: {best.Mods.ToModString(instance)}");
                    Console.WriteLine();
                    cnt++;
                }

                var beatmap = await instance.GetBeatmapAsync(75).ConfigureAwait(false);
                Console.WriteLine($"\n\nBeatmap {beatmap.Title} with id {beatmap.BeatmapId} mapped by {beatmap.Creator}\n" +
                                  $" > Difficulty : {beatmap.Difficulty}\n" +
                                  $" > State : {beatmap.State}\n" +
                                  $" > BPM : {beatmap.Bpm}\n" +
                                  $" > AR : {beatmap.ApproachRate}\n" +
                                  $" > OD : {beatmap.OverallDifficulty}\n" +
                                  $" > CS : {beatmap.CircleSize}\n" +
                                  $" > HP : {beatmap.HpDrain}\n" +
                                  $" > Star difficulty : {beatmap.DifficultyRating}\n");

                var userBestsBeatmaps = await instance.GetUserBestAndBeatmapByUsernameAsync("Evolia").ConfigureAwait(false);
                foreach (var userBestBeatmap in userBestsBeatmaps)
                {
                    Console.WriteLine($"\nScore {userBestBeatmap.UserBest.ScorePoints} with {userBestBeatmap.UserBest.Accuracy} accuracy\nOn map {userBestBeatmap.Beatmap.Title} made by {userBestBeatmap.Beatmap.Creator} with difficulty {userBestBeatmap.Beatmap.Difficulty}");
                }

                var beatmapScores = await instance.GetScoresAndBeatmapAsync(75).ConfigureAwait(false);
                Console.WriteLine($"\n\nBeatmap {beatmapScores.Beatmap.Title} with id {beatmapScores.Beatmap.BeatmapId} mapped by {beatmapScores.Beatmap.Creator}\n" +
                                  $" > Difficulty : {beatmapScores.Beatmap.Difficulty}\n" +
                                  $" > State : {beatmapScores.Beatmap.State}\n" +
                                  $" > BPM : {beatmapScores.Beatmap.Bpm}\n" +
                                  $" > AR : {beatmapScores.Beatmap.ApproachRate}\n" +
                                  $" > OD : {beatmapScores.Beatmap.OverallDifficulty}\n" +
                                  $" > CS : {beatmapScores.Beatmap.CircleSize}\n" +
                                  $" > HP : {beatmapScores.Beatmap.HpDrain}\n" +
                                  $" > Star difficulty : {beatmapScores.Beatmap.DifficultyRating}");
                foreach (var score in beatmapScores.Score)
                {
                    Console.WriteLine($"\nScore {score.TotalScore} with {score.Accuracy}% accuracy made by {score.Username}");
                }

                var beatmapScoresUsers = await instance.GetScoresWithUsersAndBeatmapAsync(75).ConfigureAwait(false);
                Console.WriteLine($"\n\nBeatmap {beatmapScores.Beatmap.Title} with id {beatmapScores.Beatmap.BeatmapId} mapped by {beatmapScores.Beatmap.Creator}\n" +
                                  $" > Difficulty : {beatmapScores.Beatmap.Difficulty}\n" +
                                  $" > State : {beatmapScores.Beatmap.State}\n" +
                                  $" > BPM : {beatmapScores.Beatmap.Bpm}\n" +
                                  $" > AR : {beatmapScores.Beatmap.ApproachRate}\n" +
                                  $" > OD : {beatmapScores.Beatmap.OverallDifficulty}\n" +
                                  $" > CS : {beatmapScores.Beatmap.CircleSize}\n" +
                                  $" > HP : {beatmapScores.Beatmap.HpDrain}\n" +
                                  $" > Star difficulty : {beatmapScores.Beatmap.DifficultyRating}");
                foreach (var score in beatmapScores.Score)
                {
                    var currentUser = beatmapScoresUsers.Users.SingleOrDefault(x => x.Username == score.Username);
                    Console.WriteLine(currentUser != null
                        ? $"\nScore {score.TotalScore} with {score.Accuracy}% accuracy made by {currentUser.Username} that has {currentUser.Pp} performance points."
                        : $"\nScore {score.TotalScore} with {score.Accuracy}% accuracy made by {score.Username}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            await Task.Delay(Timeout.Infinite).ConfigureAwait(false);
        }
    }
}
