using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public sealed class UserPage : IUserPage
    {
        public string Html { get; internal set; }

        public string Raw { get; internal set; }

        internal UserPage()
        {
            
        }
    }
}