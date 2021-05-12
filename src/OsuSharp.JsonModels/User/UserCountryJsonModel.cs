using System.Globalization;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class UserCountryJsonModel
    {
        [JsonProperty("code")]
        public string Code { get; internal set; }

        [JsonProperty("name")]
        public string Name { get; internal set; }

        [JsonIgnore]
        public RegionInfo RegionInfo => _regionInfo ??= new RegionInfo(Name);
        private RegionInfo _regionInfo;

        internal UserCountryJsonModel()
        {
            
        }
    }
}