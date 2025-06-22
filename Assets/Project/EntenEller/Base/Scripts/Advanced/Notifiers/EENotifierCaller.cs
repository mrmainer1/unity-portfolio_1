using Project.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Project.EntenEller.Base.Scripts.Advanced.Notifiers
{
    public class EENotifierCaller : EEBehaviour
    {
        public EENotifier Notifier;

        public void Notify()
        {
            Notifier.Notify();
        }
    }
}
