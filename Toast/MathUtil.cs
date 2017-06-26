using System;
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
    }
}