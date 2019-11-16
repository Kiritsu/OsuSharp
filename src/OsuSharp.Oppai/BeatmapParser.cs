using System;

namespace OsuSharp.Oppai
{
    public sealed class BeatmapParser
    {
        public int NbLine { get; set; }
        public string LastLine { get; set; }

        public string LastPosition
        {
            get => _lastPosition;
            set
            {
                value = value.Trim();
                _lastPosition = value;
            }
        }
        private string _lastPosition;

        public bool Parsed { get; set; }

        public ParsedBeatmap Beatmap { get; set; }

        public string Section { get; set; }
        public bool ArFound { get; set; }

        private BeatmapParser()
        {
            NbLine = 0;
            LastLine = "";
            LastPosition = "";
            Parsed = false;

            Beatmap = new ParsedBeatmap();

            Section = "";
            ArFound = false;
        }

        private string[] GetLineProperties()
        {
            var split = LastLine.Split(new char[] { ':' }, 2);

            LastPosition = split[0];
            split[0] = LastPosition;

            if (split.Length > 1)
            {
                LastPosition = split[1];
                split[1] = LastPosition;
            }

            return split;
        }

        private void ParseMetadata()
        {
            var properties = GetLineProperties();

            switch (properties[0])
            {
                case "Title":
                    Beatmap.Title = properties[1];
                    break;
                case "TitleUnicode":
                    Beatmap.TitleUnicode = properties[1];
                    break;
                case "Artist":
                    Beatmap.Artist = properties[1];
                    break;
                case "ArtistUnicode":
                    Beatmap.ArtistUnicode = properties[1];
                    break;
                case "Creator":
                    Beatmap.Creator = properties[1];
                    break;
                case "Difficulty":
                    Beatmap.Difficulty = properties[1];
                    break;
            }
        }

        private void ParseGameMode()
        {
            var properties = GetLineProperties();

            if (properties[0] == "Mode")
            {
                LastPosition = properties[1];
                Beatmap.Mode = int.Parse(LastPosition);

                if (Beatmap.Mode != OppaiConsts.MODE_STD)
                {
                    throw new InvalidOperationException("This game mode is not yet supported.");
                }
            }
        }

        private void ParseDifficulty()
        {
            var properties = GetLineProperties();

            LastPosition = properties[1];
            var value = float.Parse(LastPosition);

            switch (properties[0])
            {
                case "CircleSize":
                    Beatmap.CS = value;
                    break;
                case "OverallDifficulty":
                    Beatmap.OD = value;
                    break;
                case "ApproachRate":
                    Beatmap.AR = value;
                    ArFound = true;
                    break;
                case "HPDrainRate":
                    Beatmap.HP = value;
                    break;
                case "SliderMultiplier":
                    Beatmap.SV = value;
                    break;
                case "SliderTickRate":
                    Beatmap.TickRate = value;
                    break;
            }
        }

        private void ParseTimingPoint()
        {
            var s = LastLine.Split(',');

            if (s.Length > 8)
            {
                //todo: logging_warn => Timing point with trailing values.
            }

            var timing = new Timing();

            LastPosition = s[0];
            timing.Time = double.Parse(LastPosition);

            LastPosition = s[1];
            timing.MsPerBeat = double.Parse(s[1]);

            if (s.Length >= 7)
            {
                timing.Change = !(s[6].Trim() == "0");
            }
        }

        private void ParseHitObject()
        {
            var s = LastLine.Split(',');

            if (s.Length > 11)
            {
                //todo: logging_warn => Timing point with trailing values.
            }

            var hitObject = new HitObject();

            LastPosition = s[2];
            hitObject.Time = double.Parse(LastPosition);

            LastPosition = s[3];
            hitObject.Type = (NoteType)int.Parse(LastPosition);

            switch (hitObject.Type)
            {
                case NoteType.Circle:
                    ++Beatmap.NbCircles;

                    var circle = new Circle();
                    
                    LastPosition = s[0];
                    circle.Position.X = double.Parse(LastPosition);

                    LastPosition = s[1];
                    circle.Position.Y = double.Parse(LastPosition);

                    hitObject.Note = circle;
                    break;
                case NoteType.Slider:
                    ++Beatmap.NbSliders;

                    var slider = new Slider();

                    LastPosition = s[0];
                    slider.Position.X = double.Parse(LastPosition);

                    LastPosition = s[1];
                    slider.Position.Y = double.Parse(LastPosition);

                    LastPosition = s[6];
                    slider.Repetition = int.Parse(LastPosition);

                    LastPosition = s[7];
                    slider.Distance = double.Parse(LastPosition);

                    hitObject.Note = slider;
                    break;
                case NoteType.Spinner:
                    ++Beatmap.NbSpinners;
                    break;
            }

            Beatmap.HitObjects.Add(hitObject);
        }

        public static ParsedBeatmap Parse(string osuFile)
        {
            var lines = osuFile.Split('\n');

            var beatmapParser = new BeatmapParser();

            foreach (var lne in lines)
            {
                var line = lne;

                beatmapParser.LastLine = line;
                ++beatmapParser.NbLine;

                //Comment according to osu!Lazer.
                if (line.StartsWith(" ") || line.StartsWith("_"))
                {
                    continue;
                }

                line = line.Trim();
                beatmapParser.LastLine = line;

                if (line.Length <= 0)
                {
                    continue;
                }

                //Another kind of comment.
                if (line.StartsWith("//"))
                {
                    continue;
                }

                //[SectionName]
                if (line.StartsWith("["))
                {
                    beatmapParser.Section = line.Substring(1, line.Length - 2);
                }

                try
                {
                    switch (beatmapParser.Section)
                    {
                        case "Metadata":
                            beatmapParser.ParseMetadata();
                            break;
                        case "General":
                            beatmapParser.ParseGameMode();
                            break;
                        case "Difficulty":
                            beatmapParser.ParseDifficulty();
                            break;
                        case "TimingPoints":
                            beatmapParser.ParseTimingPoint();
                            break;
                        case "HitObjects":
                            beatmapParser.ParseHitObject();
                            break;
                        default:
                            var fileFormatVersion = line.IndexOf("file format v");
                            if (fileFormatVersion < 0)
                            {
                                continue;
                            }

                            beatmapParser.Beatmap.FormatVersion = int.Parse(line.Substring(fileFormatVersion + 13));
                            break;
                    }
                }
                catch (Exception ex)
                {
                    //todo: Add logging here.
                }
            }

            if (!beatmapParser.ArFound)
            {
                beatmapParser.Beatmap.AR = beatmapParser.Beatmap.OD;
            }

            beatmapParser.Parsed = true;
            return beatmapParser.Beatmap;
        }
    }
}
