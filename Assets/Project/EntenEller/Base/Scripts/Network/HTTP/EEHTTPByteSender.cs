using BestHTTP;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Project.EntenEller.Base.Scripts.Network.HTTP
{
    [ExecuteBefore(typeof(EEHTTP))]
    public abstract class EEHTTPByteSender : EEBehaviour
    {
        protected override void EEEnable()
        {
            base.EEEnable();
            GetSelf<EEHTTP>().NewRequestEvent += NewRequest;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            GetSelf<EEHTTP>().NewRequestEvent -= NewRequest;
        }
        
        private void NewRequest(HTTPRequest request)
        {
            request.RawData = Collect();
        }

        protected abstract byte[] Collect();
    }
}
