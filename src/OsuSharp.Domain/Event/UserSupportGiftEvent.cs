namespace OsuSharp.Domain
{
    public sealed class UserSupportGiftEvent : Event
    {
        public EventUserModel User { get; internal set; }
    }
}
