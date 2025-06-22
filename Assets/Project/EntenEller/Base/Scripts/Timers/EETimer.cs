using System;
using UnityEngine;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using EEBehaviour = Project.EntenEller.Base.Scripts.Cache.Components.Master.EEBehaviour;

namespace Project.EntenEller.Base.Scripts.Timers
{
    public class EETimer : EEBehaviour
    {
        public EENotifier BeginNotifier;
        public EENotifier PauseNotifier;
        public EENotifier ResumeNotifier;
        public EENotifier CancelNotifier;
        public EENotifier EndSuccessNotifier;
        public EENotifier EndAnyNotifier;
        
        public event Action<float> TimerUpdateEvent;

        public float FinalTime;
        public float CurrentTime;
        
        [SerializeField] private bool isCallingUpdate = true;
        [SerializeField] private bool isUnscaled;
        [SerializeField] private bool isIgnoreSceneSwitching = true;
        [SerializeField] private EETime.EETimerData timerData = new();

        protected override void EEAwake()
        {
            base.EEAwake();
            timerData.IsIgnoreSceneSwitching = isIgnoreSceneSwitching;
            timerData.ComponentID = ComponentID;
            timerData.IsUnscaled = isUnscaled;
            timerData.IsNotifyingUpdate = isCallingUpdate;
            timerData.Action = Complete;
            timerData.UpdateNotifier.Event += TimerUpdate;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            timerData.UpdateNotifier.Event -= TimerUpdate;
        }

        public void Begin()
        {
            timerData.CurrentTime = 0;
            timerData.FinalTime = FinalTime;
            TimerUpdate();
            EETime.StartTimer(timerData);
            BeginNotifier.Notify();
        }
        
        public void Pause()
        {
            EETime.PauseTimer(timerData);
            PauseNotifier.Notify();
        }
        
        public void Resume()
        {
            EETime.ResumeTimer(timerData);
            ResumeNotifier.Notify();
        }
        
        public void Cancel()
        {
            EETime.StopTimer(timerData);
            CancelNotifier.Notify();
            EndAnyNotifier.Notify();
        }
        
        private void Complete()
        {
            EETime.StopTimer(timerData);
            EndSuccessNotifier.Notify();
            EndAnyNotifier.Notify();
        }

        private void TimerUpdate()
        {
            CurrentTime = timerData.CurrentTime;
            TimerUpdateEvent.Call(CurrentTime);
        }

        public void SetFinal(float time)
        {
            FinalTime = time;
        }
    }
}
