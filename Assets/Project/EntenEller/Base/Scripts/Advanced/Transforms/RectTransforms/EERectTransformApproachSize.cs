using Project.EntenEller.Base.Scripts.Advanced.Behaviours;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.Variables.Approach;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class EERectTransformApproachSize : EEBehaviourUpdate
    {
        public RectTransform RectTarget;
        public EEVector2Approach Vector2Approach;
        
        protected override void EEUpdate()
        {
            base.EEUpdate();
            Vector2Approach.Current = GetSelf<RectTransform>().sizeDelta;
            if (RectTarget) Vector2Approach.SetTarget(RectTarget.sizeDelta);
            Vector2Approach.Proceed();
            GetSelf<RectTransform>().sizeDelta = Vector2Approach.Current;
        }

        public void SetRectTarget(RectTransform rectTransform)
        {
            RectTarget = rectTransform;
        }
    }
}
