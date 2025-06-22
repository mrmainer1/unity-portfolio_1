using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Physics
{
    public static class EEPhysicsUtils
    {
        public static readonly RaycastHit[] Hits = new RaycastHit[256];
        private static readonly RaycastHitComparer comparer = new();
        
        public static int RayCast(Vector3 start, Vector3 direction, LayerMask layerMask)
        {
            var amount = UnityEngine.Physics.RaycastNonAlloc(start, direction, Hits, float.PositiveInfinity, layerMask);
            if (amount == 0) return amount;
            Array.Sort(Hits, 0, amount, comparer);
            return amount;
        }
        
        private class RaycastHitComparer : IComparer<RaycastHit>
        {
            public int Compare(RaycastHit x, RaycastHit y)
            {
                return x.distance.CompareTo(y.distance);
            }
        }
    }
}