using System;
using System.Collections.Generic;
using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.Components;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Cache.Components.Parent
{
    [DisallowMultipleComponent]
    public class EECachedComponentParent : EECachedComponent
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
            var result = ComponentsAll.TryGetValue(type, out var components);
            if (result) return components;
            components = FindAllComponents(type).ToArray();
#if UNITY_EDITOR
            if (Application.isPlaying)
#endif
            ComponentsAll.Add(type, components);
            return components;
        }
        
        private IEnumerable<Component> FindAllComponents(Type type)
        {
            var list = new List<Component>();
            var currentParent = transform;
            while (true)
            {
                currentParent = currentParent.parent;
                if (currentParent.IsNull()) break;
                var components = currentParent.GetComponents(type).ToList();
                list.AddRange(components);
            }
            return list;
        }
    }
}
