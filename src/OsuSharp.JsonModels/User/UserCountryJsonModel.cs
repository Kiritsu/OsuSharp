using System.Globalization;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public class UserCountryJsonModel : JsonModel
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonIgnore]
        public RegionInfo RegionInfo => _regionInfo ??= new RegionInfo(Name);

        private RegionInfo _regionInfo;
    }
}