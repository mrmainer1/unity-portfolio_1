using BestHTTP;
using Project.EntenEller.Base.Scripts.Advanced.Dictionaries;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Project.EntenEller.Base.Scripts.Network.HTTP
{
    public class EEHTTPHeaders : EEBehaviour
    {
        public EEDictionary<string, string> Data;

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
            foreach (var kv in Data.Dictionary)
            {
                request.AddHeader(kv.Key, kv.Value);
            }
        }
    }
}
