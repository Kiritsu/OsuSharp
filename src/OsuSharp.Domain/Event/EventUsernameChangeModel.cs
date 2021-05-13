namespace OsuSharp.Domain
{
    public sealed class EventUsernameChangeModel : EventUserModel
    {
        public string PreviousUsername { get; internal set; }

        internal EventUsernameChangeModel()
        {
            
        }
    }
}