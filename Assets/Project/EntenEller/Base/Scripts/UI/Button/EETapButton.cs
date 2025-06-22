using Project.EntenEller.Base.Scripts.Timers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Advanced.Inputs.Pointers;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.UI.Button
{
    [ExecutionOrder(9999)]
    public class EETapButton : EEBehaviour
    {
        [SerializeField] private bool isAllowHold;
        [SerializeField] private float inertia = 30f;
        [SerializeField] private float startingTime = 1;
        public EENotifier TapNotifier;
        private float time;

        protected override void EEAwake()
        {
            base.EEAwake();
            GetChild<EEPointer>().DownNotifier.Event += OnDown;
            GetChild<EEPointer>().UpNotifier.Event += OnUp;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            GetChild<EEPointer>().DownNotifier.Event -= OnDown;
            GetChild<EEPointer>().UpNotifier.Event -= OnUp;
        }
        
        private void OnDown()
        {
            time = startingTime;
            TapUpdate();
        }
        
        private void OnUp()
        {
            EETime.StopAllTimersForComponentID(ComponentID);
        }

        private void TapUpdate()
        {
            TapNotifier.Notify();
            if (!isAllowHold) return;
            EETime.StartTimer(new EETime.EETimerData
            {
                Action = () =>
                {
                    time -= inertia * Time.deltaTime;
                    if (time <= 0) time = 0.01f;
                    TapUpdate();
                },
                FinalTime = time,
                IsUnscaled = true,
                ComponentID = ComponentID
            });
        }
    }
}
