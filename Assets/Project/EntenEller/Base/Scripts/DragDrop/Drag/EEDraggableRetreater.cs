using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Advanced.Transforms.Simple;
using Project.EntenEller.Base.Scripts.Advanced.Variables.Approach;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.DragDrop.Drag
{
    public class EEDraggableRetreater : EEBehaviour
    {
        [SerializeField] private bool isSetPosition, isSetRotation, isSetScale;
        [SerializeField] private Transform retreatTransform;
        [ShowIf("isSetPosition")] public Vector3 Position;
        [ShowIf("isSetRotation")] public Quaternion Rotation;
        [ShowIf("isSetScale")] public Vector3 Scale;
        [SerializeField] private Approach positionApproach, rotationApproach, scaleApproach;
        [ReadOnly] public bool IsMovingBack;
        public EENotifier StartedGoBackNotifier, FinishedGoBackNotifier;
        
        protected override void EEEnable()
        {
            base.EEEnable();
            GetSelf<EEDraggableDropZoneFinder>().NoDraggablesToStickNotifier.Event += Back;
            GetSelf<EEDraggablePullListener>().DragStartNotifier.Event += DragStart;
        }

        private void DragStart()
        {
            OnReachBack();
            GetSelf<EEDraggablePullListener>().DragStartNotifier.Event -= DragStart;
            CachePRS();
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            GetSelf<EEDraggableDropZoneFinder>().NoDraggablesToStickNotifier.Event -= Back;
        }

        public void Back()
        {
            IsMovingBack = true;
            GetParent<EETransformApproachPRS>().SetApproachStyles(positionApproach, rotationApproach, scaleApproach);
            if (retreatTransform)
            {
                GetParent<EETransformApproachPRS>().SetTarget(retreatTransform);
            }
            else
            {
                GetParent<EETransformApproachPRS>().SetTarget(null);
                GetParent<EETransformApproachPRS>().SetTarget(Position, Rotation, Scale);
            }
            StartedGoBackNotifier.Notify();
            GetParent<EETransformApproachPosition>().Position.ReachNotifier.Event += OnReachBack;
        }

        private void OnReachBack()
        {
            if (!IsMovingBack) return;
            FinishedGoBackNotifier.Notify();
            GetParent<EETransformApproachPosition>().Position.ReachNotifier.Event -= OnReachBack;
            IsMovingBack = false;
        }

        public void CachePRS()
        {
            if (!isSetPosition) Position = GetParent<Transform>().position;
            if (!isSetRotation) Rotation = GetParent<Transform>().rotation;
            if (!isSetScale) Scale = GetParent<Transform>().localScale;
        }

        public void SetRetreatTarget(Transform ts)
        {
            retreatTransform = ts;
        }
    }
}
