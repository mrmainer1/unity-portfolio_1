using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.Scripts.Map.Cell
{
    public class MapCellHolder : EEBehaviour
    {
        public Vector2Int Cell;
        public EENotifier CellUpdateNotifier;
        public bool IsValid;

        public void Refresh(int x, int y)
        {
            if (Cell.x == x && Cell.y == y) return;
            Cell.x = x;
            Cell.y = y;
            IsValid = MapCellVerifier.Check(Cell);
            CellUpdateNotifier.Notify();
        }
    }
}
