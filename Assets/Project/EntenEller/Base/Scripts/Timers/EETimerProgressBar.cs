using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.UI.ProgressBar;
using UnityEngine;
using UnityEngine.UI;

namespace Project.EntenEller.Base.Scripts.Timers
{
    public class EETimerProgressBar : EEBehaviour
    {        
        [SerializeField] private EETimer timer;
        [SerializeField] private bool isReverse;
        
        protected override void EEEnable()
        {
            base.EEEnable();
            timer.TimerUpdateEvent += TimerUpdate;
            timer.BeginNotifier.Event += Begin;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            timer.TimerUpdateEvent -= TimerUpdate;
            timer.BeginNotifier.Event -= Begin;
        }

        private void Begin()
        {
            if (!isReverse)
            {
                GetSelf<EEProgressBar>().SetCurrent(1);
                GetSelf<EEProgressBar>().SetTarget(1);
            }
            else
            {
                GetSelf<EEProgressBar>().SetCurrent(0);
                GetSelf<EEProgressBar>().SetTarget(0);
            }
        }
        
        private void TimerUpdate(float time)
        {
#if UNITY_EDITOR
            if (this == null) return;
#endif
            if (isReverse)
            {
                var r = timer.FinalTime.IsAlmostZero() ? 0 : time / timer.FinalTime;
                GetSelf<EEProgressBar>().SetTarget(r);
            }
            else
            {
                var r = timer.FinalTime.IsAlmostZero() ? 1 : (1 - time / timer.FinalTime);
                GetSelf<EEProgressBar>().SetTarget(r);
            }
        }
    }
}
