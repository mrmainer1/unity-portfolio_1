using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using Project.EntenEller.Base.Scripts.UI.InputField;
using Project.EntenEller.Base.Scripts.UI.Menu;
using UnityEngine;

namespace Project.Scripts.UINewMap
{
    public class NameMap : EEBehaviour
    {
        [SerializeField] private EEInputField inputField;
        [SerializeField] private EEMenuView menuView;
        private NameMapManager nameMapManager;

        protected override void EEAwake()
        {
            base.EEAwake();
            inputField.UpdateNotifier.Event += () => nameMapManager.SetName(inputField.Data);
            nameMapManager = EESingleton.Get<NameMapManager>();
            menuView.StartShowNotifier.Event += () => inputField.Set(nameMapManager.GetName());
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            inputField.UpdateNotifier.Event -= () => nameMapManager.SetName(inputField.Data);
            menuView.StartShowNotifier.Event -= () => inputField.Set(nameMapManager.GetName());
        }
    }
}

