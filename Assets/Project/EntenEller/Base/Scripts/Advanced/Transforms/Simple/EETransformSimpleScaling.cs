using Project.EntenEller.Base.Scripts.Advanced.Behaviours;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class EETransformSimpleScaling : EEBehaviourUpdate
    {
        public Vector3 Scale;
        public float Speed;
        
        protected override void EEUpdate()
        {
            base.EEUpdate();
            GetSelf<Transform>().localScale = GetSelf<Transform>().localScale + Scale * (Speed * Time.deltaTime);
        }

        public void ChangeSpeed(float speedNew)
        {
            Speed = speedNew;
        }
        
        public void ChangeScale(Vector3 scaleNew)
        {
            Scale = scaleNew;
        }
    }
}
