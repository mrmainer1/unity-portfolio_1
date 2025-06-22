using System;
using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Dictionaries;
using Project.EntenEller.Base.Scripts.Advanced.Tags;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Raycasters
{
    public abstract class EERaycaster : EEBehaviour
    {
        protected readonly RaycastHit[] Hits = new RaycastHit[256];
#if UNITY_EDITOR
        [SerializeField] [ReadOnly] private EEDictionary<Transform, float> DebugDictionary = new EEDictionary<Transform, float>();
#endif
        [SerializeField] protected float Distance = float.PositiveInfinity;
        [SerializeField] protected LayerMask LayerMask;
        [SerializeField] private List<string> eeTags;
        [SerializeField] private EERaycasterTarget targetCurrent;
        private EERaycasterTarget target;
        private Comparer<RaycastHit> comparer;

        public void ResetTarget()
        {
            targetCurrent = null;
            comparer = Comparer<RaycastHit>.Create((a, b) => a.distance.CompareTo(b.distance));
        }
        
        public void Call()
        {
            target = null;
            
            var amount = CastRay();
            Array.Sort(Hits, 0, amount, comparer);
            
#if UNITY_EDITOR
            DebugDictionary.ClearAll();
            for (var i = 0; i < amount; i++) 
            { 
                var hit = Hits[i];
                DebugDictionary.Add(hit.transform, hit.distance);
            }
#endif
            
            for (var i = 0; i < amount; i++)
            {
                var hit = Hits[i];
                if (Hit(hit.transform)) break;
            }
            
            if (targetCurrent == target) return;
            if (targetCurrent) targetCurrent.Off(this);
            if (target) target.On(this);
            
            targetCurrent = target;
        }
        
        public abstract int CastRay();

        public bool Hit(Transform ts)
        {
            var rt = ts.GetComponent<EERaycasterTarget>();
            if (!rt) return false;
            if (!rt.GetSelf<EETagHolder>().IsHavingAnyTag(eeTags)) return false;
            target = rt;
            return true;
        }
    }
}