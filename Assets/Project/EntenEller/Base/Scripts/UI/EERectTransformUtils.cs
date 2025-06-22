using UnityEngine;

namespace Project.EntenEller.Base.Scripts.UI
{
    public static class EERectTransformUtils
    {
        public static Vector2 ToScaledSize(this RectTransform target)
        {
            return Vector2.Scale(target.rect.size, target.lossyScale);
        }
        
        public static Rect ToScreenSpace(this RectTransform target)
        {
            var scaledSize = target.ToScaledSize();
            var position = (Vector2) target.position - target.pivot * scaledSize;
            return new Rect(position, scaledSize);
        }
    }
}
