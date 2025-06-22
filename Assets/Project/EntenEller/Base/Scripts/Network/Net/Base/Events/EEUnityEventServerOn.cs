using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;

namespace Project.EntenEller.Base.Scripts.Network.Net.Base.Events
{
    public class EENotifierServerOn : EEBehaviour
    {
        public EENotifier ServerOnNotifier;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            if (EESingleton.Get<EENetServer>().IsActive)
            {
                ServerOnNotifier.Notify();
            }
            else
            {
                EESingleton.Get<EENetServer>().OnEvent += On;
                void On()
                {
                    if (enabled) ServerOnNotifier.Notify();
                }
            }
        }
    }
}