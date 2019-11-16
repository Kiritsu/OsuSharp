using System;
using System.Collections.Generic;
using System.Linq;

namespace OsuSharp.Oppai
{
    public sealed class DiffCalculation
    {
        /// <summary>
        ///     Star rating.
        /// </summary>
        public double Total { get; set; }

        /// <summary>
        ///     Aim stars.
        /// </summary>
        public double Aim { get; set; }

        /// <summary>
        ///     Used to calc length bonus.
        /// </summary>
        public double AimDifficulty { get; set; }

        /// <summary>
        ///     Unused for now.
        /// </summary>
        public double AimlengthBonus { get; set; }

        /// <summary>
        ///     Speed stars.
        /// </summary>
        public double Speed { get; set; }

        /// <summary>
        ///     Used to calc length bonus.
        /// </summary>
        public double SpeedDifficulty { get; set; }

        /// <summary>
        ///     Unused at the moment.
        /// </summary>
        public double SpeedLengthBonus { get; set; }

        /// <summary>
        ///     Number of notes that are considered singletaps by the difficulty calculator.
        /// </summary>
        public int SinglesCount { get; set; }

        /// <summary>
        ///     Number of taps slower or equal to the singletap threshold.
        /// </summary>
        public int SinglesCountThreshold { get; set; }

        /// <summary>
        ///     Beatmap we want to calculate the difficulty for.
        /// </summary>
        public ParsedBeatmap Beatmap { get; set; }

        private double _speedMultipler;
        private readonly List<double> _strains;

        public const double DEFAULT_SINGLETAP_THRESHOLD = 125.0;

        private DiffCalculation()
        {
            _strains = new List<double>(512);

            Total = 0.0;
            Aim = 0.0;
            Speed = 0.0;

            SinglesCount = 0;
            SinglesCountThreshold = 0;

            _speedMultipler = 1.0;
        }

        private static double GetLengthBonus(double stars, double difficulty)
        {
            return 0.32 + (0.5 * (Math.Log10(difficulty + stars) - Math.Log10(stars)));
        }

        private DiffValues CalcIndividual(int type)
        {
            _strains.Clear();

            var strain_step = OppaiConsts.STRAIN_STEP * _speedMultipler;
            
            // the first object doesn't generate a strain
            // so we begin with an incremented interval end 
            var interval_end = Math.Ceiling(Beatmap.HitObjects[0].Time / strain_step) * strain_step;
            var max_strain = 0.0;

            // calculate all strains
            for (var i = 0; i < Beatmap.HitObjects.Count; ++i)
            {
                var obj = Beatmap.HitObjects[i];
                var prev = i > 0 ? Beatmap.HitObjects[i - 1] : null;

                if (prev != null)
                {
                    OppaiConsts.DStrain(type, obj, prev, _speedMultipler);
                }

                while (obj.Time > interval_end)
                {
                    // add max strain for this interval
                    _strains.Add(max_strain);

                    if (prev != null)
                    {
                        // decay last object's strains until the next
                        // interval and use that as the initial max
                        // strain 
                        var decay = Math.Pow(OppaiConsts.DECAY_BASE[type], (interval_end - prev.Time) / 1000.0);

                        max_strain = prev.Strains[type] * decay;
                    }
                    else
                    {
                        max_strain = 0.0;
                    }

                    interval_end += strain_step;
                }

                max_strain = Math.Max(max_strain, obj.Strains[type]);
            }

            // don't forget to add the last strain interval 
            _strains.Add(max_strain);

            // weigh the top strains sorted from highest to lowest 
            var weight = 1.0;
            var total = 0.0;
            var difficulty = 0.0;

            var sortedStrains = _strains.OrderByDescending(x => x);

            foreach (var strain in sortedStrains)
            {
                total += Math.Pow(strain, 1.2);
                difficulty += strain * weight;
                weight *= OppaiConsts.DECAY_WEIGHT;
            }

            return new DiffValues(difficulty, total);
        }

        /// <summary>
        ///     Calculates beatmap difficulty and stores it in total, aim,
        ///     speed, nsingles, nsingles_speed fields.
        /// </summary>
        /// <param name="mods">
        ///     Mods to be used in the calculation.
        /// </param>
        /// <param name="singletap_threshold">
        ///     The smallest milliseconds interval
        ///     that will be considered singletappable. for example,
        ///     125ms is 240 1/2 singletaps ((60000 / 240) / 2)
        /// </param>
        public static DiffCalculation Calc(ParsedBeatmap beatmap, int mods, double singletap_threshold)
        {
            var diffCalculation = new DiffCalculation();
            diffCalculation.Beatmap = beatmap;

            var mapstats = new BeatmapStats();
            mapstats.CS = diffCalculation.Beatmap.CS;
            mapstats.ApplyMods(mods, OppaiConsts.APPLY_CS);

            diffCalculation._speedMultipler = mapstats.Speed;

            var radius = OppaiConsts.PLAYFIELD_WIDTH / 16.0 * (1.0 - (0.7 * (mapstats.CS - 5.0) / 5.0));

            // positions are normalized on circle radius so that we can
            // calc as if everything was the same circlesize 
            var scaling_factor = 52.0 / radius;

            if (radius < OppaiConsts.CIRCLESIZE_BUFF_THRESHOLD)
            {
                scaling_factor *= 1.0 + (Math.Min(OppaiConsts.CIRCLESIZE_BUFF_THRESHOLD - radius, 5.0) / 50.0);
            }

            var normalized_center = new Vector2(OppaiConsts.PLAYFIELD_CENTER).Multiply(scaling_factor);

            HitObject prev1 = null;
            HitObject prev2 = null;
            var i = 0;

            // calculate normalized positions 
            foreach (var obj in diffCalculation.Beatmap.HitObjects)
            {
                if (((int)obj.Type & OppaiConsts.OBJ_SPINNER) != 0)
                {
                    obj.NormPosition = new Vector2(normalized_center);
                }

                else
                {
                    Vector2 pos;

                    if (((int)obj.Type & OppaiConsts.OBJ_SLIDER) != 0)
                    {
                        pos = ((Slider)obj.Note).Position;
                    }

                    else if (((int)obj.Type & OppaiConsts.OBJ_CIRCLE) != 0)
                    {
                        pos = ((Circle)obj.Note).Position;
                    }

                    else
                    {
                        pos = new Vector2();
                    }

                    obj.NormPosition = new Vector2(pos).Multiply(scaling_factor);
                }

                if (i >= 2)
                {
                    var v1 = new Vector2(prev2.NormPosition).Substract(prev1.NormPosition);
                    var v2 = new Vector2(obj.NormPosition).Substract(prev1.NormPosition);
                    var dot = v1.Dot(v2);
                    var det = (v1.X * v2.Y) - (v1.Y * v2.X);
                    obj.Angle = Math.Abs(Math.Atan2(det, dot));
                }
                else
                {
                    obj.Angle = double.NaN;
                }

                prev2 = prev1;
                prev1 = obj;
                ++i;
            }

            // speed and aim stars 
            var aimvals = diffCalculation.CalcIndividual(OppaiConsts.DIFF_AIM);
            diffCalculation.Aim = aimvals.Difficulty;
            diffCalculation.AimDifficulty = aimvals.Total;
            diffCalculation.AimlengthBonus = GetLengthBonus(diffCalculation.Aim, diffCalculation.AimDifficulty);

            var speedvals = diffCalculation.CalcIndividual(OppaiConsts.DIFF_SPEED);
            diffCalculation.Speed = speedvals.Difficulty;
            diffCalculation.SpeedDifficulty = speedvals.Total;
            diffCalculation.SpeedLengthBonus = GetLengthBonus(diffCalculation.Speed, diffCalculation.SpeedDifficulty);

            diffCalculation.Aim = Math.Sqrt(diffCalculation.Aim) * OppaiConsts.STAR_SCALING_FACTOR;
            diffCalculation.Speed = Math.Sqrt(diffCalculation.Speed) * OppaiConsts.STAR_SCALING_FACTOR;
            if ((mods & OppaiConsts.MODS_TOUCH_DEVICE) != 0)
            {
                diffCalculation.Aim = Math.Pow(diffCalculation.Aim, 0.8);
            }

            // total stars 
            diffCalculation.Total = diffCalculation.Aim + diffCalculation.Speed + (Math.Abs(diffCalculation.Speed - diffCalculation.Aim) * OppaiConsts.EXTREME_SCALING_FACTOR);

            /* singletap stats */
            for (i = 1; i < diffCalculation.Beatmap.HitObjects.Count; ++i)
            {
                var prev = diffCalculation.Beatmap.HitObjects[i - 1];
                var obj = diffCalculation.Beatmap.HitObjects[i];

                if (obj.IsSingle)
                {
                    ++diffCalculation.SinglesCount;
                }

                if (((int)obj.Type & (OppaiConsts.OBJ_CIRCLE | OppaiConsts.OBJ_SLIDER)) == 0)
                {
                    continue;
                }

                var interval = (obj.Time - prev.Time) / diffCalculation._speedMultipler;

                if (interval >= singletap_threshold)
                {
                    ++diffCalculation.SinglesCountThreshold;
                }
            }

            return diffCalculation;
        }

        public static DiffCalculation Calc(ParsedBeatmap beatmap, int mods)
        {
            return Calc(beatmap, mods, DEFAULT_SINGLETAP_THRESHOLD);
        }
        
        public static DiffCalculation Calc(ParsedBeatmap beatmap)
        {
            return Calc(beatmap, OppaiConsts.MODS_NOMOD, DEFAULT_SINGLETAP_THRESHOLD);
        }

        private class DiffValues
        {
            public double Difficulty { get; set; }
            public double Total { get; set; }

            public DiffValues(double difficulty, double total)
            {
                Difficulty = difficulty;
                Total = total;
            }
        }
    }
}
