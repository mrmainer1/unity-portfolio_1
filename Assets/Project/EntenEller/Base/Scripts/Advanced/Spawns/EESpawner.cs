using System;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.GameObjects;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Advanced.Tags;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Spawns
{
    public abstract class EESpawner : EEBehaviour
    {
        [SerializeField] private int length = 1000; 
        public int ActiveAmount => Index;
        [NonSerialized] public int Index;
        public EEGameObject[] SpawnList;
        
        [SerializeField] protected EEGameObjectFinder parentToStick;
        [SerializeField] private bool isGetOnlyParentTransformValues;
        
        public event Action<EEGameObject> BeforeEnable;
        [SerializeField] private bool isResetRPS = true;
        public EENotifier SpawnNotifier;
        private EEGameObject eeGameObject;

        protected override void EEAwake()
        {
            base.EEAwake();
            SpawnList = new EEGameObject[length];
            eeGameObject = GetComponent<EEGameObject>();
            if (eeGameObject == null) throw new Exception("Require EEGameObject or EEPoolObject!");
        }

        public EEGameObject Spawn(bool isActiveAfterSpawn = true)
        {
            var obj = OnSpawn();
            obj.SetSpawner(this);
            var parent = parentToStick.GetSingle(this);
            if (isGetOnlyParentTransformValues)
            {
                var they = parent.transform;
                var me = obj.transform;
                me.position = they.position;
                me.rotation = they.rotation;
            }
            else
            {
                obj.SetParent(parentToStick.GetSingle(this), isResetRPS);
            }
            SpawnList[Index] = obj;
            obj.SetSpawnData(Index);
            Index++;
            BeforeEnable.Call(obj);
            if (isActiveAfterSpawn) obj.On();
            SpawnNotifier.Notify();
#if UNITY_EDITOR
            if (!obj.gameObject.name.Contains("[SPAWNED]"))
            {
                var o = obj.gameObject;
                o.name = "[SPAWNED] " + o.name + " " + obj.ComponentID;
            }
#endif
            return obj;
        }
        
        public void SimpleSpawn()
        {
            Spawn();
        }
        
        public void SimpleSpawnMultiple(int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                Spawn();
            }
        }
        
        public void DespawnAll()
        {
            while (Index != 0)
            {
                Despawn(SpawnList[0]);
            }
        }

        protected void SortSpawnArray(EEGameObject obj)
        {
            Index--;
            var last = SpawnList[Index];
            last.SpawnIndex = obj.SpawnIndex;
            SpawnList[obj.SpawnIndex] = SpawnList[Index];
        }
        
        public abstract void Despawn(EEGameObject obj);
        protected abstract EEGameObject OnSpawn();
    }
}