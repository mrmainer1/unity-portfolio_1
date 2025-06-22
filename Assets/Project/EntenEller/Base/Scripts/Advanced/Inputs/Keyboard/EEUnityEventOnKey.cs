using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Inputs.Keyboard
{
    public class EEUnityEventOnKey : EEBehaviour
    {
        [SerializeField] private KeyCode keyCode;
        public EENotifier OnNotifier, OffNotifier;
        
        private void LateUpdate()
        {
            if (Input.GetKeyDown(keyCode)) OnNotifier.Notify();
            if (Input.GetKeyUp(keyCode)) OffNotifier.Notify();
        }
    }
}
