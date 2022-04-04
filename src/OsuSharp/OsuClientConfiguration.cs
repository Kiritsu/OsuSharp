using OsuSharp.Domain;
using OsuSharp.Interfaces;

namespace OsuSharp;

/// <summary>
/// Represents the configuration of a OsuClient.
/// </summary>
public sealed class OsuClientConfiguration : IOsuClientConfiguration
{
    /// <summary>
    /// Gets or sets the given client ID after registering your application.
    /// </summary>
    public long ClientId { get; set; }

    /// <summary>
    /// Gets or sets the given client secret after registering your application.
    /// </summary>
    public string ClientSecret { get; set; } = null!;

    /// <summary>
    /// Gets or sets whether to wait or throw on rate-limits. Defaults to false.
    /// </summary>
    public bool ThrowOnRateLimits { get; set; } = false;

    /// <summary>
    /// Gets or sets the separator used to convert <see cref="Mods"/> into their string representation.
    /// Defaults to space. 
    /// </summary>
    public string ModFormatSeparator { get; set; } = " ";

    /// <summary>
    /// Gets or sets the base URL of the API.
    /// </summary>
    public string BaseUrl { get; set; } = "https://osu.ppy.sh";
}