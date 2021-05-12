using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class UserSupportAgainEventJsonModel : EventJsonModel
    {
        [JsonProperty("user")]
        public EventUserJsonModel UserJson { get; internal set; }

        internal UserSupportAgainEventJsonModel()
        {
        }
    }
}