using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.Scripts.Builder.Building;
using UnityEngine;

namespace Project.Scripts.Builder
{
    public class BuildingRotatorView : EEBehaviour
    {
        [SerializeField] private BuildingRotator buildingRotator;
        private Vector3 startingPosition;

        protected override void EEAwake()
        {
            base.EEAwake();
            startingPosition = transform.localPosition;
            buildingRotator.RotateNotifier.Event += OnRotate;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            buildingRotator.RotateNotifier.Event -= OnRotate;
        }

        private void OnRotate()
        {
            SetQuadrant(buildingRotator.Quadrant);
        }

        public void SetQuadrant(int quadrant)
        {
            transform.localEulerAngles = new Vector3(0, quadrant * 90, 0);
            var pos = transform.localPosition;
            if (quadrant % 2 == 0)
            {
                pos = startingPosition;
            }
            else
            {
                pos.x = startingPosition.z;
                pos.z = startingPosition.x;
            }
            transform.localPosition = pos;
        }
    }
}
