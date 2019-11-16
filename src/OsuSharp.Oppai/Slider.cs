namespace OsuSharp.Oppai
{
    public sealed class Slider : Note
    {
        public Vector2 Position { get; set; }

        public double Distance { get; set; }

        public int Repetition { get; set; }

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
