using System;

namespace OsuSharp.Oppai
{
    public sealed class PPv2
    {
        public double Total { get; internal set; }

        public double Aim { get; internal set; }
        public double Speed { get; internal set; }
        public double Accuracy { get; internal set; }

        public Accuracy ComputedAccuracy { get; internal set; }

        private PPv2(double aimStars, double speedStars, int maxCombo, int circleCount, int sliderCount, int objectCount, float baseAr, float baseOd, int gameMode, int mods, int combo, int count300, int count100, int count50, int countMiss, int scoreVersion, ParsedBeatmap beatmap, double accu = -1F)
        {
            if (beatmap != null)
            {
                gameMode = beatmap.Mode;
                baseAr = beatmap.AR;
                baseOd = beatmap.OD;
                maxCombo = beatmap.GetMaxCombo();
                sliderCount = beatmap.NbSliders;
                circleCount = beatmap.NbCircles;
                objectCount = beatmap.HitObjects.Count;
            }

            if (gameMode != OppaiUtilities.MODE_STD)
            {
                throw new InvalidOperationException("This gamemode is not supported yet.");
            }

            if (maxCombo <= 0)
            {
                maxCombo = 1;
            }

            if (combo < 0)
            {
                combo = maxCombo - countMiss;
            }

            if (count300 < 0)
            {
                count300 = objectCount - count100 - count50 - countMiss;
            }

            ComputedAccuracy = new Accuracy(count300, count100, count50, countMiss);
            var accuracy = accu < 0 ? ComputedAccuracy.GetAccuracy() : accu;
            var realAccuracy = accuracy;

            switch (scoreVersion)
            {
                case 1:
                    var spinnerCount = objectCount - sliderCount - circleCount;
                    realAccuracy = new Accuracy(count300 - sliderCount - spinnerCount, count100, count50, countMiss).GetAccuracy();
                    realAccuracy = Math.Max(0.0, realAccuracy);
                    break;
                case 2:
                    circleCount = objectCount;
                    break;
                default:
                    throw new InvalidOperationException($"This score version is not supported: {scoreVersion}.");
            }

            var objectCountOver2k = objectCount / 2000.0;
            var lengthBonus = 0.95 + (0.4 * Math.Min(1.0, objectCountOver2k));

            if (objectCount > 2000)
            {
                lengthBonus += Math.Log10(objectCountOver2k) * 0.5;
            }

            var missPenalty = Math.Pow(0.97, countMiss);
            var comboBreak = Math.Pow(combo, 0.8) / Math.Pow(maxCombo, 0.8);

            var mapStats = new BeatmapStats
            {
                AR = baseAr,
                OD = baseOd
            };

            mapStats.ApplyMods(mods, OppaiUtilities.APPLY_AR | OppaiUtilities.APPLY_OD);

            var arBonus = 1.0;
            if (mapStats.AR > 10.33)
            {
                arBonus += 0.3 * (mapStats.AR - 10.33);
            }
            else if (mapStats.AR < 8)
            {
                arBonus += 0.01 * (8.0 - mapStats.AR);
            }

            Aim = OppaiUtilities.PPBase(aimStars);
            Aim *= lengthBonus;
            Aim *= missPenalty;
            Aim *= comboBreak;
            Aim *= arBonus;

            var hdBonus = 1.0;
            if ((mods & OppaiUtilities.MODS_HD) != 0)
            {
                hdBonus *= 1.0 + (0.04 * (12.0 - mapStats.AR));
            }

            Aim *= hdBonus;

            if ((mods & OppaiUtilities.MODS_FL) != 0)
            {
                var flBonus = 1.0 + (0.35 * Math.Min(1.0, objectCount / 200.0));
                if (objectCount > 200)
                {
                    flBonus += 0.3 * Math.Min(1.0, (objectCount - 200) / 300.0);
                }
                if (objectCount > 500)
                {
                    flBonus += (objectCount - 500) / 1200.0;
                }
                Aim *= flBonus;
            }

            var accBonus = 0.5 + (accuracy / 2.0);
            var od_squared = mapStats.OD * mapStats.OD;
            var odBonus = 0.98 + (od_squared / 2500.0);

            Aim *= accBonus;
            Aim *= odBonus;

            Speed = OppaiUtilities.PPBase(speedStars);
            Speed *= lengthBonus;
            Speed *= missPenalty;
            Speed *= comboBreak;

            if (mapStats.AR > 10.33)
            {
                Speed *= arBonus;
            }

            Speed *= hdBonus;

            Speed *= 0.02 + accuracy;
            Speed *= 0.96 + (od_squared / 1600.0);

            Accuracy = Math.Pow(1.52163, mapStats.OD) * Math.Pow(realAccuracy, 24.0) * 2.83;
            Accuracy *= Math.Min(1.15, Math.Pow(circleCount / 1000.0, 0.3));

            if ((mods & OppaiUtilities.MODS_HD) != 0)
            {
                Accuracy *= 1.08;
            }

            if ((mods & OppaiUtilities.MODS_FL) != 0)
            {
                Accuracy *= 1.02;
            }

            var final_multiplier = 1.12;

            if ((mods & OppaiUtilities.MODS_NF) != 0)
            {
                final_multiplier *= 0.90;
            }

            if ((mods & OppaiUtilities.MODS_SO) != 0)
            {
                final_multiplier *= 0.95;
            }

            Total = Math.Pow(Math.Pow(Aim, 1.1) + Math.Pow(Speed, 1.1) + Math.Pow(Accuracy, 1.1), 1.0 / 1.1) * final_multiplier;
        }

        public PPv2(PPv2Parameters parameters) : this(parameters.AimStars, parameters.SpeedStars, parameters.MaxCombo, parameters.CircleCount, parameters.SliderCount, parameters.ObjectCount, parameters.BaseAR, parameters.BaseOD, parameters.GameMode, parameters.Mods, parameters.Combo, parameters.Count300, parameters.Count100, parameters.Count50, parameters.CountMiss, parameters.ScoreVersion, parameters.Beatmap, parameters.Accuracy)
        {

        }

        public PPv2(double aimStars, double speedStars, ParsedBeatmap beatmap) : this(aimStars, speedStars, -1, beatmap.NbCircles, beatmap.NbSliders, beatmap.HitObjects.Count, beatmap.AR, beatmap.OD, beatmap.Mode, OppaiUtilities.MODS_NOMOD, -1, -1, 0, 0, 0, 1, beatmap)
        {

        }
    }
}
