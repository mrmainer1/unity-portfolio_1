using System;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Project.EntenEller.Base.Scripts.UI.Button
{
    public class EEButton : EEBehaviour
    {
        public event Action<EEButton> ClickEvent;
        public EENotifier ClickNotifier, ActiveNotifier, InactiveNotifier;
        public bool State = true;
        private UnityEngine.UI.Button button;

        protected override void EEAwake()
        {
            base.EEAwake();
            button = GetComponent<UnityEngine.UI.Button>();
            State = !State;
            SetState(!State);
        }

        protected override void EEEnable()
        {
            base.EEEnable();
            button.onClick.AddListener(Click);
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            button.onClick.RemoveListener(Click);
        }

        public void SetState(bool state)
        {
            if (State == state) return;
            State = state;
            button.interactable = state;
            if (State) ActiveNotifier.Notify();
            else InactiveNotifier.Notify();
        }

        public void Click()
        {
            if (!enabled) return;
            ClickNotifier.Notify();
            ClickEvent.Call(this);
        }
    }
}
