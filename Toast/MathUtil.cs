using System;
using SFML.Graphics;
using SFML.System;

namespace Toast
{
    public static class MathUtil
    {
        public static float Magnitude(this Vector2f v)
        {
            return (float)Math.Sqrt(v.X * v.X + v.Y * v.Y);
        }
        public static float SquaredMagnitude(this Vector2f v)
        {
            return v.X * v.X + v.Y * v.Y;
        }


        public static Vector2f Normalize(this Vector2f v)
        {
            var mag = v.Magnitude();
            return mag < 0.000001 ? new Vector2f() : new Vector2f(v.X / mag, v.Y / mag);
        }


        public static float Rotation(this Vector2f v)
        {
            var arctan = (float)(Math.Atan2(v.Y, v.X) * 180 / Math.PI);
            if (v.X < 0)
            {
                arctan -= 360;
            }
            return arctan;
        }

        public static Vector2f ToFloat(this Vector2i v)
        {
            return new Vector2f(v.X, v.Y);
        }

        public static Vector2f Size(this Shape s)
        {
            var bounds = s.GetLocalBounds();
            return new Vector2f(bounds.Width, bounds.Height);
        }

        public static float RandomGaussian(float mean, float sigma, float maxDelta = Single.NaN)
        {
            var u1 = 1.0 - RNG.NextDouble(); //uniform(0,1] random doubles
            var u2 = 1.0 - RNG.NextDouble();
            var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            var value = (float)(mean + sigma * randStdNormal);
            return float.IsNaN(maxDelta) ? value : Clamp(value, mean - maxDelta, mean + maxDelta);
        }

        public static float Clamp(float value, float min, float max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }

        private static readonly Random RNG = new Random();
    }
}