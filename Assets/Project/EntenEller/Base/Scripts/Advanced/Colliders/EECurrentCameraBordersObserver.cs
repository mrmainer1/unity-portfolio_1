using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.Components;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Colliders
{
    [RequireComponent(typeof(BoxCollider))]
    public class EECurrentCameraBordersObserver : EEBehaviourUpdate
    {
        public bool IsOnScreen { get; private set; }
        public float DistanceToCameraCenter { get; private set; }

        protected override void EEUpdate()
        {
            base.EEUpdate();
            if (Camera.current.IsNull()) return;
            CheckFieldView();
            CheckDistance();
        }

        private void CheckFieldView()
        {
            var planes = GeometryUtility.CalculateFrustumPlanes(Camera.current);
            IsOnScreen = GeometryUtility.TestPlanesAABB(planes, GetSelf<BoxCollider>().bounds);
        }

        private void CheckDistance()
        {
            DistanceToCameraCenter = (GetSelf<Transform>().position - Camera.current.transform.position).magnitude;
        }
    }
}