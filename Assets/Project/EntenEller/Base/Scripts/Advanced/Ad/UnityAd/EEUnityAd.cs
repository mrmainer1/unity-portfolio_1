#if UNITY_ADS
using System;
using UnityEngine.Advertisements;

namespace Project.EntenEller.Base.Scripts.Advanced.Ad.UnityAd
{
    public class EEUnityAd : EEAdNormal, IUnityAdsListener
    {
        protected override void EEAwake()
        {
            base.EEAwake();
            Advertisement.AddListener(this);
            Load();
        }

        protected override void Load()
        {
            if (StartLoad()) Advertisement.Load(GetCurrentPlatformKey());
        }

        protected override void Show()
        {
            Advertisement.Show(ID);
        }
        
        public void OnUnityAdsReady(string id)
        {
            FinishLoad(id);
        }

        public void OnUnityAdsDidError(string msg)
        {
            Error();
        }

        public void OnUnityAdsDidStart(string id) {}

        public void OnUnityAdsDidFinish(string placementId, ShowResult result)
        {
            switch (result)
            {
                case ShowResult.Skipped:
                    Skip();
                    break;
                case ShowResult.Finished:
                    Complete();
                    break;
                case ShowResult.Failed:
                    Error();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(result), result, null);
            }
        }
    }
}
#endif