using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public class Cursor : ICursor
    {
        public string ApprovedDate { get; internal set; }
        public string Id { get; internal set; }

        internal Cursor()
        {

        }
    }
}
