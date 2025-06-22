using System;
using Project.EntenEller.Base.Scripts.Advanced.Animators;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.UI.Menu
{
    [ExecuteBefore(typeof(EEMenu))]
    public class EEMenuView : EEBehaviour
    {
        private enum State
        {
            Show,
            Hide,
            Unknown
        }

        [SerializeField] [ReadOnly] private State state = State.Unknown;
        
        public EENotifier StartShowNotifier;
        public EENotifier StartHideNotifier;
        public EENotifier FinishShowNotifier;
        public EENotifier FinishHideNotifier;

        private EEAnimatorUI animatorUI;
        private EEMenu menu;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            animatorUI = GetSelf<EEAnimatorUI>();
            menu = GetSelf<EEMenu>();

            animatorUI.FinishNotifier.Event += Finished;
            menu.ShowEvent += StartShow;
            menu.HideEvent += StartHide;

            if (menu.IsActive)
            {
                animatorUI.TimeApproach.SetCurrent(1);
                animatorUI.TimeApproach.SetTarget(1);
            }
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            animatorUI.FinishNotifier.Event -= Finished;
            menu.ShowEvent -= StartShow;
            menu.HideEvent -= StartHide;
        }

        private void Finished()
        {
            switch (state)
            {
                case State.Show:
                    FinishShowNotifier.Notify();
                    break;
                case State.Hide:
                    FinishHideNotifier.Notify();
                    break;
                case State.Unknown:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void StartShow()
        {
            StartShowNotifier.Notify();
            state = State.Show;
            PlayEffect(true);
        }
        
        private void StartHide()
        {
            StartHideNotifier.Notify();
            state = State.Hide;
            PlayEffect(false);
        }
        
        private void PlayEffect(bool isVisible)
        {
            if (isVisible)
            {
                animatorUI.PlayForward("Main");
            }
            else 
            {
                animatorUI.PlayBackward("Main");
            }
        }
    }
}