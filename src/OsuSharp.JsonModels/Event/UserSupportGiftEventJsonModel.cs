using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public class UserSupportGiftEventJsonModel : EventJsonModel
    {
        [JsonProperty("user")]
        public EventUserModelJsonModel UserJson { get; set; }
    }
}