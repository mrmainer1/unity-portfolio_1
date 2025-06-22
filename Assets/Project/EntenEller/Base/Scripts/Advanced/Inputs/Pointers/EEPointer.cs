using System;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Inputs.Pointers
{
    public abstract class EEPointer : EEBehaviour
    {
        [SerializeField] protected int MouseButton = 0;
        [ReadOnly] public bool IsFocused;
        [ReadOnly] public bool IsDown;
        public EENotifier DownNotifier, UpNotifier, ClickNotifier, DragNotifier, EnterNotifier, ExitNotifier, DownHoldNotifier;
        public event Action<Vector2> DragEvent;
        
        protected override void EEEnable()
        {
            base.EEEnable();
            var mainPointer = EESingleton.Get<EEPointerManager>().PointersData[MouseButton];
            mainPointer.RawUpEvent += Up;
            mainPointer.RawDownEvent += Down;
            mainPointer.RawDownHoldEvent += DownHold;
            mainPointer.RawClickEvent += Click;
            mainPointer.RawDragEvent += Drag;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            var mainPointer = EESingleton.Get<EEPointerManager>().PointersData[MouseButton];
            mainPointer.RawUpEvent -= Up;
            mainPointer.RawDownEvent -= Down;
            mainPointer.RawDownHoldEvent -= DownHold;
            mainPointer.RawClickEvent -= Click;
            mainPointer.RawDragEvent -= Drag;
            
            if (!IsFocused) return;
            IsFocused = false;
            ExitNotifier.Notify();
        }

        private void Up()
        {
            if (!IsDown) return;
            IsDown = false;
            UpNotifier.Notify();
        }

        private void Down()
        {
            if (!IsFocused) return;
            IsDown = true;
            DownNotifier.Notify();
        }

        private void DownHold()
        {
            if (!IsFocused) return;
            DownHoldNotifier.Notify();
        }

        private void Click()
        {
            if (IsFocused) ClickNotifier.Notify();
        }

        private void Drag(Vector2 delta)
        {
            if (!IsDown) return;
            DragEvent.Call(delta);
            DragNotifier.Notify();
        }

        protected void Enter()
        {
            IsFocused = true;
            EnterNotifier.Notify();
        }

        protected void Exit()
        {
            IsFocused = false;
            ExitNotifier.Notify();
        }
    }
}
