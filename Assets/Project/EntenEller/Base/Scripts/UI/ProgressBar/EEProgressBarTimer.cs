using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Timers;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.UI.ProgressBar
{
    public class EEProgressBarTimer : EETimer
    {
        private EEProgressBar progressBar;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            progressBar = GetComponent<EEProgressBar>();
        }

        protected override void EEEnable()
        {
            base.EEEnable();
            TimerUpdateEvent += OnUpdateTimer;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            TimerUpdateEvent -= OnUpdateTimer;
        }

        private void OnUpdateTimer(float current)
        {
            progressBar.SetTarget(current / FinalTime);
        }
    }
}
