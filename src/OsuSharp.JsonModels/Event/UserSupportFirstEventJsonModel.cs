using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class UserSupportFirstEventJsonModel : EventJsonModel
    {
        [JsonProperty("user")]
        public EventUserJsonModel UserJson { get; internal set; }

        internal UserSupportFirstEventJsonModel()
        {
            
        }
    }
}