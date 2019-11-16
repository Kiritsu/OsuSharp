using System;

namespace OsuSharp.Oppai
{
    /// <summary>
    ///     Represents a basic implementation of Vector2.
    /// </summary>
    public sealed class Vector2
    {
        public double X { get; internal set; }
        public double Y { get; internal set; }

        public double Length => Math.Sqrt((X * X) + (Y * Y));

        public Vector2() : this(0.0, 0.0)
        {
        }

        public Vector2(Vector2 copy) : this(copy.X, copy.Y)
        {

        }

        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        ///     Substracts the current vector with another vector.
        /// </summary>
        /// <param name="other">Right side vector to substract.</param>
        public Vector2 Substract(Vector2 other)
        {
            return new Vector2
            {
                X = X - other.X,
                Y = Y - other.Y
            };
        }

        /// <summary>
        ///     Multiplies the current vector's x and y with the specified value.
        /// </summary>
        /// <param name="value">Value to mulitply with this vector's x and y</param>
        public Vector2 Multiply(double value)
        {
            return new Vector2
            {
                X = X * value,
                Y = Y * value
            };
        }

        /// <summary>
        ///     Dot product between two vectors. Correlates with the angles.
        /// </summary>
        /// <param name="other">Right side vector to do the dot product with.</param>
        public double Dot(Vector2 other)
        {
            return (X * other.X) + (Y * other.Y);
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
