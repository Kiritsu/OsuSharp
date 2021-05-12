using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class UsernameChangeEventJsonModel : EventJsonModel
    {
        [JsonProperty("user")]
        public EventUsernameChangeJsonModel User { get; internal set; }

        internal UsernameChangeEventJsonModel()
        {
        }
    }
}