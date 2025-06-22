using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class EERectTransformPositionObserver : EETransformPositionObserverBase
    {
        protected override void EEFixedUpdate()
        {
            base.EEFixedUpdate();
            Last = Current;
            Current = GetSelf<RectTransform>().anchoredPosition;
        }
        
        public void Restart()
        {
            Current = GetSelf<RectTransform>().anchoredPosition;
        }
    }
}
