using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class EETransformPositionObserver : EETransformPositionObserverBase
    {
        protected override void EEFixedUpdate()
        {
            base.EEFixedUpdate();
            Last = Current;
            Current = GetSelf<Transform>().position;
        }
    }
}
