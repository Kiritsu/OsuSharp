using System;

namespace OsuSharp.Oppai
{
    public sealed class Accuracy
    {
        public int Count300 { get; set; }
        public int Count100 { get; set; }
        public int Count50 { get; set; }
        public int CountMiss { get; set; }

        public Accuracy(int count300, int count100, int count50, int countMiss)
        {
            Count300 = count300;
            Count100 = count100;
            Count50 = count50;
            CountMiss = countMiss;
        }

        public Accuracy(int count100, int count50, int countMiss) : this(-1, count100, count50, countMiss)
        {

        }

        public Accuracy(int count100, int count50) : this(-1, count100, count50, 0)
        {

        }

        public Accuracy(int count100) : this(-1, count100, 0, 0)
        {

        }

        public Accuracy(double accuracy, int countObjects, int countMiss)
        {
            var max300 = countObjects - countMiss;
            var maxAccuracy = new Accuracy(max300, 0, 0, countMiss).GetAccuracy() * 100.0;

            accuracy = Math.Max(0.0, Math.Min(maxAccuracy, accuracy));

            Count100 = (int)Math.Round(-3.0 * ((((accuracy * 0.01) - 1.0) * countObjects) + countMiss) * 0.5);

            if (Count100 > max300)
            {
                // acc lower than all 100s, use 50s
                Count100 = 0;
                Count50 = (int)Math.Round(-6.0 * ((((accuracy * 0.01) - 1.0) * countObjects) + countMiss) * 0.5);
                Count50 = Math.Min(max300, Count50);
            }

            Count300 = countObjects - Count100 - Count50 - countMiss;
        }

        private double GetAccuracy()
        {
            return GetAccuracy(-1);
        }

        private double GetAccuracy(int countObjects)
        {
            if (countObjects < 0 && Count300 < 0)
            {
                throw new ArgumentException("Either nobjects or n300 must be specified");
            }

            var n300_ = Count300 > 0 
                ? Count300 
                : countObjects - Count100 - Count50 - CountMiss;

            if (countObjects < 0)
            {
                countObjects = n300_ + Count100 + Count50 + CountMiss;
            }

            var res = ((Count50 * 50.0) + (Count100 * 100.0) + (n300_ * 300.0)) / (countObjects * 300.0);

            return Math.Max(0, Math.Min(res, 1.0));
        }
    }
}
