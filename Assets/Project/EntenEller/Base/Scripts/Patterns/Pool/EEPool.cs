using System;
using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Scenes;
using Project.EntenEller.Base.Scripts.Advanced.Spawns;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.EntenEller.Base.Scripts.Patterns.Pool
{
    public static class EEPool
    {
        private static readonly Dictionary<EEPoolObject, PoolObjectData[]> pool = new();
        private static bool isInit;
        
        public static EEPoolObject GetFree(EEPoolObject origin)
        {
            if (!isInit)
            {
                isInit = true;
                EESceneResource.ScenesFinishedChangesEventRAW += () =>
                {
                    foreach (var array in pool)
                    {
                        foreach (var data in array.Value)
                        {
                            if (data.IsExist) Object.Destroy(data.PoolObject);
                        }
                    }
                    pool.Clear();
                };
            }

            CheckAndAddOriginArray(origin);
            var array = pool[origin];

            for (var j = 0; j < array.Length; j++)
            {
                var data = array[j];
                if (!data.IsExist) return Spawn(origin, j);
                if (data.PoolObject.IsActive) continue;
                if (data.PoolObject.FrameEnabled == Time.frameCount) continue;
                if (Time.time < data.PoolObject.DisableTime + data.PoolObject.DelayTimeBeforeNextActive) continue;
                return data.PoolObject;
            }
            
            throw new Exception("Cant create or find free pool object! " + origin.Origin);
        }

        private static EEPoolObject Spawn(EEPoolObject origin, int j)
        {
            var obj = EESpawnUtils.Spawn(origin);
            pool[origin][j].Set(obj);
            obj.Origin = origin;
            obj.J = j;
            return obj;
        }

        public static void PreSpawn(EEPoolObject origin, int amount)
        {
            CheckAndAddOriginArray(origin);
            for (var i = 0; i < amount; i++)
            {
                var array = pool[origin];
                if (array[i].IsExist) continue;
                Spawn(origin, i);
            }
        }

        private static void CheckAndAddOriginArray(EEPoolObject origin)
        {
            if (!pool.ContainsKey(origin)) pool.Add(origin, new PoolObjectData[10001]);
        }

        public static int GetIndex (EEPoolObject poolObject)
        {
            var array = pool[poolObject.Origin];
            return Array.IndexOf(array, poolObject);
        }
        
        public static int GetFromIndexes(EEPoolObject poolObject)
        {
            var array = pool[poolObject.Origin];
            return Array.IndexOf(array, poolObject);
        }

        private struct PoolObjectData
        {
            public EEPoolObject PoolObject;
            public bool IsExist;

            public void Set(EEPoolObject poolObject)
            {
                PoolObject = poolObject;
                IsExist = true;
            }
        }
    }
}