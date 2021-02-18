using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    public sealed class UserSupportAgainEvent : Event
    {
        [JsonProperty("user")]
        public EventUserModel User { get; internal set; }

        internal UserSupportAgainEvent()
        {
            
        }
    }
}