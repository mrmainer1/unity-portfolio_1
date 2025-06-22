using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Inputs.Misc
{
    public class EEMouseCursor : EEBehaviour
    {
        public bool IsLocked { get; private set; }
        public EENotifier LockedNotifier;
        public EENotifier UnlockedNotifier;

        public void Lock()
        {
            IsLocked = true;
            LockedNotifier.Notify();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void Unlock()
        {
            IsLocked = false;
            UnlockedNotifier.Notify();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
