using System;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class EETransformRotationAccordingToMove : EEBehaviourUpdate
    {
        public Axis Axe;
        public Vector3 Rotation = new Vector3(0, 0, 0);
        
        protected override void EEUpdate()
        {
            base.EEUpdate();
            if (!GetSelf<EETransformPositionObserver>().IsMoving) return;
            var delta = GetSelf<EETransformPositionObserver>().Delta;
            var angle = Mathf.Atan2(-delta.x, -delta.z) * Mathf.Rad2Deg;
            switch (Axe)
            {
                case Axis.x:
                    Rotation.x = angle;
                    break;
                case Axis.y:
                    Rotation.y = angle;
                    break;
                case Axis.z:
                    Rotation.z = angle;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            GetSelf<EETransformApproachRotation>().Rotation.SetTarget(Quaternion.Euler(Rotation));
        }

        public enum Axis
        {
            x,y,z
        }
    }
}
