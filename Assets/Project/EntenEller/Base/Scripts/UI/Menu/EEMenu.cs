using System;
using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Animators;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.UI.Menu
{
    [RequireComponent(typeof(EEMenuView))]
    [RequireComponent(typeof(EEAnimatorUI))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CanvasGroup))]

    public class EEMenu : EEBehaviour
    {
        public event Action ShowEvent;
        public event Action HideEvent;
        public bool IsActive;
        public bool IgnoreMenuSystem;
        private bool stateCurrent;
        public static List<EEMenu> Menus = new List<EEMenu>();

        protected override void EEAwake()
        {
            stateCurrent = IsActive;
            Menus.Add(this);
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            Menus.Remove(this);
        }

        public void SetState(bool isOn)
        {
            if (stateCurrent == isOn) return;
            stateCurrent = IsActive = isOn;
            (IsActive ? ShowEvent : HideEvent).Call();
        }
    }
}