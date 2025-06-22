using System;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.UI.Text;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Timers
{
    public class EETimerEEText : EEBehaviour
    {
        [SerializeField] private EETimer timer;
        [SerializeField] private string format = @"mm\:ss";
        [SerializeField] private bool isReversed;

        protected override void EEEnable()
        {
            base.EEEnable();
            timer.TimerUpdateEvent += TimerUpdate;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            timer.TimerUpdateEvent -= TimerUpdate;
        }

        private void TimerUpdate(float time)
        {
#if UNITY_EDITOR
            if (this == null) return;
#endif
            var delta = 0f;
            if (isReversed) delta = time - timer.FinalTime;
            else delta = timer.FinalTime - time;
            var str = TimeSpan.FromSeconds(delta).ToString(format);
            GetSelf<EETextSimple>().SetData(str);
        }
    }
}
