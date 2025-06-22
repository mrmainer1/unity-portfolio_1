using System;
using System.IO;
using System.Threading.Tasks;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Textures
{
    public static class EETextureUtils
    {
        public static Texture2D Rotate(Texture2D target, int angle)
        {
            var rotatedTexture = new Texture2D(target.width, target.height);
            var pivotPoint = new Vector2(target.width / 2f, target.height / 2f);
            for (var x = 0; x < rotatedTexture.width; x++)
            for (var y = 0; y < rotatedTexture.height; y++)
            {
                var rotatedPosition = EEAngleUtils.RotatePoint(new Vector2(x, y), pivotPoint, angle);
                var pixelColor = target.GetPixel((int) rotatedPosition.x, (int) rotatedPosition.y);
                rotatedTexture.SetPixel(x, y, pixelColor);
            }
            rotatedTexture.Apply();
            return rotatedTexture;
        }
        
        public static async Task LoadAsync(string filePath, Action<Texture2D> OnImageLoaded)
        {
            if (!File.Exists(filePath))
            {
                Debug.LogError("File not found at " + filePath);
                return;
            }
            var fileData = await File.ReadAllBytesAsync(filePath);
            var texture = new Texture2D(2, 2);
            if (texture.LoadImage(fileData))
            {
                OnImageLoaded?.Invoke(texture);
            }
            else
            {
                Debug.LogError("Failed to load texture from " + filePath);
            }
        }
    }
}