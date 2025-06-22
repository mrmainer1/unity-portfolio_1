using System;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using Project.EntenEller.Base.Scripts.Advanced.Debugs;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.GameObjects;
using Object = UnityEngine.Object;

namespace Project.EntenEller.Base.Scripts.Advanced.Spawns
{
    public static class EESpawnUtils
    {
        public static event Action<EEGameObject> EarlySpawnEvent, SpawnEvent;
        public static event Action SpawnDoneEvent;
        private static bool isInit;
        private static bool isSpawn;
        
        public static T Spawn<T>(T original) where T : Component
        {
            if (isInit)
            {
                isInit = true;
                Timing.RunCoroutine(LateUpdate(), Segment.LateUpdate);
            }
            isSpawn = true;
            var gameObject = original.gameObject;
            gameObject.SetActive(false);
            var obj = Object.Instantiate(original);
            gameObject.SetActive(true);
            var eeGameObject = obj.GetEEGameObject();
            eeGameObject.Spawn();
#if UNITY_EDITOR
            EEDebug.Log(eeGameObject);
#endif
            EarlySpawnEvent.Call(eeGameObject);
            SpawnEvent.Call(eeGameObject);
            return obj;
        }


        private static IEnumerator<float> LateUpdate()
        {
            while (true)
            {
                yield return Timing.WaitForOneFrame;
                if (!isSpawn) continue;
                isSpawn = false;
                SpawnDoneEvent.Call();
            }
        }
    }
}