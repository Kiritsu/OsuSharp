namespace OsuSharp.Entities
{
    public abstract class EntityBase
    {
        public OsuClient Client { get; internal set; }

        internal EntityBase() { }
    }
}
