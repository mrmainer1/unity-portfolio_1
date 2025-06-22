using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public abstract class EETransformApproach : EEBehaviourSelectableLoop
    {
        public Transform Target;
        public bool IsGlobal;
        private Transform _CachedTransform;

        protected Transform CachedTransform
        {
            get
            {
                if (_CachedTransform) return _CachedTransform;
                _CachedTransform = GetSelf<Transform>();
                return _CachedTransform;
            }
        }

        public void SetTarget(Transform target)
        {
            Target = target;
        }
        
        public void RemoveTarget()
        {
            Target = null;
        }
    }
}
