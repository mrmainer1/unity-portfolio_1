using UnityEngine;
using Project.EntenEller.Base.Scripts.Advanced.Platform;
using UnityEngine.Advertisements;
#if UNITY_ADS

#endif

namespace Project.EntenEller.Base.Scripts.Advanced.Ad.UnityAd
{
    public class EEUnityAdManager : EEPlatformKeyBehaviour 
    {
#if UNITY_ADS
        public EENotifier InitNotifier;

        protected override void EEAwake()
        {
            base.EEAwake();
            Init();
        }

        private void Init()
        {
            Advertisement.Initialize(GetCurrentPlatformKey(), true, this);
        }
#endif
    }
}
