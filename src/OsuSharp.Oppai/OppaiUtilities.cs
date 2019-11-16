using System;
using System.Text;

namespace OsuSharp.Oppai
{
    internal static class OppaiUtilities
    {
        public const int MODE_STD = 0;

        public const int DIFF_SPEED = 0;
        public const int DIFF_AIM = 1;

        public const int OBJ_CIRCLE = 1 << 0;
        public const int OBJ_SLIDER = 1 << 1;
        public const int OBJ_SPINNER = 1 << 3;

        public const double OD0_MS = 80;
        public const double OD10_MS = 20;
        public const double AR0_MS = 1800.0;
        public const double AR5_MS = 1200.0;
        public const double AR10_MS = 450.0;

        public const double OD_MS_STEP = (OD0_MS - OD10_MS) / 10.0;
        public const double AR_MS_STEP1 = (AR0_MS - AR5_MS) / 5.0;
        public const double AR_MS_STEP2 = (AR5_MS - AR10_MS) / 5.0;

        public const int APPLY_AR = 1 << 0;
        public const int APPLY_OD = 1 << 1;
        public const int APPLY_CS = 1 << 2;
        public const int APPLY_HP = 1 << 3;

        /// <summary>
        ///     Arbitrary thresholds to determine when a stream is spaced
        ///     enough that it becomes hard to alternate.
        /// </summary>
        public const double SINGLE_SPACING = 125.0;

        /// <summary>
        ///     Strain decay per interval. 
        /// </summary>
        public static readonly double[] DECAY_BASE = { 0.3, 0.15 };

        /// <summary>
        ///     Balances speed and aim.
        /// </summary>
        public static readonly double[] WEIGHT_SCALING = { 1400.0, 26.25 };

        /// <summary>
        ///     Max strains are weighted from highest to lowest, this is how
        ///     much the weight decays.
        /// </summary>
        public const double DECAY_WEIGHT = 0.9;

        /// <summary>
        ///     Strains are calculated by analyzing the map in chunks and taking
        ///     the peak strains in each chunk. this is the length of a strain
        ///     interval in milliseconds
        /// </summary>
        public const double STRAIN_STEP = 400.0;

        /// <summary>
        ///     Non-normalized diameter where the small circle buff starts.
        /// </summary>
        public const double CIRCLESIZE_BUFF_THRESHOLD = 30.0;

        /// <summary>
        ///     Global stars multiplier.
        /// </summary>
        public const double STAR_SCALING_FACTOR = 0.0675;

        /// <summary>
        ///     In osu! pixels.
        /// </summary>
        public const double PLAYFIELD_WIDTH = 512.0;
        public const double PLAYFIELD_HEIGHT = 384.0;

        public static readonly Vector2 PLAYFIELD_CENTER = new Vector2(PLAYFIELD_WIDTH / 2.0, PLAYFIELD_HEIGHT / 2.0);

        /// <summary>
        ///     50% of the difference between aim and speed is added to total
        ///     star rating to compensate for aim/speed only maps
        /// </summary>
        public const double EXTREME_SCALING_FACTOR = 0.5;

        public const double MIN_SPEED_BONUS = 75.0;
        public const double MAX_SPEED_BONUS = 45.0;
        public const double ANGLE_BONUS_SCALE = 90.0;
        public const double AIM_TIMING_THRESHOLD = 107;
        public const double SPEED_ANGLE_BONUS_BEGIN = 5 * Math.PI / 6;
        public const double AIM_ANGLE_BONUS_BEGIN = Math.PI / 3;

        public const int MODS_NOMOD = 0;

        public const int MODS_NF = 1 << 0;
        public const int MODS_EZ = 1 << 1;
        public const int MODS_TOUCH_DEVICE = 1 << 2;
        public const int MODS_TD = MODS_TOUCH_DEVICE;
        public const int MODS_HD = 1 << 3;
        public const int MODS_HR = 1 << 4;
        public const int MODS_DT = 1 << 6;
        public const int MODS_HT = 1 << 8;
        public const int MODS_NC = 1 << 9;
        public const int MODS_FL = 1 << 10;
        public const int MODS_SO = 1 << 12;

        public const int MODS_SPEED_CHANGING = MODS_DT | MODS_HT | MODS_NC;

        public const int MODS_MAP_CHANGING = MODS_HR | MODS_EZ | MODS_SPEED_CHANGING;

        public static string ModsToString(int mods)
        {
            var builder = new StringBuilder();

            if ((mods & MODS_NF) != 0)
            {
                builder.Append("NF");
            }

            if ((mods & MODS_EZ) != 0)
            {
                builder.Append("EZ");
            }

            if ((mods & MODS_TOUCH_DEVICE) != 0)
            {
                builder.Append("TD");
            }

            if ((mods & MODS_HD) != 0)
            {
                builder.Append("HD");
            }

            if ((mods & MODS_HR) != 0)
            {
                builder.Append("HR");
            }

            if ((mods & MODS_NC) != 0)
            {
                builder.Append("NC");
            }
            else if ((mods & MODS_DT) != 0)
            {
                builder.Append("DT");
            }

            if ((mods & MODS_HT) != 0)
            {
                builder.Append("HT");
            }

            if ((mods & MODS_FL) != 0)
            {
                builder.Append("FL");
            }

            if ((mods & MODS_SO) != 0)
            {
                builder.Append("SO");
            }

            return builder.ToString();
        }

        public static int ModsFromString(string str)
        {
            var mask = 0;

            while (str.Length > 0)
            {
                if (str.StartsWith("NF"))
                {
                    mask |= MODS_NF;
                }
                else if (str.StartsWith("EZ"))
                {
                    mask |= MODS_EZ;
                }
                else if (str.StartsWith("TD"))
                {
                    mask |= MODS_TOUCH_DEVICE;
                }
                else if (str.StartsWith("HD"))
                {
                    mask |= MODS_HD;
                }
                else if (str.StartsWith("HR"))
                {
                    mask |= MODS_HR;
                }
                else if (str.StartsWith("DT"))
                {
                    mask |= MODS_DT;
                }
                else if (str.StartsWith("HT"))
                {
                    mask |= MODS_HT;
                }
                else if (str.StartsWith("NC"))
                {
                    mask |= MODS_NC;
                }
                else if (str.StartsWith("FL"))
                {
                    mask |= MODS_FL;
                }
                else if (str.StartsWith("SO"))
                {
                    mask |= MODS_SO;
                }
                else
                {
                    str = str.Substring(1);
                    continue;
                }
                str = str.Substring(2);
            }

            return mask;
        }

        /// <summary>
        ///     Apply mods to a <see cref="BeatmapStats"/>.
        /// </summary>
        /// <param name="beatmapStats">The base <see cref="BeatmapStats"/>.</param>
        /// <param name="mods">Mods you want to add to the <see cref="BeatmapStats"/>.</param>
        /// <param name="flags">Bitmask that specifies which stats to modify. Only the stats specified here need to be initialized in <see cref="BeatmapStats"/>.</param>
        public static BeatmapStats ApplyMods(this BeatmapStats beatmapStats, int mods, int flags)
        {
            beatmapStats.Speed = 1.0f;

            if ((mods & MODS_MAP_CHANGING) == 0)
            {
                return beatmapStats;
            }

            if ((mods & (MODS_DT | MODS_NC)) != 0)
            {
                beatmapStats.Speed = 1.5f;
            }

            if ((mods & MODS_HT) != 0)
            {
                beatmapStats.Speed *= 0.75f;
            }

            var od_ar_hp_multiplier = 1.0f;

            if ((mods & MODS_HR) != 0)
            {
                od_ar_hp_multiplier = 1.4f;
            }

            if ((mods & MODS_EZ) != 0)
            {
                od_ar_hp_multiplier *= 0.5f;
            }

            if ((flags & APPLY_AR) != 0)
            {
                beatmapStats.AR *= od_ar_hp_multiplier;

                // convert AR into milliseconds window 
                var arms = beatmapStats.AR < 5.0f
                    ? AR0_MS - (AR_MS_STEP1 * beatmapStats.AR)
                    : AR5_MS - (AR_MS_STEP2 * (beatmapStats.AR - 5.0f));

                // stats must be capped to 0-10 before HT/DT which brings
                // them to a range of -4.42->11.08 for OD and -5->11 for AR 
                arms = Math.Min(AR0_MS, Math.Max(AR10_MS, arms));
                arms /= beatmapStats.Speed;

                beatmapStats.AR = (float)(
                    arms > AR5_MS
                    ? (AR0_MS - arms) / AR_MS_STEP1
                    : 5.0 + ((AR5_MS - arms) / AR_MS_STEP2)
                );
            }

            if ((flags & APPLY_OD) != 0)
            {
                beatmapStats.OD *= od_ar_hp_multiplier;
                var odms = OD0_MS - Math.Ceiling(OD_MS_STEP * beatmapStats.OD);
                odms = Math.Min(OD0_MS, Math.Max(OD10_MS, odms));
                odms /= beatmapStats.Speed;
                beatmapStats.OD = (float)((OD0_MS - odms) / OD_MS_STEP);
            }

            if ((flags & APPLY_CS) != 0)
            {
                if ((mods & MODS_HR) != 0)
                {
                    beatmapStats.CS *= 1.3f;
                }

                if ((mods & MODS_EZ) != 0)
                {
                    beatmapStats.CS *= 0.5f;
                }

                beatmapStats.CS = Math.Min(10.0f, beatmapStats.CS);
            }

            if ((flags & APPLY_HP) != 0)
            {
                beatmapStats.HP = Math.Min(10.0f, beatmapStats.HP * od_ar_hp_multiplier);
            }

            return beatmapStats;
        }

        public static double DSpacingWeight(int type, double distance, double deltaTime, double prevDistance, double prevDeltaTime, double angle)
        {
            var strain_time = Math.Max(deltaTime, 50.0);
            var prev_strain_time = Math.Max(prevDeltaTime, 50.0);

            double angle_bonus;
            switch (type)
            {
                case DIFF_AIM:
                    {
                        var result = 0.0;
                        if (!double.IsNaN(angle) && angle > AIM_ANGLE_BONUS_BEGIN)
                        {
                            angle_bonus = Math.Sqrt(Math.Max(prevDistance - ANGLE_BONUS_SCALE, 0.0) * Math.Pow(Math.Sin(angle - AIM_ANGLE_BONUS_BEGIN), 2.0) * Math.Max(distance - ANGLE_BONUS_SCALE, 0.0));
                            result = 1.5 * Math.Pow(Math.Max(0.0, angle_bonus), 0.99) / Math.Max(AIM_TIMING_THRESHOLD, prev_strain_time);
                        }

                        var weighted_distance = Math.Pow(distance, 0.99);

                        return Math.Max(result + (weighted_distance / Math.Max(AIM_TIMING_THRESHOLD, strain_time)), weighted_distance / strain_time);
                    }

                case DIFF_SPEED:
                    {
                        distance = Math.Min(distance, SINGLE_SPACING);
                        deltaTime = Math.Max(deltaTime, MAX_SPEED_BONUS);

                        var speed_bonus = 1.0;

                        if (deltaTime < MIN_SPEED_BONUS)
                        {
                            speed_bonus += Math.Pow((MIN_SPEED_BONUS - deltaTime) / 40.0, 2);
                        }

                        angle_bonus = 1.0;

                        if (!double.IsNaN(angle) && angle < SPEED_ANGLE_BONUS_BEGIN)
                        {
                            var s = Math.Sin(1.5 * (SPEED_ANGLE_BONUS_BEGIN - angle));
                            angle_bonus += Math.Pow(s, 2) / 3.57;
                            if (angle < Math.PI / 2.0)
                            {
                                angle_bonus = 1.28;
                                if (distance < ANGLE_BONUS_SCALE && angle < Math.PI / 4.0)
                                {
                                    angle_bonus += (1.0 - angle_bonus) * Math.Min((ANGLE_BONUS_SCALE - distance) / 10.0, 1.0);
                                }
                                else if (distance < ANGLE_BONUS_SCALE)
                                {
                                    angle_bonus += (1.0 - angle_bonus) * Math.Min((ANGLE_BONUS_SCALE - distance) / 10.0, 1.0) * Math.Sin(((Math.PI / 2.0) - angle) * 4.0 / Math.PI);
                                }
                            }
                        }

                        return (1 + ((speed_bonus - 1) * 0.75)) * angle_bonus * (0.95 + (speed_bonus * Math.Pow(distance / SINGLE_SPACING, 3.5))) / strain_time;
                    }
            }

            throw new InvalidOperationException("This difficulty type does not exist.");
        }

        /// <summary>
        ///     Calculates the strain for one difficulty type and stores it in
        ///     obj. this assumes that normpos is already computed.
        ///     this also sets is_single if type is DIFF_SPEED
        /// </summary>
        public static void DStrain(int type, HitObject obj, HitObject prev, double speed_mul)
        {
            var value = 0.0;
            var time_elapsed = (obj.Time - prev.Time) / speed_mul;
            var decay =
                Math.Pow(DECAY_BASE[type], time_elapsed / 1000.0);

            obj.DeltaTime = time_elapsed;

            // this implementation doesn't account for sliders
            if (((int)obj.Type & (OBJ_SLIDER | OBJ_CIRCLE)) != 0)
            {
                var distance = new Vector2(obj.NormPosition).Substract(prev.NormPosition).Length;
                obj.DDistance = distance;

                if (type == DIFF_SPEED)
                {
                    obj.IsSingle = distance > SINGLE_SPACING;
                }

                value = DSpacingWeight(type, distance, time_elapsed, prev.DDistance, prev.DeltaTime, obj.Angle);
                value *= WEIGHT_SCALING[type];
            }

            obj.Strains[type] = (prev.Strains[type] * decay) + value;
        }

        /// <summary>
        ///     Returns base pp given the star rating.
        /// </summary>
        public static double PPBase(double stars)
        {
            return Math.Pow((5.0 * Math.Max(1.0, stars / 0.0675)) - 4.0, 3.0) / 100000.0;
        }
    }
}
