using System;
using System.Collections.Generic;

namespace OsuSharp.Oppai
{
    public sealed class ParsedBeatmap
    {
        public int FormatVersion { get; set; }
        public int Mode { get; set; }

        public string Title { get; set; }
        public string TitleUnicode { get; set; }

        public string Artist { get; set; }
        public string ArtistUnicode { get; set; }

        public string Creator { get; set; }
        public string Difficulty { get; set; }

        public int NbCircles { get; set; }
        public int NbSliders { get; set; }
        public int NbSpinners { get; set; }
        
        public float HP { get; set; }
        public float CS { get; set; }
        public float OD { get; set; }
        public float AR { get; set; }

        public float SV { get; set; }
        public float TickRate { get; set; }

        public List<HitObject> HitObjects { get; set; } 
        public List<Timing> TimingPoints { get; set; }

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
        public int GetMaxCombo()
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
