using Project.EntenEller.Base.Scripts.Advanced.Inputs.Pointers;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Project.EntenEller.Base.Scripts.Mobile
{
    public class EETouchPlatformNotifier : EEBehaviour
    {
        public EENotifier TouchNotifier, NonTouchNotifier;

        public void Check()
        {
            if (EEPointerUtils.IsTouchDevice()) TouchNotifier.Notify();
            else NonTouchNotifier.Notify();
        }
    }
}
