using System;
using Project.EntenEller.Base.Scripts.Advanced.Canvases;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using Project.Scripts.Map;
using Project.Scripts.Map.Cell;
using UnityEngine;

namespace Project.Scripts.Builder.Building
{
    public class Building : EEBehaviour
    {
        public int Width, Height;
        [HideInInspector] public int StartingWidth, StartingHeight;
        public bool IsValid;
        public bool IsPlaced;
        [HideInInspector] public MapCellHolder mapCellHolder;
        private MapResizer mapResizer;
        private Vector2Int cellToCheck;
        private RaycastHit hit;

        public EENotifier ValidNotifier, InvalidNotifier, PlaceNotifier, UnplaceNotifier;
        public static event Action FailPlaceEvent;

        protected override void EEAwake()
        {
            base.EEAwake();
            StartingWidth = Width;
            StartingHeight = Height;
            if (Height == 1 && Width == 1)
                mapCellHolder = EESingleton.Get<MapCellFinderByMouse>().GetSelf<MapCellHolder>();
            else
                mapCellHolder = EESingleton.Get<MapCellFinderProcessed>().GetSelf<MapCellHolder>();

            mapResizer = EESingleton.Get<MapResizer>();
            mapCellHolder.CellUpdateNotifier.Event += OnCellUpdate;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            mapCellHolder.CellUpdateNotifier.Event -= OnCellUpdate;
        }

        protected override void EEEnable()
        {
            base.EEEnable();
            InvalidNotifier.Notify();
        }

        public void Place(int x0, int y0)
        {
            for (var x = x0; x < x0 + Width; x++)
            for (var y = y0; y < y0 + Height; y++)
            {
                var cell = MapCell.GetCellByXY(x, y);
                if (cell == null)
                {
                    FailPlaceEvent.Call();
                    throw new Exception("Building position is out of map borders!");
                }
                cell.SetBuilding(this);
            }
            IsPlaced = true;
            PlaceNotifier.Notify();
        }
        
        public void Unplace()
        {
            foreach (var mapCell in MapCell.GetCellByBuilding(this))
            {
                mapCell.ClearBuilding();
            }
            IsValid = true;
            OnCellUpdate();
            IsPlaced = false;
            UnplaceNotifier.Notify();
            ValidNotifier.Notify();
        }

        public void OnCellUpdate()
        {
            if (!enabled) return;
            IsValid = mapCellHolder.IsValid;
            IsOutOfBorders();
            if (EECanvas.CheckHitInAnyCanvas(Input.mousePosition)) IsValid = false; 
            IsFullCell();
            CheckValid();
        }
        private void IsOutOfBorders()
        {
            if (mapCellHolder.Cell.x + Width > mapResizer.Width) IsValid = false;
            if (mapCellHolder.Cell.y + Height > mapResizer.Height) IsValid = false;
        }
        
        private void IsFullCell()
        {
            var x0 = mapCellHolder.Cell.x;
            var y0 = mapCellHolder.Cell.y;

            for (var x = x0; x < x0 + Width; x++)
            for (var y = y0; y < y0 + Height; y++)
            {
                var cell = MapCell.GetCellByXY(x, y);
                if (cell == null) continue;
                if (cell.IsFull)
                {
                    IsValid = false;
                    return;
                }
            }
        }

        private void CheckValid()
        {
            if (IsValid) ValidNotifier.Notify();
            else InvalidNotifier.Notify();
        }
        
        public void SetSize(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
