using OpenTK;
using System;

namespace Extensions
{
    public static class Utility
    {
        public static Vector2 Rotate(this Vector2 v, float angle)
        {
            Vector2 rotate = new Vector2();
            rotate.X = (float) (Math.Cos(angle) * v.X - Math.Sin(angle) * v.Y);
            rotate.Y = (float)(Math.Sin(angle) * v.X + Math.Cos(angle) * v.Y);
            return rotate;
        }
    }
}
