using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Cache.Components.Master
{
    public abstract class EECachedComponent : MonoBehaviour
    {
        protected readonly Dictionary<Type, Component> ComponentsSingle = new Dictionary<Type, Component>();
        protected readonly Dictionary<Type, Component[]> ComponentsAll = new Dictionary<Type, Component[]>();

        public void Clean()
        {
            ComponentsSingle.Clear();
            ComponentsAll.Clear();
        }
    }
}