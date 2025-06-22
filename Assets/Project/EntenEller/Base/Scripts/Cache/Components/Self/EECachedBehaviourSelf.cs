using System;
using Project.EntenEller.Base.Scripts.Advanced.Components;
using Project.EntenEller.Base.Scripts.Cache.Components.Neighbor;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Cache.Components.Self
{
    public abstract class EECachedBehaviourSelf : EECachedBehaviourNeighbor
    {
        private EECachedComponentSelf self;
        private bool hasCacheSelf;
        
        public T GetSelf<T>() where T : Component
        {
#if UNITY_EDITOR
            if (!Application.isPlaying) return GetComponent<T>();
#endif
            return (T) GetSelf(typeof(T));
        }
        
        public object GetSelf(Type type)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying) return GetComponent(type);
#endif
            if (hasCacheSelf) return self.Get(type);
            hasCacheSelf = true;
            self = CreateCachedComponent<EECachedComponentSelf>();
            return self.Get(type);
        } 
        
        public T[] GetAllSelf<T>() where T : Component
        {
            return Array.ConvertAll(GetAllSelf(typeof(T)), a => (T) a);
        }
        
        public Component[] GetAllSelf(Type type)
        {
            if (self.IsNull()) self = CreateCachedComponent<EECachedComponentSelf>();
            return self.GetAll(type);
        }
    }
}
