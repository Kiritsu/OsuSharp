using OsuSharp.Domain;

namespace OsuSharp.Interfaces;

public interface IOsuClientConfiguration
{
    /// <summary>
    /// Gets or sets the given client ID after registering your application.
    /// </summary>
    long ClientId { get; set; }

    /// <summary>
    /// Gets or sets the given client secret after registering your application.
    /// </summary>
    string ClientSecret { get; set; }

    /// <summary>
    /// Gets or sets whether to wait or throw on rate-limits. Defaults to false.
    /// </summary>
    bool ThrowOnRateLimits { get; set; }

    /// <summary>
    /// Gets or sets the separator used to convert <see cref="Mods"/> into their string representation.
    /// Defaults to space. 
    /// </summary>
    string ModFormatSeparator { get; set; }

    /// <summary>
    /// Gets or sets the base URL of the API.
    /// </summary>
    string BaseUrl { get; set; }
}