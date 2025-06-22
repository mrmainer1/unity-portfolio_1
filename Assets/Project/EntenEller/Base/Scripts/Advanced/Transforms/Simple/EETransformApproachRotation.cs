using Project.EntenEller.Base.Scripts.Advanced.Variables.Approach;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class EETransformApproachRotation : EETransformApproach
    {
        public EEQuaternionApproach Rotation;
        
        protected override void EEEnable()
        {
            base.EEEnable();
            Rotation.Target = Rotation.Current = IsGlobal ? CachedTransform.rotation : CachedTransform.localRotation;
        }
        
        protected override void EESelectableLoop()
        {
            base.EESelectableLoop();
            if (IsGlobal)
            {
                Rotation.Current = CachedTransform.rotation;
                if (Target) Rotation.Target = Target.rotation;
                Rotation.Proceed();
                CachedTransform.rotation = Rotation.Current;
            }
            else
            {
                Rotation.Current = CachedTransform.localRotation;
                if (Target) Rotation.Target = Target.localRotation;
                Rotation.Proceed();
                CachedTransform.localRotation = Rotation.Current;
            }
        }
        
        public void AddX(float x)
        {
            var angle = Rotation.Target.eulerAngles;
            angle.x += x;
            Rotation.SetTarget(Quaternion.Euler(angle));
        }
        
        public void AddY(float y) 
        {
            var angle = Rotation.Target.eulerAngles;
            angle.y += y;
            Rotation.SetTarget(Quaternion.Euler(angle));
        }
        
        public void AddZ(float z) 
        {
            var angle = Rotation.Target.eulerAngles;
            angle.z += z;
            Rotation.SetTarget(Quaternion.Euler(angle));
        }
    }
}
