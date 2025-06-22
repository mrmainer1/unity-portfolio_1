using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using Project.EntenEller.Base.Scripts.Save;

namespace Project.EntenEller.Base.Scripts.Advanced.Store
{
    [ExecutionOrder(9999)]
    public class EEStoreItem : EEBehaviour
    {
        public EENotifier BuyNotifier;
        
        protected override void EEEnable()
        {
            base.EEEnable();
            if (EESingleton.Get<EEStoreManager>().IsLocalMode)
            {
                if (!GetSelf<EESaver>()) return;
               // if (GetSelf<EESaver>().Data.LoadBool()) Buy();
            }
        }

        public void Buy()
        {
            BuyNotifier.Notify();
            if (EESingleton.Get<EEStoreManager>().IsLocalMode)
            {
                if (!GetSelf<EESaver>()) return;
             //   GetSelf<EESaver>().Data.Value = true;
              //  GetSelf<EESaver>().Data.Save();
            }
        }
    }
}
