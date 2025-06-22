using System;
using Project.EntenEller.Base.Scripts.Advanced.Components;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Cache.Components.Child
{
    public abstract class EECachedBehaviourChild : EECachedBehaviourMaster
    {
        private EECachedComponentChild child;
        private bool hasCacheChild;
        
        public T GetChild<T>() where T : Component
        {
            return (T) GetChild(typeof(T));
        }
        
        public object GetChild(Type type)
        {
            if (hasCacheChild) return child.Get(type);
            hasCacheChild = true;
            child = CreateCachedComponent<EECachedComponentChild>();
            return child.Get(type);
        }
        
        public T[] GetAllChild<T>() where T : Component
        {
            return Array.ConvertAll(GetAllChild(typeof(T)), a => (T) a);
        }
        
        public Component[] GetAllChild(Type type)
        {
            if (child.IsNull()) child = CreateCachedComponent<EECachedComponentChild>();
            return child.GetAll(type);
        }
    }
}