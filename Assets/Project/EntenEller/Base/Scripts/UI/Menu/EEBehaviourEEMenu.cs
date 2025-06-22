using System.Collections.Generic;
using MEC;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.UI.Menu
{
    [ExecutionOrder(2000)]
    public abstract class EEBehaviourEEMenu : EEMenuNotifierBehaviour
    {
        private CoroutineHandle coroutine;
        private bool isActive;
        
        protected override void EEEnable()
        {
            MenuStartShowNotifier.Event += MenuStartShow;
            MenuStartHideNotifier.Event += MenuStartHide;
            MenuFinishShowNotifier.Event += MenuFinishShow;
            MenuFinishHideNotifier.Event += MenuFinishHide;
            base.EEEnable();
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            MenuStartShowNotifier.Event -= MenuStartShow;
            MenuStartHideNotifier.Event -= MenuStartHide;
            MenuFinishShowNotifier.Event -= MenuFinishShow;
            MenuFinishHideNotifier.Event -= MenuFinishHide;
        }

        protected virtual void MenuStartShow()
        {
            isActive = true;
        }
        
        protected virtual void MenuStartHide()
        {
            isActive = false;
        }

        protected override void EESelectableLoop()
        {
            base.EESelectableLoop();
            if (isActive) MenuLoop();
        }
        
        protected virtual void MenuFinishShow() {}
        
        protected virtual void MenuFinishHide() {}
        
        protected virtual void MenuLoop() {}
    }
}
