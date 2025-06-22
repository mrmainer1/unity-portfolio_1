using Project.EntenEller.Base.Scripts.Advanced.Tags;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Triggers
{
    [ExecuteAfter(typeof(EETriggerColliderListener))]
    public class EETriggerApprover : EEBehaviour
    {
        protected override void EEEnable()
        {
            base.EEEnable();
            GetSelf<EETriggerColliderListener>().RawEnterEvent += RawEnter;
            GetSelf<EETriggerColliderListener>().RawExitEvent += RawExit;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            GetSelf<EETriggerColliderListener>().RawEnterEvent -= RawEnter;
            GetSelf<EETriggerColliderListener>().RawExitEvent -= RawExit;
        }

        public void RawEnter (Component component)
        {
            var trigger = IsHavingTrigger(component);
            if (!trigger) return;
            GetSelf<EETrigger>().Add(trigger, IsHavingSameEETag(trigger));
        }
        
        public void RawExit (Component component)
        {
            var trigger = IsHavingTrigger(component);
            if (!trigger) return;
            GetSelf<EETrigger>().Remove(trigger, IsHavingSameEETag(trigger));
        }
        
        private static EETrigger IsHavingTrigger(Component component)
        {
            return component.GetComponent<EETrigger>();
        }
        
        private bool IsHavingSameEETag(EETrigger trigger)
        {
            var th = trigger.GetSelf<EETagHolder>();
            return th.IsHavingAnyTag(GetSelf<EETrigger>().CollidingTags);
        }
    }
}
