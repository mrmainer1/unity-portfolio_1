using Project.EntenEller.Base.Scripts.Advanced.Tags;
using Project.EntenEller.Base.Scripts.Advanced.Variables.Collections;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using BestHTTP;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Network.HTTP
{
    [ExecuteBefore(typeof(EEHTTPStringSender))]
    public class EEHTTPCollectionInt : EEBehaviour
    {
        [SerializeField] private EEGameObjectFinder target;
        [SerializeField] private string key;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            GetSelf<EEHTTP>().NewRequestEvent += OnNewRequest;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            GetSelf<EEHTTP>().NewRequestEvent -= OnNewRequest;
        }

        private void OnNewRequest(HTTPRequest http)
        {
            GetSelf<EEHTTPStringSender>().Data.SetValue(key, target.GetSingle().GetSelf<EECollectionInt>().List[0].ToString());
        }
    }
}
