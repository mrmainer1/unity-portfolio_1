using Project.EntenEller.Base.Scripts.Advanced.Behaviours;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.Transforms.Simple;
using Project.EntenEller.Base.Scripts.Advanced.Variables.Approach;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.DragDrop.Drag
{
    public abstract class EEDraggableFollower : EEBehaviourUpdate
    { 
        [ReadOnly] public Vector3 Offset;
        [ReadOnly] public bool IsActive;
        [SerializeField] private Approach positionApproach, rotationApproach, scaleApproach;
        
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
            if (!IsActive) return;
            var pos = GetPosition() + Offset;
            if (GetSelf<EEDraggable>().LockedX) pos.x = GetParent<EETransformApproachPosition>().Position.Current.x;
            if (GetSelf<EEDraggable>().LockedY) pos.y = GetParent<EETransformApproachPosition>().Position.Current.y;
            GetParent<EETransformApproachPosition>().Position.SetTarget(pos);
        }

        private void StartDrag()
        {
            GetParent<EETransformApproachPRS>().SetApproachStyles(positionApproach, rotationApproach, scaleApproach);
            GetParent<EETransformApproachPRS>().SetTarget(null);
            Offset = GetParent<Transform>().position - GetPosition();
            IsActive = true;
        }
        
        private void StopDrag()
        {
            IsActive = false;
        }

        protected abstract Vector3 GetPosition();
    }
}