using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Transforms;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using Project.EntenEller.Base.Scripts.Timers;
using Project.Scripts.Builder;
using Project.Scripts.Builder.Building;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Scripts.Map.Cell
{
    public class MapCell : EEBehaviour
    {
        [ReadOnly] public Vector2Int Cell;
        [ReadOnly] public Building Building;

        public bool IsFull => Building != null;
        
        private static List<MapCell> mapCells = new List<MapCell>();
        private static MapResizer mapResizer;

        protected override void EEAwake()
        {
            base.EEAwake();
            if (mapResizer == null) mapResizer = EESingleton.Get<MapResizer>();
            mapResizer.ChangeSizeFinishNotifier.Event += RecountFinish;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            mapResizer.ChangeSizeFinishNotifier.Event -= RecountFinish;
        }

        private void RecountFinish()
        {
            Building = null;
            var children = transform.parent.GetFirstRowOfChildren();
            children.RemoveAll(a => !a.gameObject.activeSelf);
            var index = children.IndexOf(transform);
            Cell.x = index % mapResizer.Width;
            Cell.y = index / mapResizer.Width;
        }

        protected override void EEEnable()
        {
            base.EEEnable();
            mapCells.Add(this);
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            mapCells.Remove(this);
        }

        public static MapCell GetCellByXY(int x, int y)
        {
            foreach (var mapCell in mapCells)
            {
                if (mapCell.Cell.x == x && mapCell.Cell.y == y) return mapCell;
            }
            return null;
        }

        public static List<MapCell> GetCellByBuilding(Building building)
        {
            var allCells = new List<MapCell>();
            foreach (var mapCell in mapCells)
            {
                if (mapCell.Building == building) allCells.Add(mapCell);
            }

            return allCells;
        }

        public void SetBuilding(Building building)
        {
            Building = building;
        }

        public void ClearBuilding() => Building = null;
    }
}