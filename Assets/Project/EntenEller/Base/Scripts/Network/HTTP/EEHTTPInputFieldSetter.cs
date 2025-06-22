using BestHTTP;
using Project.EntenEller.Base.Scripts.Advanced.Tags;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.UI.InputField;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Network.HTTP
{
    [ExecuteBefore(typeof(EEHTTPStringSender))]
    public class EEHTTPInputFieldSetter : EEBehaviour
    {
        [SerializeField] private string key;
        [SerializeField] private EEGameObjectFinder target;
        
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

        private void OnNewRequest(HTTPRequest httpRequest)
        {
            GetSelf<EEHTTPStringSender>().Data.SetValue(key, target.GetSingle().GetSelf<EEInputField>().Data);
        }
    }
}
