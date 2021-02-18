using Newtonsoft.Json;

namespace OsuSharp.Entities.Event
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