using System;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Project.EntenEller.Base.Scripts.Mobile
{
    public class EEMobileKeyboard : EEBehaviour
    {
        public event Action<string> TypeEvent;
        public EENotifier OnEventEE, OffEventEE, TypeEventEE;
        public bool IsOn;
        
        public void On()
        {
            IsOn = true;
            OnEventEE.Notify();
        }

        public void Off()
        {
            IsOn = false;
            OffEventEE.Notify();
        }
        
        public void Call(string ch)
        {
            TypeEvent.Call(ch);
            TypeEventEE.Notify();
        }
    }
}
