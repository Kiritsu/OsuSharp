using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    public sealed class EventUsernameChangeModel : EventUserModel
    {
        [JsonProperty("previous_username")]
        public string PreviousUsername { get; internal set; }
        
        internal EventUsernameChangeModel()
        {
            
        }
    }
}