using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class EETransformSimpleRotation : EEBehaviourUpdate
    {
        private Vector3 current;
        public Vector3 Rotation;
        public float Speed;
        private Transform myTransform;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            myTransform = transform;
        }

        protected override void EEUpdate()
        {
            base.EEUpdate();
            current += Rotation * (Speed * Time.deltaTime);
            myTransform.localEulerAngles = current;
        }
        
        public void ChangeSpeed(float speedNew)
        {
            Speed = speedNew;
        }
        
        public void ChangeRotation(Vector3 rotationNew)
        {
            Rotation = rotationNew;
        }
    }
}
