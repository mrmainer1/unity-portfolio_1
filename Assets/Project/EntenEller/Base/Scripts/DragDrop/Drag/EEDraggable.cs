using System;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.Inputs.Pointers;
using Project.EntenEller.Base.Scripts.Advanced.Triggers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.DragDrop.Drag
{
    public class EEDraggable : EEBehaviourUpdate
    {
        [ReadOnly] public bool IsDragging;
        [SerializeField] public bool LockedX = false;
        [SerializeField] public bool LockedY = false;
        [SerializeField] public bool IsStickWithoutPointerUp = false;
        
        public event Action<EEDraggable> StartDragEvent, StopDragEvent; 
        public static event Action StartDragAnyEvent, StopDragAnyEvent; 
        
        protected override void EEEnable()
        {
            base.EEEnable();
            GetSelf<EEDraggablePullListener>().DragStartNotifier.Event += StartDrag;
            GetSelf<EEDraggablePullListener>().DragStopNotifier.Event += StopDrag;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            GetSelf<EEDraggablePullListener>().DragStartNotifier.Event -= StartDrag;
            GetSelf<EEDraggablePullListener>().DragStopNotifier.Event -= StopDrag;
        }

        protected override void EEUpdate()
        {
            base.EEUpdate();
            if (!IsStickWithoutPointerUp) return;
            if (!IsDragging) return;
            if (GetNeighbor<EETrigger>().RightCollisions.Count == 0) return;
            StopDrag();
        }

        private void StartDrag()
        {
            IsDragging = true;
            StartDragEvent.Call(this);
            StartDragAnyEvent.Call();
        }
        
        private void StopDrag()
        {
            IsDragging = false;
            StopDragEvent.Call(this);
            StopDragAnyEvent.Call();
        }
    }
}
