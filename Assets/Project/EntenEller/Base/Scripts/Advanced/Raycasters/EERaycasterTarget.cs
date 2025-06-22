using System;
using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Project.EntenEller.Base.Scripts.Advanced.Raycasters
{
    public class EERaycasterTarget : EEBehaviour
    {
        public event Action<EERaycaster> OnEvent, OffEvent;
        public EENotifier OnNotifier, OffNotifier;
        
        public List<EERaycaster> Raycasters = new List<EERaycaster>();
        
        public void On(EERaycaster raycaster)
        {
            Raycasters.Add(raycaster);
            OnEvent.Call(raycaster);
            OnNotifier.Notify();
        }
        
        public void Off(EERaycaster raycaster)
        {
            Raycasters.Remove(raycaster);
            OffEvent.Call(raycaster);
            OffNotifier.Notify();
        }
    }
}