using System;
using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Components;
using Project.EntenEller.Base.Scripts.Advanced.Debugs;
using Project.EntenEller.Base.Scripts.Advanced.GameObjects;
using Project.EntenEller.Base.Scripts.Advanced.Scenes;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Patterns.Singleton
{
    public static class EESingleton
    {
        private static readonly Dictionary<object, object> cache = new Dictionary<object, object>();
        private static bool isSubscribedToSceneSwitch;
        
        public static T Get<T>()
        {
            return (T) Get(typeof(T));
        }
        
        public static object Get(Type type)
        {
            if (!isSubscribedToSceneSwitch)
            {
                isSubscribedToSceneSwitch = true;
                EESceneResource.ScenesFinishedChangesEventRAW += () =>
                {
                    cache.Clear();
                };
            }
            var obj = TryGetCached(type);
            if (obj != null) return obj;
            var count = 0;
            foreach (var eeObj in EEGameObjectUtils.EEGameObjects)
            {
                if (eeObj.IsNull()) continue;
                var component = eeObj.GetSelf(type);
                if (component == null) continue;
                obj = component;
                count++;
#if !DEBUG
                break;
#endif
                // ReSharper disable once HeuristicUnreachableCode
                if (count == 1) continue;
                var mono = (MonoBehaviour) obj;
                var ts = mono.transform;
                var debugInfo = string.Empty;
                while (ts.IsNotNull())
                {
                    debugInfo = ts.gameObject.name + " -> ";
                    ts = ts.parent;
                }
                EEDebug.Log(EEDebugTag.Singleton, debugInfo);
                throw new Exception("There is more than one singleton on scene! " + type);
            }
            if (obj == null)
            {
                obj = EEComponentUtils.Spawn(type).GetComponent(type);
            }
            cache.Add(type, obj);
            return obj;
        }
        
        private static object TryGetCached(Type type)
        {
            cache.TryGetValue(type, out var result);
            return result;
        }
    }
}
