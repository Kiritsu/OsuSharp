using System.Net.Http;

namespace OsuSharp
{
    public sealed class OsuSharpConfiguration
    {
        /// <summary>
        ///     Represents the osu! API Key to be used.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        ///     Custom HttpClient. If not given, a new one will be instanciated.
        /// </summary>
        public HttpClient Client { get; set; }

        /// <summary>
        ///     Defines the default separator to use when using <see cref="Mods.ToString()"/>.
        /// </summary>
        public string ModsSeparator { get; set; }
    }
}
