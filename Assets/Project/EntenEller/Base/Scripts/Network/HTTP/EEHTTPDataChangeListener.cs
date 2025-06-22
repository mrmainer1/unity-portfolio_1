using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Project.EntenEller.Base.Scripts.Network.HTTP
{
    public class EEHTTPDataChangeListener : EEBehaviour
    {
        public string DataCurrent;
        public EENotifier ChangeNotifier;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            GetSelf<EEHTTPStringReceiver>().SuccessEvent += data =>
            {
                if (DataCurrent != string.Empty && DataCurrent != data)
                {
                    print(DataCurrent != data);
                    ChangeNotifier.Notify();
                }
                DataCurrent = data;
            };
        }
    }
}
