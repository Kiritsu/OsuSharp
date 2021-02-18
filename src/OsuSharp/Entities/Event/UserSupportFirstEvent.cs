using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    public sealed class UserSupportFirstEvent : Event
    {
        [JsonProperty("user")]
        public EventUserModel User { get; internal set; }

        internal UserSupportFirstEvent()
        {
            
        }
    }
}