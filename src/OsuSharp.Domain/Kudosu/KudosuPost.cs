using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public sealed class KudosuPost : IKudosuPost
    {
        public string Url { get; internal set; }

        public string Title { get; internal set; }

        internal KudosuPost()
        {
            
        }
    }
}