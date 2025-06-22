using System;
using Project.EntenEller.Base.Scripts.Advanced.Components;
using Project.EntenEller.Base.Scripts.Cache.Components.Self;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Cache.Components.Parent
{
    public abstract class EECachedBehaviourParent : EECachedBehaviourSelf
    {
        private EECachedComponentParent parent;
        private bool hasCacheParent;
        
        public T GetParent<T>() where T : Component
        {
            return (T) GetParent(typeof(T));
        }
        
        public object GetParent(Type type)
        {
            if (hasCacheParent) return parent.Get(type);
            hasCacheParent = true;
            parent = CreateCachedComponent<EECachedComponentParent>();
            return parent.Get(type);
        } 
        
        public T[] GetAllParent<T>() where T : Component
        {
            return Array.ConvertAll(GetAllParent(typeof(T)), a => (T) a);
        }
        
        public Component[] GetAllParent(Type type)
        {
            if (parent.IsNull()) parent = CreateCachedComponent<EECachedComponentParent>();
            return parent.GetAll(type);
        }
    }
}
