using Project.EntenEller.Base.Scripts.Advanced.Debugs;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Timers;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Actions
{
    public class EEActionRepeat : EEBehaviour
    {
        [SerializeField] protected EEFloatRandomOrDefined delayToRepeat;
        public EEAction Action;
        
        public void Call()
        {
            if (delayToRepeat.Value == 0)
            {
                EEException.Call(this, "Delay is almost zero! " + gameObject.name);
                return;
            }
            SetTimer();
        }

        public void Change(EEFloatRandomOrDefined delay)
        {
            delayToRepeat = delay;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            EETime.StopTimer(CallRepeat);
        }

        private void CallRepeat()
        {
            Task();
            CallWithTimer();
        }

        private void SetTimer()
        {
            EETime.StopTimer(CallRepeat);
            CallWithTimer();
        }

        private void CallWithTimer()
        {
            EETime.StartTimer(new EETime.EETimerData
            {
                ComponentID = ComponentID,
                Action = CallRepeat,
                FinalTime = delayToRepeat.Value
            });
        }

        private void Task()
        {
            Action.Call();
        }
    }
}
