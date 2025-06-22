using Project.EntenEller.Base.Scripts.Advanced.Actions;
using Project.EntenEller.Base.Scripts.Advanced.Dictionaries;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Network.HTTP
{
    public class EEHTTPUnityEventOnStringReceive : EEBehaviour
    {
        [SerializeField] private EEDictionary<string, EEAction> dictionary;

        protected override void EEAwake()
        {
            base.EEAwake();
            GetSelf<EEHTTPStringReceiver>().SuccessEvent += OnSuccess;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            GetSelf<EEHTTPStringReceiver>().SuccessEvent -= OnSuccess;
        }

        private void OnSuccess(string text)
        {
            if (dictionary.Dictionary.ContainsKey(text))
            {
                dictionary.Dictionary[text].Call();
            }
        }
    }
}
