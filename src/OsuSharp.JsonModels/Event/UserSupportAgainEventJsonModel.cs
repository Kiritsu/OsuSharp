using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public class UserSupportAgainEventJsonModel : EventJsonModel
    {
        [JsonProperty("user")]
        public EventUserModelJsonModel UserJson { get; set; }
    }
}