using System;
using BestHTTP;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Project.EntenEller.Base.Scripts.Network.HTTP
{
    public class EEHTTPByteReceiver : EEBehaviour
    {
        public Action<byte[]> SuccessEvent;
        
        protected override void EEEnable()
        {
            base.EEEnable();
            GetSelf<EEHTTP>().SuccessEvent += Success;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            GetSelf<EEHTTP>().SuccessEvent -= Success;
        }
        
        protected virtual void Success(HTTPResponse result)
        {
            SuccessEvent.Call(result.Data);
        }
    }
}
