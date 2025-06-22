using System.Collections.Generic;
using MEC;
using Project.EntenEller.Base.Scripts.Advanced.Components;
using Project.EntenEller.Base.Scripts.UI.Menu;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Animators
{
    [RequireComponent(typeof(Animator))]
    public class EEAnimatorUI : EEAnimatorBase
    {
        private EEMenuView menu;

        protected override void EEAwake()
        {
            base.EEAwake();
            GetSelf<Animator>().enabled = false;
        }

        protected override void EEEnable()
        {
            base.EEEnable();
            menu = GetSelf<EEMenuView>();
            if (menu.IsNull()) menu = GetParent<EEMenuView>();
            menu.StartShowNotifier.Event += StartAnimation;
            menu.FinishHideNotifier.Event += StopAnimation;
            if (menu.GetSelf<EEMenu>().IsActive) StartAnimation();
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            menu.StartShowNotifier.Event -= StartAnimation;
            menu.FinishHideNotifier.Event -= StopAnimation;
            StopAnimation();
        }

        public void StartAnimation()
        {
            Timing.KillCoroutines(ComponentID);
            Timing.RunCoroutine(Animate(), Segment.RealtimeUpdate, ComponentID);
        }
        
        public void StopAnimation()
        {
            Timing.KillCoroutines(ComponentID);
            Timing.RunCoroutine(DisableAnimator(), Segment.RealtimeUpdate, ComponentID);
        }

        private IEnumerator<float> DisableAnimator()
        {
            yield return Timing.WaitForOneFrame;
            if (this != null) GetSelf<Animator>().enabled = false;
        }

        private IEnumerator<float> Animate()
        {
            GetSelf<Animator>().enabled = true;
            while (true)
            {
                yield return Timing.WaitForOneFrame;
                PlayAnimation();
            }
        }
    }
}