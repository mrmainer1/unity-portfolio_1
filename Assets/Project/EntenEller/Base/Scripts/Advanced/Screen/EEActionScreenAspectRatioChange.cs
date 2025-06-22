using Project.EntenEller.Base.Scripts.Advanced.Actions;
using Project.EntenEller.Base.Scripts.Advanced.Dictionaries;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Screen
{
    [ExecutionOrder(9999)]
    public class EEActionScreenAspectRatioChange : EEBehaviour
    {
        [SerializeField] private EEDictionary<float, EEAction> aspectRatioActions;
        private EEScreenManager screenManager;
        private int indexCurrent = -1;

        protected override void EEAwake()
        {
            base.EEAwake();
            screenManager = EESingleton.Get<EEScreenManager>();
            screenManager.ResolutionChangeNotifier.Event += OnResolutionChange;
            OnResolutionChange();
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            screenManager.ResolutionChangeNotifier.Event -= OnResolutionChange;
        }

        private void OnResolutionChange()
        {
            var aspectRatio = screenManager.Resolution.x / screenManager.Resolution.y;
            int i;
            for (i = 0; i < aspectRatioActions.Dictionary.Count; i++)
            {
                var kv = aspectRatioActions.GetList()[i];
                if (aspectRatio < kv.Key) break;
            }
            i--;
            if (indexCurrent == i) return;
            indexCurrent = i;
            aspectRatioActions.GetList()[indexCurrent].Value.Call();
        }
    }
}
