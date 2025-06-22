using Project.EntenEller.Base.Scripts.Advanced.Editors;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using Project.EntenEller.Base.Scripts.Advanced.ForEditor;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Timers;

namespace Project.EntenEller.Base.Scripts.Advanced.Actions
{
    [ExecutionOrder(-9999)]
    public class EEAction : EEBehaviour
    {
        public EEFloatRandomOrDefined Delay;
        public bool IsIgnoreSceneSwitching;
        public bool IsUnscaled;
        public bool IsActiveWhenDisabled;
        public UnityEvent UnityEvent;
        
        [ReadOnly] public EETime.EETimerData EETimerData = new EETime.EETimerData();
        
        [ReadOnly] public bool IsActive;
#if DEBUG
        [ReadOnly] public EEEditorTimePoint TimePoint = new EEEditorTimePoint();
#endif

        protected override void EEAwake()
        {
            base.EEAwake();
            EETimerData.Action = Action;
            EETimerData.ComponentID = ComponentID;
            EETimerData.IsUnscaled = IsUnscaled;
            EETimerData.IsIgnoreSceneSwitching = IsIgnoreSceneSwitching;
        }

        protected override void EEEnable()
        {
            base.EEEnable();
            IsActive = true;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            IsActive = false;
        }

        public void Call()
        {
            EETimerData.FinalTime = Delay.Value;
            EETime.StartTimer(EETimerData);
        }

        public void Stop()
        {
            EETime.StopTimer(EETimerData);
        }

        private void Action()
        {
            if (!IsActive && !IsActiveWhenDisabled) return;
#if DEBUG
            TimePoint.Refresh();
#endif
            UnityEvent.Invoke();
        }
    }
}
