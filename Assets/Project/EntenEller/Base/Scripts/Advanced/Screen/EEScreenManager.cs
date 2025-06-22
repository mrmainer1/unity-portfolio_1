using System;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Project.EntenEller.Base.Scripts.Timers;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Screen
{
    [ExecutionOrder(9999)]
    public class EEScreenManager : EEBehaviourUpdate
    {
        public EENotifier ResolutionChangeNotifier = new EENotifier();

        private Vector2 _resolution;
        public Vector2 Resolution
        {
            get
            {
                if (_resolution != Vector2.zero) return _resolution;
                _resolution = new Vector2(UnityEngine.Screen.width, UnityEngine.Screen.height);
                return _resolution;
            }
        }

        protected override void EEUpdate()
        {
            base.EEUpdate();
            var resolution = new Vector2(UnityEngine.Screen.width, UnityEngine.Screen.height);
            if (resolution.x.IsAlmostEqual(Resolution.x) && resolution.y.IsAlmostEqual(Resolution.y)) return;
            _resolution = resolution;
            EETime.StopAllTimersForComponentID(ComponentID);
            EETime.StartTimer(new EETime.EETimerData
            {
                Action = () =>
                {
                    ResolutionChangeNotifier.Notify();
                },
                ComponentID = ComponentID
            });
        }
    }
}
