namespace OsuSharp.Domain
{
    public sealed class UserSupportAgainEvent : Event
    {
        public EventUserModel User { get; internal set; }
    }
}
