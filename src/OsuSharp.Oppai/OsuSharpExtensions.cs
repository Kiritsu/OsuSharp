using System.Net.Http;
using System.Threading.Tasks;

namespace OsuSharp.Oppai
{
    public static class OsuSharpExtensions
    {
        private static readonly HttpClient _httpClient;

        static OsuSharpExtensions()
        {
            _httpClient = new HttpClient();
        }

        public static async Task<PPv2> GetPPv2Async(this Score score)
        {
            var beatmap = await score.GetBeatmapAsync();
            var bm = BeatmapParser.Parse(await _httpClient.GetStringAsync(beatmap.BeatmapDownloadUri));
            var mods = score.Mods.ToModInt();
            var difficulty = DiffCalculation.Calc(bm, mods);
            var ppParams = new PPv2Parameters
            {
                Beatmap = bm,
                AimStars = difficulty.Aim,
                SpeedStars = difficulty.Speed,
                Mods = mods,
                //MaxCombo = bm.GetMaxCombo(),
                BaseAR = bm.AR,
                BaseOD = bm.OD,
                CircleCount = bm.NbCircles,
                SliderCount = bm.NbSliders,
                GameMode = bm.Mode,
                ObjectCount = bm.HitObjects.Count,
                //Combo = score.MaxCombo.GetValueOrDefault(-1),
                Count100 = score.Count100,
                Count300 = score.Count300,
                Count50 = score.Count50,
                CountMiss = score.Miss
            };
            
            return new PPv2(ppParams);
        }

        public static async Task<PPv2> GetPPv2Async(this Beatmap beatmap)
        {
            var bm = BeatmapParser.Parse(await _httpClient.GetStringAsync(beatmap.BeatmapDownloadUri));
            var difficulty = DiffCalculation.Calc(bm);

            return new PPv2(difficulty.Aim, difficulty.Speed, bm);
        }

        public static async Task<PPv2> GetPPv2Async(this Beatmap beatmap, Mode modes)
        {
            var bm = BeatmapParser.Parse(await _httpClient.GetStringAsync(beatmap.BeatmapDownloadUri));

            var mods = modes.ToModInt();
            var difficulty = DiffCalculation.Calc(bm, mods);

            var ppParams = new PPv2Parameters
            {
                Beatmap = bm,
                AimStars = difficulty.Aim,
                SpeedStars = difficulty.Speed,
                Mods = mods,
                //MaxCombo = bm.GetMaxCombo(),
                BaseAR = bm.AR,
                BaseOD = bm.OD,
                CircleCount = bm.NbCircles,
                SliderCount = bm.NbSliders,
                GameMode = bm.Mode,
                ObjectCount = bm.HitObjects.Count
            };

            return new PPv2(ppParams);
        }

        public static async Task<PPv2> GetPPv2Async(this Beatmap beatmap, float accuracy)
        {
            var bm = BeatmapParser.Parse(await _httpClient.GetStringAsync(beatmap.BeatmapDownloadUri));

            var difficulty = DiffCalculation.Calc(bm);

            var ppParams = new PPv2Parameters
            {
                Beatmap = bm,
                AimStars = difficulty.Aim,
                SpeedStars = difficulty.Speed,
                //MaxCombo = bm.GetMaxCombo(),
                BaseAR = bm.AR,
                BaseOD = bm.OD,
                CircleCount = bm.NbCircles,
                SliderCount = bm.NbSliders,
                GameMode = bm.Mode,
                ObjectCount = bm.HitObjects.Count,
                Accuracy = accuracy
            };

            return new PPv2(ppParams);
        }

        public static async Task<PPv2> GetPPv2Async(this Beatmap beatmap, Mode modes, float accuracy)
        {
            var bm = BeatmapParser.Parse(await _httpClient.GetStringAsync(beatmap.BeatmapDownloadUri));

            var mods = modes.ToModInt();
            var difficulty = DiffCalculation.Calc(bm, mods);

            var ppParams = new PPv2Parameters
            {
                Beatmap = bm,
                AimStars = difficulty.Aim,
                SpeedStars = difficulty.Speed,
                Mods = mods,
                //MaxCombo = bm.GetMaxCombo(),
                BaseAR = bm.AR,
                BaseOD = bm.OD,
                CircleCount = bm.NbCircles,
                SliderCount = bm.NbSliders,
                GameMode = bm.Mode,
                ObjectCount = bm.HitObjects.Count,
                Accuracy = accuracy
            };

            return new PPv2(ppParams);
        }

        private static int ToModInt(this Mode mode)
        {
            var flags = OppaiUtilities.MODS_NOMOD;

            if (mode.HasFlag(Mode.DoubleTime))
            {
                flags |= OppaiUtilities.MODS_DT;
            }
            if (mode.HasFlag(Mode.Easy))
            {
                flags |= OppaiUtilities.MODS_EZ;
            }
            if (mode.HasFlag(Mode.Flashlight))
            {
                flags |= OppaiUtilities.MODS_FL;
            }
            if (mode.HasFlag(Mode.Hidden))
            {
                flags |= OppaiUtilities.MODS_HD;
            }
            if (mode.HasFlag(Mode.HardRock))
            {
                flags |= OppaiUtilities.MODS_HR;
            }
            if (mode.HasFlag(Mode.HalfTime))
            {
                flags |= OppaiUtilities.MODS_HT;
            }
            if (mode.HasFlag(Mode.Nightcore))
            {
                flags |= OppaiUtilities.MODS_NC;
            }
            if (mode.HasFlag(Mode.NoFail))
            {
                flags |= OppaiUtilities.MODS_NF;
            }
            if (mode.HasFlag(Mode.SpunOut))
            {
                flags |= OppaiUtilities.MODS_SO;
            }

            return flags;
        }
    }
}
