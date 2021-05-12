using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class UserSupportGiftEventJsonModel : EventJsonModel
    {
        [JsonProperty("user")]
        public EventUserJsonModel UserJson { get; internal set; }

        internal UserSupportGiftEventJsonModel()
        {
            
        }
    }
}