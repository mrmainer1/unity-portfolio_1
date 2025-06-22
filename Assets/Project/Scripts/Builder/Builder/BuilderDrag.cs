using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Patterns.Pool;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using Project.EntenEller.Base.Scripts.UI.Toggles;
using Project.Scripts.Map.Cell;
using UnityEngine;

namespace Project.Scripts.Builder.Builder
{
    public class BuilderDrag : EEBehaviourUpdate
    {
        [SerializeField] private MapCellHolder mapCellHolderRaw;
        public EENotifier DragNotifier;
        
        protected override void EEUpdate()
        {
            base.EEUpdate();
            if (!Input.GetMouseButtonDown(1)) return;
            if(EESingleton.Get<BuilderManager>().CurrentBuilding != null) return;
            var mapCell = MapCell.GetCellByXY(mapCellHolderRaw.Cell.x, mapCellHolderRaw.Cell.y);
            if (mapCell == null) return;
            if (mapCell.Building == null) return;
            EESingleton.Get<BuilderManager>().SetBuilding(mapCell.Building,true);
            mapCell.Building.GetSelf<EEPoolObject>().Spawner.GetNeighbor<EEToggle>().Disable();
            mapCell.Building.Unplace();
        }
    }
}
