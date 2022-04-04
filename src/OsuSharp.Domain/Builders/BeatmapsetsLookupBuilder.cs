namespace OsuSharp.Domain;

/// <summary>
/// Represents a beatmapset lookup builder.
/// </summary>
public class BeatmapsetsLookupBuilder
{
    /// <summary>
    /// Gets or sets the search keywords.
    /// </summary>
    public string? Keywords { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="GameMode"/> to filter in the search.
    /// </summary>
    public GameMode? GameMode { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="BeatmapsetCategory"/> to filter in the search.
    /// </summary>
    public BeatmapsetCategory? Category { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="BeatmapsetGenre"/> to filter in the search.
    /// </summary>
    public BeatmapsetGenre? Genre { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="BeatmapsetLanguage"/> to filter in the search.
    /// </summary>
    public BeatmapsetLanguage? Language { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="BeatmapSearchGeneral"/> to filter in the search.
    /// </summary>
    public BeatmapSearchGeneral General { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="BeatmapSearchExtra"/> to filter in the search.
    /// </summary>
    public BeatmapSearchExtra Extra { get; set; }

    /// <summary>
    /// Gets or sets whether to include or not NSFW content in the search.
    /// </summary>
    public bool? Nsfw { get; set; }

    /// <summary>
    /// Gets or sets whether to include played maps (of the current authenticated user) only in the search.
    /// </summary>
    public bool? Played { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="RankAchieved"/> to filter in the search.
    /// </summary>
    public BeatmapRank RankAchieved { get; set; }

    /// <summary>
    /// Specifies the keywords to include in the search.
    /// </summary>
    /// <param name="keywords">Keywords to include.</param>
    public BeatmapsetsLookupBuilder WithKeywords(string? keywords = null)
    {
        Keywords = keywords;

        return this;
    }

    /// <summary>
    /// Specifies the gamemode to include in the search.
    /// </summary>
    /// <param name="gameMode">Gamemode to include.</param>
    public BeatmapsetsLookupBuilder WithGameMode(GameMode? gameMode = null)
    {
        GameMode = gameMode;

        return this;
    }

    /// <summary>
    /// Specifies the categorie to include in the search.
    /// </summary>
    /// <param name="category">Categorie to include.</param>
    public BeatmapsetsLookupBuilder WithCategory(BeatmapsetCategory? category = null)
    {
        Category = category;

        return this;
    }

    /// <summary>
    /// Specifies the genre to include in the search.
    /// </summary>
    /// <param name="genre">Genre to include.</param>
    public BeatmapsetsLookupBuilder WithGenre(BeatmapsetGenre? genre = null)
    {
        Genre = genre;

        return this;
    }

    /// <summary>
    /// Specifies the language to include in the search.
    /// </summary>
    /// <param name="language">Language to include.</param>
    /// <returns></returns>
    public BeatmapsetsLookupBuilder WithLanguage(BeatmapsetLanguage? language = null)
    {
        Language = language;

        return this;
    }

    /// <summary>
    /// Includes the recommended difficulties.
    /// </summary>
    public BeatmapsetsLookupBuilder WithRecommendedDifficulty()
    {
        if ((General & BeatmapSearchGeneral.Recommended) != BeatmapSearchGeneral.Recommended) 
        {
            General |= BeatmapSearchGeneral.Recommended;
        }

        return this;
    }

    /// <summary>
    /// Includes the converted beatmaps.
    /// </summary>
    public BeatmapsetsLookupBuilder WithConvertedBeatmaps()
    {
        if ((General & BeatmapSearchGeneral.Converts) != BeatmapSearchGeneral.Converts)
        {
            General |= BeatmapSearchGeneral.Converts;
        }

        return this;
    }

    /// <summary>
    /// Includes the subscribed mappers maps.
    /// </summary>
    public BeatmapsetsLookupBuilder WithSubscribedMappers()
    {
        if ((General & BeatmapSearchGeneral.Follows) != BeatmapSearchGeneral.Follows)
        {
            General |= BeatmapSearchGeneral.Follows;
        }

        return this;
    }

    /// <summary>
    /// Includes maps with videos.
    /// </summary>
    public BeatmapsetsLookupBuilder WithVideo()
    {
        if ((Extra & BeatmapSearchExtra.Video) != BeatmapSearchExtra.Video)
        {
            Extra |= BeatmapSearchExtra.Video;
        }

        return this;
    }

    /// <summary>
    /// Includes maps with storyboards.
    /// </summary>
    public BeatmapsetsLookupBuilder WithStoryboard()
    {
        if ((Extra & BeatmapSearchExtra.Storyboard) != BeatmapSearchExtra.Storyboard)
        {
            Extra |= BeatmapSearchExtra.Storyboard;
        }

        return this;
    }

    /// <summary>
    /// Includes explicit content.
    /// </summary>
    /// <param name="nsfw">Whether to include or not any explicit content.</param>
    public BeatmapsetsLookupBuilder WithExplicitContent(bool? nsfw = true)
    {
        Nsfw = nsfw;

        return this;
    }

    /// <summary>
    /// Includes played content.
    /// </summary>
    /// <param name="played">Whether to include or not played content.</param>
    public BeatmapsetsLookupBuilder WithPlayed(bool? played = true)
    {
        Played = played;

        return this;
    }

    /// <summary>
    /// Specifies the beatmap rank to include in the search.
    /// </summary>
    /// <param name="rankAchieved">Beatmap rank achieved to include.</param>
    public BeatmapsetsLookupBuilder WithRankAchieved(BeatmapRank rankAchieved)
    {
        RankAchieved = rankAchieved;

        return this;
    }
}