#if UNITY_ADS
using Project.EntenEller.Base.Scripts.Advanced.Platform;
using Project.EntenEller.Base.Scripts.Advanced.Scenes;
using Project.EntenEller.Base.Scripts.Advanced.Screen;
using Project.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Project.EntenEller.Base.Scripts.Advanced.Ad.UnityAd
{
    public class EEUnityBanner : EEPlatformKeyBehaviour
    {
        [SerializeField] private BannerPosition Position;
        public EENotifier OnNotifier, OffNotifier;
        public bool IsActive;

        protected override void EEAwake()
        {
            base.EEAwake();
            EESingleton.Get<EEScreenManager>().ResolutionChangedNotifier.Event += Restart;
            EESceneData.ScenesFinishedChangesEvent += OnScenesFinishedChanges;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            EESingleton.Get<EEScreenManager>().ResolutionChangedNotifier.Event -= Restart;
            EESceneData.ScenesFinishedChangesEvent -= OnScenesFinishedChanges;
        }

        private void OnScenesFinishedChanges()
        {
            if (IsActive) OnNotifier.Call();
            else OffNotifier.Call();
        }

        public void Restart()
        {
            Off();
            On();
        }

        public void On()
        {
            IsActive = true;
            Advertisement.Banner.SetPosition(Position);
            
            BannerLoadOptions options = new BannerLoadOptions
            {
                loadCallback = OnBannerLoaded,
                errorCallback = OnBannerError
            };
            
            Advertisement.Banner.Load(GetCurrentPlatformKey(), options);
            OnNotifier.Call();
        }
        
        private void OnBannerLoaded()
        {
            ShowBannerAd();    
        }

        private void OnBannerError(string message)
        {
            
        }
        
        private void ShowBannerAd()
        {
            Advertisement.Banner.Show();
        }

        public void Off()
        {
            IsActive = false;
            Advertisement.Banner.Hide();
            OffNotifier.Call();
        }
    }
}
#endif