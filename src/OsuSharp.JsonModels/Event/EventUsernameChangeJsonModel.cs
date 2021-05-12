using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class EventUsernameChangeJsonModel : EventUserJsonModel
    {
        [JsonProperty("previous_username")]
        public string PreviousUsername { get; internal set; }

        internal EventUsernameChangeJsonModel()
        {
        }
    }
}