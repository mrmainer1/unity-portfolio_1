using System;
using BestHTTP;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Network.HTTP
{
    [ExecuteBefore(typeof(EEHTTP))]
    public class EEHTTPStringReceiver : EEBehaviour
    {
        [SerializeField] [TextArea(1, 50)] private string stringResult;
        private string oldData;
        public event Action<string> SuccessEvent, SuccessSameDataEvent, SuccessNewDataEvent;

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
        
        private void Success(HTTPResponse result)
        {
            stringResult = result.DataAsText;
            SuccessEvent.Call(stringResult);
            if (stringResult == oldData) SuccessSameDataEvent.Call(stringResult);
            else SuccessNewDataEvent.Call(stringResult);
        }
    }
}
