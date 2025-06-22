using System;
using System.Collections.Generic;
using System.Linq;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Cache.Components.Child
{
    [DisallowMultipleComponent]
    public class EECachedComponentChild : EECachedComponent
    {
        public object Get(Type type)
        {
            var result = ComponentsSingle.TryGetValue(type, out var component);
            if (result) return component;
            component = FindAllComponents(type).FirstOrDefault();
#if UNITY_EDITOR
            if (Application.isPlaying)
#endif
            ComponentsSingle.Add(type, component);
            return component;
        }
        
        public Component[] GetAll(Type type)
        {
            var result = ComponentsAll.TryGetValue(type, out var list);
            if (result) return list;
            var components = FindAllComponents(type).ToArray();
#if UNITY_EDITOR
            if (Application.isPlaying)
#endif
            ComponentsAll.Add(type, components);
            return components;
        }

        private IEnumerable<Component> FindAllComponents(Type type)
        {
            return transform.GetComponentsInChildren(type,true).Where(a => a.gameObject != gameObject);
        }
    }
}