namespace OsuSharp.Domain
{
    public class BeatmapsetsLookupBuilder
    {
        public string Keywords { get; set;  }


        public GameMode? GameMode { get; set; }

        public BeatmapsetCategory? Category { get; set; }

        public BeatmapsetGenre? Genre { get; set; }

        public BeatmapsetLanguage? Language { get; set; }

        public BeatmapSearchGeneral General { get; set; }

        public BeatmapSearchExtra Extra { get; set; }


        public bool? Nsfw { get; set; }


        public bool? Played { get; set; }


        public BeatmapRank RankAchieved { get; set; }

        public BeatmapsetsLookupBuilder WithKeywords(string keywords = null)
        {
            Keywords = keywords;

            return this;
        }

        public BeatmapsetsLookupBuilder WithGameMode(GameMode? gameMode = null)
        {
            GameMode = gameMode;

            return this;
        }

        public BeatmapsetsLookupBuilder WithCategory(BeatmapsetCategory? category = null)
        {
            Category = category;

            return this;
        }

        public BeatmapsetsLookupBuilder WithGenre(BeatmapsetGenre? genre = null)
        {
            Genre = genre;

            return this;
        }

        public BeatmapsetsLookupBuilder WithLanguage(BeatmapsetLanguage? language = null)
        {
            Language = language;

            return this;
        }

        public BeatmapsetsLookupBuilder WithRecommendedDifficulty()
        {
            if ((General & BeatmapSearchGeneral.Recommended) != BeatmapSearchGeneral.Recommended) 
            {
                General |= BeatmapSearchGeneral.Recommended;
            }

            return this;
        }

        public BeatmapsetsLookupBuilder WithConvertedBeatmaps()
        {
            if ((General & BeatmapSearchGeneral.Converts) != BeatmapSearchGeneral.Converts)
            {
                General |= BeatmapSearchGeneral.Converts;
            }

            return this;
        }

        public BeatmapsetsLookupBuilder WithSubscribedMappers()
        {
            if ((General & BeatmapSearchGeneral.Follows) != BeatmapSearchGeneral.Follows)
            {
                General |= BeatmapSearchGeneral.Follows;
            }

            return this;
        }

        public BeatmapsetsLookupBuilder WithVideo()
        {
            if ((Extra & BeatmapSearchExtra.Video) != BeatmapSearchExtra.Video)
            {
                Extra |= BeatmapSearchExtra.Video;
            }

            return this;
        }

        public BeatmapsetsLookupBuilder WithStoryboard()
        {
            if ((Extra & BeatmapSearchExtra.Storyboard) != BeatmapSearchExtra.Storyboard)
            {
                Extra |= BeatmapSearchExtra.Storyboard;
            }

            return this;
        }

        public BeatmapsetsLookupBuilder WithExplicitContent(bool nsfw = true)
        {
            Nsfw = nsfw;

            return this;
        }

        public BeatmapsetsLookupBuilder WithPlayed(bool? played = true)
        {
            Played = played;

            return this;
        }

        public BeatmapsetsLookupBuilder WithRankAchieved(BeatmapRank rankAchieved)
        {
            RankAchieved = rankAchieved;

            return this;
        }
    }
}
