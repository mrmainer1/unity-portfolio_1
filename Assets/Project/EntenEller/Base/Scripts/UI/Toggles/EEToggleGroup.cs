using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Project.EntenEller.Base.Scripts.UI.Toggles
{
    public class EEToggleGroup : EEBehaviour
    {
        [SerializeField] private bool isAllowToSwitchOff;
        
        [ReadOnly] public EEToggle[] AllToggles;
        [ReadOnly] public EEToggle ActiveToggle;
        [ReadOnly] public bool IsAnyOn;
        
        public EENotifier FirstOnNotifier, AllOffNotifier;

        protected override void EEAwake()
        {
            base.EEAwake();
            SearchAndSubscribe();
        }
        
        public void SearchAndSubscribe()
        {
            AllToggles.ForEach(a => a.OffEvent -= OnValueOff);
            AllToggles.ForEach(a => a.OnEvent -= OnValueOn);
            AllToggles = GetComponentsInChildren<EEToggle>(true);
            AllToggles.ForEach(a => a.OffEvent += OnValueOff);
            AllToggles.ForEach(a => a.OnEvent += OnValueOn);
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            AllToggles.ForEach(a => a.OffEvent -= OnValueOff);
            AllToggles.ForEach(a => a.OnEvent -= OnValueOn);
        }

        private void OnValueOff(EEToggle toggle)
        {
            IsAnyOn = AllToggles.Any(a => a.IsOn);
            if (IsAnyOn) return;
            AllOffNotifier.Notify();
        }
        
        private void OnValueOn(EEToggle toggle)
        {
            if (ActiveToggle) ActiveToggle.GetSelf<Toggle>().enabled = true;
            
            ActiveToggle = toggle;
            if (IsAnyOn == false)
            {
                IsAnyOn = true;
                FirstOnNotifier.Notify();
            }

            foreach (var t in AllToggles)
            {
                if (t == ActiveToggle) continue;
                if (!t.IsOn) continue;
                t.SetOff();
            }

            if (!isAllowToSwitchOff)
            {
                ActiveToggle.GetSelf<Toggle>().enabled = false;
            }
        }
    }
}
