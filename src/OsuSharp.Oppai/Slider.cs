namespace OsuSharp.Oppai
{
    public sealed class Slider : Note
    {
        public Vector2 Position { get; internal set; }

        public double Distance { get; internal set; }

        public int Repetition { get; internal set; }

        public Slider()
        {
            Position = new Vector2();
            Distance = 0.0;
            Repetition = 1;
        }

        public override string ToString()
        {
            return $"({Position}, {Distance}, {Repetition})";
        }
    }
}
