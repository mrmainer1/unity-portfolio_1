using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.GameObjects;
using Project.EntenEller.Base.Scripts.Advanced.Variables.Collections;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Spawns
{
    public abstract class EESpawnZone : EEBehaviour
    {
        [SerializeField] private float distanceBetween;

        public bool TryGeneratePosition(EEGameObject obj, out Vector3 position)
        {
            position = default;
            
            var list = new List<Vector3>();
            var spawner = obj.Spawner;
            for (var i = 0; i < spawner.Index; i++)
            {
                var spawned = spawner.SpawnList[i];
                if (spawned == obj) continue;
                list.Add(spawned.transform.position);
            }
            
            var isOk = TryGenerate(out position);
            if (!isOk) return false;
            if (list.Count == 0) return true;

            while (true)
            {
                again:
                TryGenerate(out position);
                foreach (var v3 in list)
                {
                    if (Vector3.Distance(position, v3) < distanceBetween) goto again;
                }
                return true;
            }
        }

        protected abstract bool TryGenerate(out Vector3 position);
    }
}