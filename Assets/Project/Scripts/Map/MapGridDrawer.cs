using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Advanced.Spawns;
using Project.EntenEller.Base.Scripts.Advanced.Tags;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.Utilities;
using UnityEngine.UI;

namespace Project.Scripts.Map
{
    public class MapGridDrawer : EEBehaviour
    {
        public EEGameObjectFinder gridSpawnerGameObject;
        private MapResizer mapResizer;

        public EENotifier DrawingEndedNotifier;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            mapResizer = GetSelf<MapResizer>();
            mapResizer.ChangeSizeStartNotifier.Event += OnChangeSizeStart;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            mapResizer.ChangeSizeStartNotifier.Event -= OnChangeSizeStart;
        }

        private void OnChangeSizeStart()
        {
            var grid = gridSpawnerGameObject.GetSingle();
            var gridSpawner = grid.GetSelf<EESpawner>();
            var amount = mapResizer.Width * mapResizer.Height;
            if (amount > gridSpawner.ActiveAmount)
            {
                for (var i = gridSpawner.ActiveAmount; i < amount; i++)
                {
                    gridSpawner.Spawn();
                }
            }
            if (amount < gridSpawner.ActiveAmount)
            {
                var delta = gridSpawner.ActiveAmount - amount;
                for (var i = 0; i < delta; i++)
                {
                    gridSpawner.Despawn(gridSpawner.SpawnList[0]);
                }
            }

            DrawingEndedNotifier.Notify();
            grid.GetSelf<GridLayoutGroup>().constraintCount = mapResizer.Width;
        }

        public void ClearMap()
        {
            var grid = gridSpawnerGameObject.GetSingle();
            var grids = grid.GetSelf<EESpawner>().SpawnList;
            foreach (var g in grids)
            {
                if(g == null) continue;
                g.Off();
            }
        }
    }
}
