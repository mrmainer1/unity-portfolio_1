using System;
using Project.EntenEller.Base.Scripts.Advanced.Components;
using Project.EntenEller.Base.Scripts.Cache.Components.Child;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Cache.Components.Neighbor
{
    public abstract class EECachedBehaviourNeighbor : EECachedBehaviourChild
    {
        private EECachedComponentNeighbor neighbor;
        private bool hasCacheNeighbor;
        
        public T GetNeighbor<T>() where T : Component
        {
            return (T) GetNeighbor(typeof(T));
        }
        
        public object GetNeighbor(Type type)
        {
            if (hasCacheNeighbor) return neighbor.Get(type);
            hasCacheNeighbor = true;
            neighbor = CreateCachedComponent<EECachedComponentNeighbor>();
            return neighbor.Get(type);
        }
        
        public T[] GetAllNeighbor<T>() where T : Component
        {
            return Array.ConvertAll(GetAllNeighbor(typeof(T)), a => (T) a);
        }
        
        public Component[] GetAllNeighbor(Type type)
        {
            if (neighbor.IsNull()) neighbor = CreateCachedComponent<EECachedComponentNeighbor>();
            return neighbor.GetAll(type);
        }
    }
}
