using System.Collections.Generic;

namespace OsuSharp.Domain
{
    public sealed class UserGroup
    {
        public long Id { get; internal set; }

        public string Identifier { get; internal set; }

        public bool IsProbationary { get; internal set; }

        public string Name { get; internal set; }

        public string ShortName { get; internal set; }

        public string Description { get; internal set; }

        public string Colour { get; internal set; }

        public IReadOnlyCollection<GameMode> PlayModes { get; internal set; }

        internal UserGroup()
        {
            
        }
    }
}