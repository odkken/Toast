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
    }
}