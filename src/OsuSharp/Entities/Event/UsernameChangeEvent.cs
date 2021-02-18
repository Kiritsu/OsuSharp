using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    public sealed class UsernameChangeEvent : Event
    {
        [JsonProperty("user")]
        public EventUsernameChangeModel User { get; internal set; }

        internal UsernameChangeEvent()
        {
            
        }
    }
}