using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class EETransformSimpleMovement : EEBehaviourUpdate
    {
        public bool IsForward;
        [HideIf("IsForward")] public Vector3 Direction;
        public EEFloatRandomOrDefined MinSpeed, Speed, MaxSpeed;
        public EEFloatRandomOrDefined Acceleration;
        private Transform ts;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            ts = transform;
        }

        protected override void EEUpdate()
        {
            base.EEUpdate();
            
            Speed.SetValue(Speed.Value + Acceleration.Value * Time.deltaTime);
            
            if (Speed.Value > MaxSpeed.Value) Speed.SetValue(MaxSpeed.Value);
            else if (Speed.Value < MinSpeed.Value) Speed.SetValue(MinSpeed.Value);
            
            if (IsForward) ts.localPosition += ts.forward * (Speed.Value * Time.deltaTime);
            else ts.localPosition += Direction * (Speed.Value * Time.deltaTime);
        }

        public void ChangeSpeed(float speed)
        {
            Speed.SetValue(speed);
        }
        
        public void ChangeMaxSpeed(float maxSpeed)
        {
            MaxSpeed.SetValue(maxSpeed);
        }
        
        public void ChangeAcceleration(float acceleration)
        {
            Acceleration.SetValue(acceleration);
        }
        
        public void ChangeDirection(Vector3 directionNew)
        {
            Direction = directionNew;
        }
    }
}
