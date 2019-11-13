using System;
using System.Threading.Tasks;

namespace OsuSharp.Example
{
    internal class Program
    {
        private static async Task Main()
        {
            var client = new OsuClient(new OsuSharpConfiguration
            {
                ApiKey = "yo token"
            });

            client.Logger.LogMessageReceived += Logger_LogMessageReceived;

            var bm1 = await client.GetBeatmapByHashAsync("86d35e59965dbf2078a0843f87415ebe"); //EXTREME FUCKING SOCA PARTY, Renard, Snaggletooth, Nogard's Extra
            var bm2 = await client.GetBeatmapByIdAsync(824242); //EXTREME FUCKING SOCA PARTY, Renard, Snaggletooth, Nogard's Extra
            var bs3 = await client.GetBeatmapsAsync(); //Last 500 beatmaps submitted
            var bs4 = await client.GetBeatmapsAsync(DateTimeOffset.UtcNow - TimeSpan.FromDays(365)); //Bm submitted 1 year ago
            var bs5 = await client.GetBeatmapsAsync(DateTimeOffset.UtcNow - TimeSpan.FromDays(365), GameMode.Standard); //Bm submitted 1 year ago and only standard
            var bs6 = await client.GetBeatmapsAsync(GameMode.Taiko, false); //Latest 500 taiko map not converted
            var bs7 = await client.GetBeatmapsByAuthorIdAsync(19048); //Beatmaps by Mismagius
            var bs8 = await client.GetBeatmapsByAuthorUsernameAsync("DJPop"); //Beatmaps by DJPop
            var bs9 = await client.GetBeatmapsetAsync(1391); //Kanbu de Todomatte Sugu Tokeru ~ Kyouki no Udongein, IOSYS, DJPop

            var u1 = await client.GetUserByUserIdAsync(2363, GameMode.Standard);
            var u2 = await client.GetUserByUserIdAsync(6170507, GameMode.Taiko);
            var u3 = await client.GetUserByUsernameAsync("Mismagius", GameMode.Standard);
            var u4 = await client.GetUserByUsernameAsync("Exgon", GameMode.Catch);

            var ub1 = await client.GetUserBestsByUserIdAsync(6170507, GameMode.Taiko, 34);
            var ub2 = await client.GetUserBestsByUserIdAsync(10785994, GameMode.Mania);
            var ub3 = await client.GetUserBestsByUsernameAsync("Evolia", GameMode.Catch, 82);
            var ub4 = await client.GetUserBestsByUsernameAsync("LaChipsNinja", GameMode.Standard);

            var ur1 = await client.GetUserRecentsByUserIdAsync(6170507, GameMode.Taiko);
            var ur2 = await client.GetUserRecentsByUserIdAsync(10785994, GameMode.Mania);
            var ur3 = await client.GetUserRecentsByUsernameAsync("Evolia", GameMode.Catch, 82);
            var ur4 = await client.GetUserRecentsByUsernameAsync("LaChipsNinja", GameMode.Standard);

            var s1 = await client.GetScoresByBeatmapIdAndUserIdAsync(611753, 6170507, GameMode.Standard);
            var s2 = await client.GetScoresByBeatmapIdAndUsernameAsync(40017, "Rucker", GameMode.Standard);
            var s3 = await client.GetScoresByBeatmapIdAndUserIdAsync(40017, 284905, GameMode.Standard, Mode.Hidden | Mode.Flashlight);
            var s4 = await client.GetScoresByBeatmapIdAndUsernameAsync(40017, "Ekoro", GameMode.Standard, Mode.Hidden | Mode.Flashlight);

            var sb1 = await client.GetScoresByBeatmapId(1849148, GameMode.Standard);
            var sb2 = await client.GetScoresByBeatmapId(1849148, GameMode.Standard, Mode.Hidden);

            var r1 = await client.GetReplayByUsernameAsync(1849148, "twoj stary", GameMode.Standard);
            var r2 = await client.GetReplayByUserIdAsync(1849148, 1516650, GameMode.Standard);

            var mp1 = await client.GetMultiplayerRoomAsync(1936471);
        }

        private static void Logger_LogMessageReceived(string obj)
        {
            Console.WriteLine(DateTime.Now.ToString("G") + " " + obj);
        }
    }
}
