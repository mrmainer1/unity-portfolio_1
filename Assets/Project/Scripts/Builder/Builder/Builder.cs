using Project.EntenEller.Base.Scripts.Advanced.GameObjects;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Advanced.Spawns;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using Project.EntenEller.Base.Scripts.UI.Toggles;
using Project.Scripts.Builder.Building;

namespace Project.Scripts.Builder.Builder
{
    public class Builder : EEBehaviour
    {
        public int LastQuadrant;
        public EESpawnerEEPoolObject spawner;
        public EENotifier PlaceNotifier, OffNotifier, OnNotifier, SelectedNotifier, UnselectedNotifier;
        private BuilderManager builderManager;

        protected override void EEAwake()
        {
            base.EEAwake();
            builderManager = EESingleton.Get<BuilderManager>();
        }

        public void On()
        {
            if (builderManager.CurrentBuilding != null)
            {
                spawner.Despawn(builderManager.CurrentBuilding.GetEEGameObject());
                builderManager.ClearBuilding();
            }
                
            var obj = spawner.Spawn();
            builderManager.SetBuilding(obj.GetSelf<Building.Building>());
            builderManager.CurrentBuilding.GetChild<BuildingRotator>()?.SetQuadrant(LastQuadrant);
            builderManager.SetActiveAllBuilder();
            SelectedNotifier.Notify();
            OnNotifier.Notify();
        }

        public void Off()
        {
            if (builderManager.CurrentBuilding != null)
            {
                spawner.Despawn(builderManager.CurrentBuilding.GetEEGameObject());
                builderManager.ClearBuilding();
            }
            GetChild<EEToggle>().Enable();
            
            OffNotifier.Notify();
            UnselectedNotifier.Notify();
        }

        public void Place()
        {
            if (builderManager.CurrentBuilding.GetChild<BuildingRotator>())
                LastQuadrant = builderManager.CurrentBuilding.GetChild<BuildingRotator>().Quadrant;
            
            GetChild<EEToggle>().Enable();
            builderManager.ClearBuilding();
            PlaceNotifier.Notify();
        }
    }
}
