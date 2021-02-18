using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    public sealed class UserSupportGiftEvent : Event
    {
        [JsonProperty("user")]
        public EventUserModel User { get; internal set; }

        internal UserSupportGiftEvent()
        {
            
        }
    }
}