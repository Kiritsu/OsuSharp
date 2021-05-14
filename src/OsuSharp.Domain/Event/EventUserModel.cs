using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public class EventUserModel : IEventUserModel
    {
        public string Username { get; internal set; }

        public string Url { get; internal set; }

        internal EventUserModel()
        {
            
        }
    }
}