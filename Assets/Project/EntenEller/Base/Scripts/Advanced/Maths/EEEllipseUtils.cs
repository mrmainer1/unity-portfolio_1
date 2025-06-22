using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Maths
{
    public static class EEEllipseUtils
    {
        public static bool IsInside(float x, float y, float xc, float yc, float a, float b, float rotation)
        {
            var dx = x - xc;
            var dy = y - yc;
            var cos = Mathf.Cos(rotation);
            var sin = Mathf.Sin(rotation);
            var rx = dx * cos - dy * sin;
            var ry = dx * sin + dy * cos;

            var isInside = (rx * rx) / (a * a) + (ry * ry) / (b * b) <= 1;
            return isInside;
        }

    }
}