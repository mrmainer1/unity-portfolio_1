using Project.EntenEller.Base.Scripts.Advanced.GameObjects;
using Project.EntenEller.Base.Scripts.Patterns.Pool;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Spawns
{
    public class EESpawnerEEPoolObject : EESpawner
    {
        [SerializeField] private int prespawnedAmount = 0;
        public EEPoolObject Prefab;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            EEPool.PreSpawn(Prefab, prespawnedAmount);
        }

        public void ChangePrefab(EEPoolObject prefab)
        {
            Prefab = prefab;
        }
        
        protected override EEGameObject OnSpawn()
        {
            return EEPool.GetFree(Prefab);
        }
        
        public override void Despawn(EEGameObject obj)
        {
            obj.Off();
        }

        public void DespawnPoolObject(EEPoolObject obj)
        {
            SortSpawnArray(obj);
        }
    }
}