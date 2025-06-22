using System;
using Project.EntenEller.Base.Scripts.Advanced.Animators;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Save;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using Time = UnityEngine.Time;

namespace Project.EntenEller.Base.Scripts.UI.Toggles
{
    public class EEToggle : EEBehaviour
    {
        public event Action<bool> ValueRawChangedEvent;
        public event Action<bool> ValueChangedEvent;
        public event Action<EEToggle> OnEvent;
        public event Action<EEToggle> OffEvent;
        public EENotifier OnNotifier;
        public EENotifier OffNotifier;
        private int banFrame = -1;
        
        private bool previousIsOn;
        [ReadOnly] public bool IsOn;

        protected override void EEAwake()
        {
            base.EEAwake();
            IsOn = GetSelf<Toggle>().isOn;
            previousIsOn = IsOn;
            SetVisual(IsOn);
            GetSelf<Toggle>().onValueChanged.AddListener(ValueChangedToggle);
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            GetSelf<Toggle>().onValueChanged.RemoveListener(ValueChangedToggle);
        }

        public void SetOn()
        {
            SetState(true);
        }
        
        public void SetOff()
        {
            SetState(false);
        }
        
        public void SetState(bool isOn)
        {
            IsOn = isOn;
            GetSelf<Toggle>().isOn = IsOn;
        }

        public void ForceState(bool isOn)
        {
            GetSelf<Toggle>().isOn = IsOn;
            ChangeValue(isOn);
        }

        public void NoNotifyState(bool isOn)
        {
            banFrame = Time.frameCount;
            GetSelf<Toggle>().isOn = isOn;
            SetVisual(isOn);
        }

        public void Enable()
        {
            GetSelf<Toggle>().interactable = true;
        }
        
        public void Disable()
        {
            GetSelf<Toggle>().interactable = false;
        }
        
        private void ValueChangedToggle(bool isOn)
        {
            IsOn = isOn;
            ValueChanged(isOn);
        }

        private void ValueChanged(bool isOn, bool isAnimated = true)
        {
            ValueRawChangedEvent.Call(isOn);
            if (previousIsOn == isOn) return;
            ChangeValue(isOn, isAnimated);
        }

        private void ChangeValue(bool isOn, bool isAnimated = true)
        {
            previousIsOn = isOn;
            if (banFrame == Time.frameCount) return;
            if (isOn)
            {
                OnEvent.Call(this);
                OnNotifier.Notify();
                SetVisual(true, isAnimated);
            }
            else
            {
                OffEvent.Call(this);
                OffNotifier.Notify();
                SetVisual(false, isAnimated);
            }
            ValueChangedEvent.Call(isOn);
        }

        private void SetVisual(bool isOn, bool isAnimated = true)
        {
            if (!isAnimated) return;
            if (isOn) GetChild<EEAnimatorUI>().PlayForward("Main");
            else GetChild<EEAnimatorUI>().PlayBackward("Main");
        }
    }
}