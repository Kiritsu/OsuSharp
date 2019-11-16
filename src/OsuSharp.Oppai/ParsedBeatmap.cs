using System;
using System.Collections.Generic;

namespace OsuSharp.Oppai
{
    public sealed class ParsedBeatmap
    {
        public int FormatVersion { get; internal set; }
        public int Mode { get; internal set; }

        public string Title { get; internal set; }
        public string TitleUnicode { get; internal set; }

        public string Artist { get; internal set; }
        public string ArtistUnicode { get; internal set; }

        public string Creator { get; internal set; }
        public string Difficulty { get; internal set; }

        public int NbCircles { get; internal set; }
        public int NbSliders { get; internal set; }
        public int NbSpinners { get; internal set; }
        
        public float HP { get; internal set; }
        public float CS { get; internal set; }
        public float OD { get; internal set; }
        public float AR { get; internal set; }

        public float SV { get; internal set; }
        public float TickRate { get; internal set; }

        public List<HitObject> HitObjects { get; internal set; } 
        public List<Timing> TimingPoints { get; internal set; }

        public ParsedBeatmap()
        {
            HitObjects = new List<HitObject>(512);
            TimingPoints = new List<Timing>(32);

            Title = "";
            TitleUnicode = "";
            Artist = "";
            ArtistUnicode = "";

            Creator = "";
            Difficulty = "";

            NbCircles = 0;
            NbSliders = 0;
            NbSpinners = 0;

            HP = 5.0F;
            CS = 5.0F;
            OD = 5.0F;
            AR = 5.0F;

            SV = 1.0F;
            TickRate = 1.0F;
        }

        /// <summary>
        ///     Gets the max combo for this beatmap.
        /// </summary>
        internal int GetMaxCombo()
        {
            var maxCombo = 0;
            var tIndex = -1;
            var tNext = double.NegativeInfinity;
            var pxPerBeat = 0.0;

            foreach (var hitObject in HitObjects)
            {
                if (hitObject.Type != NoteType.Slider)
                {
                    ++maxCombo;

                    continue;
                }

                while (hitObject.Time >= tNext)
                {
                    ++tIndex;

                    tNext = TimingPoints.Count > tIndex + 1 
                        ? TimingPoints[tIndex + 1].Time 
                        : double.PositiveInfinity;

                    var timingPoint = TimingPoints[tIndex];
                    var svMultiplier = 1.0;

                    if (!timingPoint.Change && timingPoint.MsPerBeat < 0)
                    {
                        svMultiplier = -100.0 / timingPoint.MsPerBeat;
                    }

                    pxPerBeat = SV * 100.0 * svMultiplier;
                    if (FormatVersion < 8)
                    {
                        pxPerBeat /= svMultiplier;
                    }
                }

                var slider = (Slider)hitObject.Note;

                var nbBeats = slider.Distance * slider.Repetition / pxPerBeat;
                var ticks = (int)Math.Ceiling((nbBeats - 0.1) / slider.Repetition * TickRate);

                --ticks;

                ticks *= slider.Repetition;
                ticks += slider.Repetition + 1;

                maxCombo += Math.Max(0, ticks);
            }

            return maxCombo;
        }
    }
}
