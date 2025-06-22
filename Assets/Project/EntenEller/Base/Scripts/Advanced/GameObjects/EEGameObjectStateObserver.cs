using System;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Project.EntenEller.Base.Scripts.Advanced.GameObjects
{
    public class EEGameObjectStateObserver : EEBehaviour
    {
        public event Action<bool> StateChangedEvent;
        public EENotifier EnableNotifier;
        public EENotifier DisableNotifier;
        
        protected override void EEEnable()
        {
            StateChangedEvent.Call(true);
            EnableNotifier.Notify();
        }

        protected override void EEDisable()
        {
            StateChangedEvent.Call(false);
            DisableNotifier.Notify();
        }
    }
}
