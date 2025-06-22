using System;
using Project.EntenEller.Base.Scripts.Advanced.Variables.Approach;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class EETransformApproachScale : EETransformApproach
    {
        public EEVector3Approach Scale;

        protected override void EEEnable()
        {
            base.EEEnable();
            Scale.Target = Scale.Current = IsGlobal ? CachedTransform.lossyScale : CachedTransform.localScale;
        }

        protected override void EESelectableLoop()
        {
            base.EESelectableLoop();
            if (IsGlobal)
            {
                throw new Exception("Impossible to do scaling in world!");
            }
            else
            {
                Scale.Current = CachedTransform.localScale;
                if (Target) Scale.Target = Target.localScale;
                Scale.Proceed();
                CachedTransform.localScale = Scale.Current;
            }
        }

        public void SetCurrent(Vector3 scale)
        {
            Scale.Current = scale;
            CachedTransform.localScale = scale;
        }
    }
}
