using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.Variables.Approach;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Cameras
{
    public class EECamera : EEBehaviourLate
    {
        [SerializeField] private EEFloatApproach orthographicSize;
        [ReadOnly] public Vector2 Size = Vector2.zero;
        [ReadOnly] public Camera SelfCamera;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            SelfCamera = GetComponent<Camera>();
            var size = SelfCamera.orthographicSize;
            orthographicSize.SetCurrent(size);
            orthographicSize.SetTarget(size);
        }

        public void SetOrthographicSize(float size)
        {
            orthographicSize.SetTarget(size);
        }

        protected override void EELateUpdate()
        {
            base.EELateUpdate();
            orthographicSize.Proceed();
            SelfCamera.orthographicSize = orthographicSize.Current;
            Size.y = SelfCamera.orthographicSize;
            Size.x = Size.y * SelfCamera.aspect;
        }
    }
}
