namespace OsuSharp.Oppai
{
    public sealed class Circle : Note
    {
        public Vector2 Position { get; internal set; }

        public Circle()
        {
            Position = new Vector2();
        }

        public override string ToString()
        {
            return Position.ToString();
        }
    }
}
