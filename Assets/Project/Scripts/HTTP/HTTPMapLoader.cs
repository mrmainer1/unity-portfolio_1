using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Advanced.Serializations;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Network.HTTP;
using Project.EntenEller.Base.Scripts.VisualHacks;
using UnityEngine;

namespace Project.Scripts.HTTP
{
    public class HTTPMapLoader : EEBehaviour
    {
        [SerializeField] private EEHTTP http;
        [SerializeField] private EEHTTPStringReceiver httpReceiver;
        [SerializeField] private EEWaitForStableFPS waitForStableFPS;
        public HTTPDataMap.Data MapInfo;
        public EENotifier SuccessLoadNotifier, UpdateCarIDNotifier;

        private bool isFirstLoad;

        protected override void EEAwake()
        {
            base.EEAwake();
            waitForStableFPS.StableNotifier.Event += () => http.Call();
            httpReceiver.SuccessEvent += OnSuccess;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            httpReceiver.SuccessEvent -= OnSuccess;
        }

        private void OnSuccess(string json)
        {
            var data = EEJSON.Deserialize<HTTPDataMap>(json);
            if (data.status != "OK") return;
            MapInfo = data.data;

            if (isFirstLoad)
            {
                UpdateCarIDNotifier.Notify();
            }
            else
            {
                SuccessLoadNotifier.Notify();    
                isFirstLoad = true;
            }
        }
    }
}
