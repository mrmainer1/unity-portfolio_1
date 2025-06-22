using System.Collections.Generic;
using BestHTTP;
using Project.EntenEller.Base.Scripts.Advanced.Dictionaries;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Network.HTTP
{
    [ExecuteBefore(typeof(EEHTTP))]
    public class EEHTTPStringSender : EEBehaviour
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
            if (request.MethodType == HTTPMethods.Post)
            {
                foreach (var kv in Data.Dictionary)
                {
                    request.AddField(kv.Key, kv.Value);
                }
            }
            if (request.MethodType == HTTPMethods.Get)
            {
                GetSelf<EEHTTP>().SetParameters(Data.Dictionary);
            }
        }

        public void UpdateData(string key, string value)
        {
            Data.SetValue(key, value);
        }
    }
}
