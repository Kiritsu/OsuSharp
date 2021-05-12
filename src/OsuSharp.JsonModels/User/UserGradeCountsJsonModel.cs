using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public sealed class UserGradeCountsJsonModel
    {
        [JsonProperty("ss")]
        public long SS { get; internal set; }

        [JsonProperty("ssh")]
        public long SSH { get; internal set; }

        [JsonProperty("s")]
        public long S { get; internal set; }

        [JsonProperty("sh")]
        public long SH { get; internal set; }

        [JsonProperty("a")]
        public long A { get; internal set; }

        internal UserGradeCountsJsonModel()
        {
        }
    }
}