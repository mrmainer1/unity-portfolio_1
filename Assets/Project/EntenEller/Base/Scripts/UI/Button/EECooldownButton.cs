using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Timers;

namespace Project.EntenEller.Base.Scripts.UI.Button
{
    public class EECooldownButton : EEBehaviour
    {
        public EENotifier TryPressNotifier, PressNotifier, RestoreNotifier;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            GetSelf<EEButton>().ClickEvent += OnClick;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            GetSelf<EEButton>().ClickEvent -= OnClick;
        }

        private void OnClick(EEButton obj)
        {
            if (!enabled) return;
            TryPressNotifier.Notify();
        }

        public void Call()
        {
            if (!enabled) return;
            PressNotifier.Notify();
        }

        public void Restore()
        {
            RestoreNotifier.Notify();
        }
    }
}
