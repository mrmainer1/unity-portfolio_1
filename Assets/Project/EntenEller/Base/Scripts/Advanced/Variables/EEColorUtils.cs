using System.Collections.Generic;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Variables
{
    public static class EEColorUtils
    {
        public static IEnumerable<byte> ToBytes(this Color color)
        {
            return new byte[]
            {
                (byte)(color.r * 255),
                (byte)(color.g * 255),
                (byte)(color.b * 255)
            };
        }
        
        public static Color HexToColor(this string hex)
        {
            hex = hex.ToUpper();
            if (!hex.StartsWith("#")) hex = "#" + hex;
            if (ColorUtility.TryParseHtmlString(hex, out var color)) return color;
            return Color.magenta;
        }
    }
}
