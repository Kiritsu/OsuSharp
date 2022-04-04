using System.Net.Http;
using OsuSharp.Legacy.Extensions;

namespace OsuSharp.Legacy;

public sealed class LegacyOsuSharpConfiguration
{
    /// <summary>
    ///     Represents the osu! API Key to be used.
    /// </summary>
    public string ApiKey { get; set; }

    /// <summary>
    ///     Custom HttpClient. If not given, a new one will be instantiated.
    /// </summary>
    public HttpClient HttpClient { get; set; }

    /// <summary>
    ///     Defines the default separator to use when using <see cref="ModeExtensions.ToModeString"/>.
    /// </summary>
    public string ModeSeparator { get; set; }

    /// <summary>
    ///     Defines the base URL to send requests to, without ending slash. Default to <c>https://osu.ppy.sh/api</c>.
    /// </summary>
    public string BaseUrl { get; set; } = "https://osu.ppy.sh/api";
}