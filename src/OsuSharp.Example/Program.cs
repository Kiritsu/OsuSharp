using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OsuSharp.Endpoints;
using OsuSharp.Entities;
using OsuSharp.Interfaces;
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
                IOsuApi instance = new OsuApi(new OsuSharpConfiguration
                {
                    ApiKey = File.ReadAllText("token.txt"),
                    ModsSeparator = "|",
                    MaxRequests = 4,
                    TimeInterval = TimeSpan.FromSeconds(8),
                    LogLevel = LoggingLevel.Debug
                });

                instance.Logger.LogMessageReceived += (sender, args) =>
                    args.Logger.Print(args.Level, args.From, args.Message, args.Time);

                User user = await instance.GetUserByNameAsync("Evolia").ConfigureAwait(false);
                Console.WriteLine($"User {user.Username} with id {user.Userid}\n" +
                                  $" > Current accuracy : {user.Accuracy}\n" +
                                  $" > Total Score : {user.TotalScore}\n" +
                                  $" > Ranked Score : {user.RankedScore}\n" +
                                  $" > Level : {user.Level}\n" +
                                  $" > Performance Points : {user.Pp}\n" +
                                  $" > Play count : {user.PlayCount}");

                List<UserBest> bests = await instance.GetUserBestByUsernameAsync("Evolia", GameMode.Standard, 20).ConfigureAwait(false);
                int cnt = 0;
                foreach (UserBest best in bests)
                {
                    Console.WriteLine($"Top Score {cnt}:");
                    Console.WriteLine($"Accuracy: {best.Accuracy}\nMods: {best.Mods.ToModString(instance)}");
                    Console.WriteLine();
                    cnt++;
                }

                Beatmap beatmap = await instance.GetBeatmapAsync(75).ConfigureAwait(false);
                Console.WriteLine($"\n\nBeatmap {beatmap.Title} with id {beatmap.BeatmapId} mapped by {beatmap.Creator}\n" +
                                  $" > Difficulty : {beatmap.Difficulty}\n" +
                                  $" > State : {beatmap.Approved}\n" +
                                  $" > BPM : {beatmap.Bpm}\n" +
                                  $" > AR : {beatmap.ApproachRate}\n" +
                                  $" > OD : {beatmap.OverallDifficulty}\n" +
                                  $" > CS : {beatmap.CircleSize}\n" +
                                  $" > HP : {beatmap.HpDrain}\n" +
                                  $" > Star difficulty : {beatmap.DifficultyRating}\n");

                List<UserBestBeatmap> userBestsBeatmaps = await instance.GetUserBestAndBeatmapByUsernameAsync("Evolia").ConfigureAwait(false);
                foreach (UserBestBeatmap userBestBeatmap in userBestsBeatmaps)
                {
                    Console.WriteLine($"\nScore {userBestBeatmap.UserBest.ScorePoints} with {userBestBeatmap.UserBest.Accuracy} accuracy\nOn map {userBestBeatmap.Beatmap.Title} made by {userBestBeatmap.Beatmap.Creator} with difficulty {userBestBeatmap.Beatmap.Difficulty}");
                }

                BeatmapScores beatmapScores = await instance.GetScoresAndBeatmapAsync(75).ConfigureAwait(false);
                Console.WriteLine($"\n\nBeatmap {beatmapScores.Beatmap.Title} with id {beatmapScores.Beatmap.BeatmapId} mapped by {beatmapScores.Beatmap.Creator}\n" +
                                  $" > Difficulty : {beatmapScores.Beatmap.Difficulty}\n" +
                                  $" > State : {beatmapScores.Beatmap.Approved}\n" +
                                  $" > BPM : {beatmapScores.Beatmap.Bpm}\n" +
                                  $" > AR : {beatmapScores.Beatmap.ApproachRate}\n" +
                                  $" > OD : {beatmapScores.Beatmap.OverallDifficulty}\n" +
                                  $" > CS : {beatmapScores.Beatmap.CircleSize}\n" +
                                  $" > HP : {beatmapScores.Beatmap.HpDrain}\n" +
                                  $" > Star difficulty : {beatmapScores.Beatmap.DifficultyRating}");
                foreach (Score score in beatmapScores.Score)
                {
                    Console.WriteLine($"\nScore {score.ScorePoints} with {score.Accuracy}% accuracy made by {score.Username}");
                }

                BeatmapScoresUsers beatmapScoresUsers = await instance.GetScoresWithUsersAndBeatmapAsync(75).ConfigureAwait(false);
                Console.WriteLine($"\n\nBeatmap {beatmapScores.Beatmap.Title} with id {beatmapScores.Beatmap.BeatmapId} mapped by {beatmapScores.Beatmap.Creator}\n" +
                                  $" > Difficulty : {beatmapScores.Beatmap.Difficulty}\n" +
                                  $" > State : {beatmapScores.Beatmap.Approved}\n" +
                                  $" > BPM : {beatmapScores.Beatmap.Bpm}\n" +
                                  $" > AR : {beatmapScores.Beatmap.ApproachRate}\n" +
                                  $" > OD : {beatmapScores.Beatmap.OverallDifficulty}\n" +
                                  $" > CS : {beatmapScores.Beatmap.CircleSize}\n" +
                                  $" > HP : {beatmapScores.Beatmap.HpDrain}\n" +
                                  $" > Star difficulty : {beatmapScores.Beatmap.DifficultyRating}");
                foreach (Score score in beatmapScores.Score)
                {
                    User currentUser = beatmapScoresUsers.Users.SingleOrDefault(x => x.Username == score.Username);
                    Console.WriteLine(currentUser != null
                        ? $"\nScore {score.ScorePoints} with {score.Accuracy}% accuracy made by {currentUser.Username} that has {currentUser.Pp} performance points."
                        : $"\nScore {score.ScorePoints} with {score.Accuracy}% accuracy made by {score.Username}");
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
