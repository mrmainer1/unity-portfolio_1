using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Sprites
{
    public static class EESpriteUtils
    {
        public static Sprite Texture2DToSprite (Texture2D texture2D)
        {
            var rect = new Rect(0, 0, texture2D.width, texture2D.height);
            var pivot = new Vector2(0.5f, 0.5f);
            return Sprite.Create(texture2D, rect, pivot);
        }
    }
}
