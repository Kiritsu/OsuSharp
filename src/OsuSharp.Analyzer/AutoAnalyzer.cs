using System;
using System.Threading;
using System.Threading.Tasks;
using OsuSharp.Interfaces;

namespace OsuSharp.Analyzer
{
    public sealed class AutoAnalyzer
    {
        private TimeSpan Delay { get; set; }

        public UserAnalyzer UserAnalyzer { get; }
        public BestScoreAnalyzer BestScoreAnalyzer { get; }
        public RecentScoreAnalyzer RecentScoreAnalyzer { get; }

        public AutoAnalyzer(IOsuApi api, TimeSpan delay)
        {
            Delay = delay;

            UserAnalyzer = new UserAnalyzer(api);
            BestScoreAnalyzer = new BestScoreAnalyzer(api);
            RecentScoreAnalyzer = new RecentScoreAnalyzer(api);
        }

        public async Task StartAsync(CancellationToken token = default)
        {
            while (!token.IsCancellationRequested)
            {
                await Task.Delay(Delay, token);

                foreach (var user in UserAnalyzer.Entities)
                {
                    await UserAnalyzer.UpdateEntityAsync(user.Key, token);
                }

                foreach (var best in BestScoreAnalyzer.Entities)
                {
                    await BestScoreAnalyzer.UpdateEntityAsync(best.Key, token);
                }

                foreach (var recent in RecentScoreAnalyzer.Entities)
                {
                    await RecentScoreAnalyzer.UpdateEntityAsync(recent.Key, token);
                }
            }
        }
    }
}
