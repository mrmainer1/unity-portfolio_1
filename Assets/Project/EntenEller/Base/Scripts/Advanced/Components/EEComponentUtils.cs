using System;
using System.Collections.Generic;
using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.GameObjects;
using Sirenix.Utilities;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.EntenEller.Base.Scripts.Advanced.Components
{
    public static class EEComponentUtils
    {
        public static List<T> FindAll<T>() where T : Component
        {
            var list = new List<T>();
            foreach (var obj in EEGameObjectUtils.EEGameObjects)
            {
                var component = obj.GetSelf<T>();
                if (component.IsNull()) continue;
                list.Add(component);
            }
            return list;
        }

        public static bool IsNull(this object obj)
        {
            return obj == null;
        }
        
        public static bool IsNotNull(this object obj)
        {
            return !IsNull(obj);
        }
        
        public static GameObject Spawn(Type type)
        {
            var obj = EEGameObjectUtils.Create(type);
            obj.AddComponent(type);
            return obj;
        }

        public static Component TryGetComponent(GameObject gameObject, Type type)
        {
            var component = gameObject.GetComponent(type);
            if (component.IsNull()) component = null;
            return component;
        }

        public static List<T> GetComponentsInChildrenExceptItself<T>(this MonoBehaviour behaviour) where T : Component
        {
            var components = behaviour.GetComponentsInChildren<T>(true).ToList();
            var selfComponents = behaviour.GetComponents<T>();
            foreach (var selfComponent in selfComponents)
            {
                components.Remove(selfComponent);
            }
            return components;
        }
    }
}
