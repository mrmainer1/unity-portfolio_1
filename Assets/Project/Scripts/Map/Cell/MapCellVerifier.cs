using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using UnityEngine;

namespace Project.Scripts.Map.Cell
{
    public static class MapCellVerifier
    {
        public static bool Check(Vector2Int cell)
        {
            var isValid = !IsOutOfBorders(cell);
            if (!isValid) return false;
            return true;
        }
        
        private static bool IsOutOfBorders(Vector2Int cell)
        {
            var mapResizer = EESingleton.Get<MapResizer>();
            var w = mapResizer.Width;
            var h = mapResizer.Height;
            if (cell.x > w) return true;
            if (cell.y > h) return true;
            if (cell.x < 0) return true;
            if (cell.y < 0) return true;
            return false;
        }
    }
}
