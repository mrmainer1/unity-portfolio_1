using System;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Maths
{
    public static class EEMathUtils 
    {
        public static float GetXOnLine2D(Vector2 pos0, Vector2 pos1, float y)
        {
            return pos0.x + (pos1.x - pos0.x) * ((y - pos0.y) / (pos1.y - pos0.y));
        }
        
        public static float RoundFloat(this float value, int decimals)
        {
            return (float) Math.Round(value, decimals);
        }
    }
}