using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Intersection
{
    public static class EEIntersectionUtils
    {
        public static bool IsIntersectCircleRectangle2D(Vector2 c1, float r, Vector2 c2 , float w, float h)
        {
            var closestX = Mathf.Clamp(c1.x, c2.x - w / 2, c2.x + w / 2);
            var closestY = Mathf.Clamp(c1.y, c2.y - h / 2, c2.y + h / 2);
            var distanceX = c1.x - closestX;
            var distanceY = c1.y - closestY;
            return (distanceX * distanceX + distanceY * distanceY) <= (r * r);
        }
    }
}
