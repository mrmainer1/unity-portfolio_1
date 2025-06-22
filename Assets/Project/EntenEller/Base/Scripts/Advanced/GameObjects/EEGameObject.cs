using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Advanced.Spawns;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.GameObjects
{
    [ExecutionOrder(-9999)]
    [DisallowMultipleComponent] 
    public class EEGameObject : EEGameObjectParentController
    {
        public EEGameObject Prefab;
        
        [ReadOnly] public bool IsActive = false;
        [ReadOnly] public EESpawner Spawner;

        public EENotifier OnNotifier, OffNotifier;
        public int SpawnIndex;

        protected override void EEAwake()
        {
            IsActive = true;
            base.EEAwake();
        }

        protected override void EEEnable()
        {
            IsActive = true;
            base.EEEnable();
        }

        protected override void EEDisable()
        {
            IsActive = false;
            base.EEDisable();
        }

        public void On()
        {
            if (IsActive) return;
            gameObject.SetActive(true);
            OnNotifier.Notify();
        }
        
        public void Off()
        {
            if (!IsActive) return;
            gameObject.SetActive(false);
            OffNotifier.Notify();
        }

        public void Spawn()
        {
            EEGameObjectUtils.Add(this);
            foreach (var ts in GetAllChild<Transform>())
            {
                EEGameObjectUtils.Add(ts.GetEEGameObject());
            }
        }

        public void SetState(bool isOn)
        {
            if (isOn) On();
            else Off();
        }
        
        public void DisableAndRemoveFromParent()
        {
            Off();
            UnsetParent();
        }

        public void Destroy()
        {
            UnsetParent();
            Destroy(gameObject);
        }
        
        public void SetSpawner(EESpawner spawner)
        {
            Spawner = spawner;
        }
        
        public void SetSpawnData(int index)
        {
            SpawnIndex = index;
        }
    }
}