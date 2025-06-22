using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.Inputs.Pointers;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Patterns.Pool;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using Project.Scripts.Builder.Builder;
using Project.Scripts.Map.Cell;
using Project.Scripts.SaveLoad;
using UnityEngine;

namespace Project.Scripts.Builder.Building
{
    public class BuildingPlacer : EEBehaviourUpdate
    {
        [SerializeField] private Building building;
        private MapCellHolder mapCellHolderProcessed;
        public EENotifier OffNotifier;
        private EEPointerUI pointerUI;
        protected override void EEAwake()
        {
            base.EEAwake();
            mapCellHolderProcessed = building.mapCellHolder;
            pointerUI = EESingleton.Get<EEPointerUI>();
        }

        protected override void EEEnable()
        {
            base.EEEnable();
            pointerUI.UpNotifier.Event += Place;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            pointerUI.UpNotifier.Event -= Place;
        }

        protected override void EEUpdate()
        {
            base.EEUpdate();
            if(Input.GetKeyDown(KeyCode.Escape))
                Delete();
        }

        private void Place()
        {
            var isDrag = EESingleton.Get<BuilderManager>().IsDrag;

            if (!building.IsValid)
                return;

            building.GetSelf<EEPoolObject>().Spawner.GetParent<Builder.Builder>().Place();
            building.Place(mapCellHolderProcessed.Cell.x, mapCellHolderProcessed.Cell.y);

            if (isDrag) return;
            building.GetSelf<EEPoolObject>().Spawner.GetParent<Builder.Builder>().On();
            GetParent<Building>().mapCellHolder.IsValid = false;
            GetParent<Building>().mapCellHolder.CellUpdateNotifier.Notify();
        }

        private void Delete()
        {
            building.IsValid = false;
            building.GetSelf<EEPoolObject>().Spawner.GetParent<Builder.Builder>().Off();
            OffNotifier.Notify();
        }
    }
}
