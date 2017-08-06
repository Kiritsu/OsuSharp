using System;
using OsuSharp.Common;
using System.Threading.Tasks;

namespace OsuSharp.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            TestUserId();
            Console.ReadLine();
        }

        public static async void TestUserId()
        {
            OsuApi.Init("API KEY HERE!");
            try
            {
                var apiUser = await OsuApi.GetUserByNameAsync("Evolia", OsuMode.Standard);
                Console.WriteLine("Test : " + apiUser.Accuracy);
                Console.WriteLine("Test : " + apiUser.ARank);
                Console.WriteLine("Test : " + apiUser.Count100);
                Console.WriteLine("Test : " + apiUser.Count300);
                Console.WriteLine("Test : " + apiUser.Count50);
                Console.WriteLine("Test : " + apiUser.Country);
                Console.WriteLine("Test : " + apiUser.Events);
                Console.WriteLine("Test : " + apiUser.GlobalRank);
                Console.WriteLine("Test : " + apiUser.Level);
                Console.WriteLine("Test : " + apiUser.PlayCount);
                Console.WriteLine("Test : " + apiUser.Pp);
                Console.WriteLine("Test : " + apiUser.RankedScore);
                Console.WriteLine("Test : " + apiUser.RegionalRank);
                Console.WriteLine("Test : " + apiUser.SRank);
                Console.WriteLine("Test : " + apiUser.SSRank);
                Console.WriteLine("Test : " + apiUser.Userid);
                Console.WriteLine("Test : " + apiUser.Username);
                await Task.Delay(1000);
                var apiUserRecent = await OsuApi.GetUserRecentByUsernameAsync("Evolia", OsuMode.Standard, 1);
                foreach (var recent in apiUserRecent)
                {
                    Console.WriteLine("Test : " + recent.BeatmapId);
                    Console.WriteLine("Test : " + recent.Count100);
                    Console.WriteLine("Test : " + recent.Count300);
                    Console.WriteLine("Test : " + recent.Count50);
                    Console.WriteLine("Test : " + recent.Date);
                    Console.WriteLine("Test : " + recent.Geki);
                    Console.WriteLine("Test : " + recent.Katu);
                    Console.WriteLine("Test : " + recent.MaxCombo);
                    Console.WriteLine("Test : " + recent.Miss);
                    Console.WriteLine("Test : " + recent.Mods);
                    Console.WriteLine("Test : " + recent.Perfect);
                    Console.WriteLine("Test : " + recent.Rank);
                    Console.WriteLine("Test : " + recent.Userid);
                }
                await Task.Delay(1000);
                var apiBeatmap = await OsuApi.GetBeatmapAsync(625193, BeatmapType.ByBeatmap);
                Console.WriteLine("Test : " + apiBeatmap.Approved);
                Console.WriteLine("Test : " + apiBeatmap.ApprovedDate);
                Console.WriteLine("Test : " + apiBeatmap.AR);
                Console.WriteLine("Test : " + apiBeatmap.Artist);
                Console.WriteLine("Test : " + apiBeatmap.BeatmapID);
                Console.WriteLine("Test : " + apiBeatmap.BeatmapsetID);
                Console.WriteLine("Test : " + apiBeatmap.BPM);
                Console.WriteLine("Test : " + apiBeatmap.Creator);
                Console.WriteLine("Test : " + apiBeatmap.CS);
                Console.WriteLine("Test : " + apiBeatmap.DifficultyRating);
                Console.WriteLine("Test : " + apiBeatmap.FavouriteCount);
                Console.WriteLine("Test : " + apiBeatmap.FileMD5);
                Console.WriteLine("Test : " + apiBeatmap.GenreId);
                Console.WriteLine("Test : " + apiBeatmap.HitLength);
                Console.WriteLine("Test : " + apiBeatmap.HP);
                Console.WriteLine("Test : " + apiBeatmap.LanguageId);
                Console.WriteLine("Test : " + apiBeatmap.LastUpdate);
                Console.WriteLine("Test : " + apiBeatmap.MaxCombo);
                Console.WriteLine("Test : " + apiBeatmap.Mode);
                Console.WriteLine("Test : " + apiBeatmap.OD);
                Console.WriteLine("Test : " + apiBeatmap.PassCount);
                Console.WriteLine("Test : " + apiBeatmap.PlayCount);
                Console.WriteLine("Test : " + apiBeatmap.Source);
                Console.WriteLine("Test : " + apiBeatmap.Tags);
                Console.WriteLine("Test : " + apiBeatmap.Title);
                Console.WriteLine("Test : " + apiBeatmap.TotalLength);
                Console.WriteLine("Test : " + apiBeatmap.Version);
                await Task.Delay(1000);
                var apiUserBest = await OsuApi.GetUserBestByUsernameAsync("Evolia", OsuMode.Standard, 1);
                foreach (var best in apiUserBest)
                {
                    Console.WriteLine("Test : " + best.BeatmapId);
                    Console.WriteLine("Test : " + best.Count100);
                    Console.WriteLine("Test : " + best.Count300);
                    Console.WriteLine("Test : " + best.Count50);
                    Console.WriteLine("Test : " + best.Date);
                    Console.WriteLine("Test : " + best.Geki);
                    Console.WriteLine("Test : " + best.Katu);
                    Console.WriteLine("Test : " + best.MaxCombo);
                    Console.WriteLine("Test : " + best.Miss);
                    Console.WriteLine("Test : " + best.Mods);
                    Console.WriteLine("Test : " + best.Perfect);
                    Console.WriteLine("Test : " + best.Rank);
                    Console.WriteLine("Test : " + best.Userid);
                    Console.WriteLine("Test : " + best.Pp);
                    Console.WriteLine("Test : " + best.Score);
                }
                await Task.Delay(1000);
                var apiScore = await OsuApi.GetScoreByUsernameAsync(774965, "Cookiezi", OsuMode.Standard);
                Console.WriteLine("Test : " + apiScore.Pp);
                Console.WriteLine("Test : " + apiScore.Count100);
                Console.WriteLine("Test : " + apiScore.Count300);
                Console.WriteLine("Test : " + apiScore.Count50);
                Console.WriteLine("Test : " + apiScore.Date);
                Console.WriteLine("Test : " + apiScore.Geki);
                Console.WriteLine("Test : " + apiScore.Katu);
                Console.WriteLine("Test : " + apiScore.MaxCombo);
                Console.WriteLine("Test : " + apiScore.Miss);
                Console.WriteLine("Test : " + apiScore.Mods);
                Console.WriteLine("Test : " + apiScore.Perfect);
                Console.WriteLine("Test : " + apiScore.Rank);
                Console.WriteLine("Test : " + apiScore.Userid);
                Console.WriteLine("Test : " + apiScore.Username);
                Console.WriteLine("Test : " + apiScore.Pp);
                Console.WriteLine("Test : " + apiScore.Score);
                Console.WriteLine("Test : " + apiScore.ScoreId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
