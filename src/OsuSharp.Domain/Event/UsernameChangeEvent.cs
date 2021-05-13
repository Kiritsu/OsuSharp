namespace OsuSharp.Domain
{
    public sealed class UsernameChangeEvent : Event
    {
        public EventUsernameChangeModel User { get; internal set; }

        internal UsernameChangeEvent()
        {
            
        }
    }
}