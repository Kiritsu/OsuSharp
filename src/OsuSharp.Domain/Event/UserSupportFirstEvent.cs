namespace OsuSharp.Domain
{
    public sealed class UserSupportFirstEvent : Event
    {
        public EventUserModel User { get; internal set; }
    }
}