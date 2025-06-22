using Project.EntenEller.Base.Scripts.Advanced.Behaviours;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Timers;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.UI.Menu
{
    public abstract class EEMenuNotifierBehaviour : EEBehaviourSelectableLoop
    {
        [SerializeField] protected EENotifier MenuStartShowNotifier = null;
        [SerializeField] protected EENotifier MenuStartHideNotifier = null;
        [SerializeField] protected EENotifier MenuFinishShowNotifier = null;
        [SerializeField] protected EENotifier MenuFinishHideNotifier = null;
        private EEMenuView menuView;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            menuView = GetParent<EEMenuView>();
        }

        protected override void EEEnable()
        {
            base.EEEnable();
            if (GetParent<EEMenu>().IsActive)
            {
                EETime.StartTimer(new EETime.EETimerData
                {
                    Action = () =>
                    {
                        StartShow();
                        FinishShow();
                    },
                    FinalTime = 0.01f,
                    IsUnscaled = true
                });
            }
            menuView.StartShowNotifier.Event += StartShow;
            menuView.StartHideNotifier.Event += StartHide;
            menuView.FinishShowNotifier.Event += FinishShow;
            menuView.FinishHideNotifier.Event += FinishHide;
            if (!GetParent<EEMenu>().IsActive) return;
            MenuStartShowNotifier.Notify();
            MenuFinishShowNotifier.Notify();
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            menuView.StartShowNotifier.Event -= StartShow;
            menuView.StartHideNotifier.Event -= StartHide;
            menuView.FinishShowNotifier.Event -= FinishShow;
            menuView.FinishHideNotifier.Event -= FinishHide;
        }
        
        private void StartShow()
        {
            if (this == null) return;
            if (enabled) MenuStartShowNotifier.Notify();
        }
        
        private void StartHide()
        {
            if (enabled) MenuStartHideNotifier.Notify();
        }
        
        private void FinishShow()
        {
            if (enabled) MenuFinishShowNotifier.Notify();
        }

        private void FinishHide()
        {
            if (enabled) MenuFinishHideNotifier.Notify();
        }
    }
}
