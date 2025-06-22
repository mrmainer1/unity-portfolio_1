using System;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    [ExecutionOrder(9999)]
    public abstract class EETransformPositionObserverBase : EEBehaviourFixed
    {
        public Vector3 Current;
        public Vector3 Last;
        public Vector3 Delta;
        public EENotifier StartMoveNotifier, StopMoveNotifier, ChangeNotifier;
        public Action<Vector3> ChangedEvent;
        public bool IsMoving;
        [SerializeField] private float accuracy = EEConstants.MeasurementAccuracyMax;
        
        protected override void EEFixedUpdate()
        {
            base.EEFixedUpdate();
            Delta = Current - Last;
            if (!Delta.sqrMagnitude.IsAlmostZero(accuracy))
            {
                if (!IsMoving)
                {
                    IsMoving = true;
                    StartMoveNotifier.Notify();
                }
                ChangedEvent.Call(Delta);
                ChangeNotifier.Notify();
            }
            else
            {
                if (!IsMoving) return;
                IsMoving = false;
                StopMoveNotifier.Notify();
            }
        }
    }
}
