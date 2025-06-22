using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.Scripts.Builder.Building
{
    public class BuildingRotator : EEBehaviour
    {
        [SerializeField] private BuildingRotatorInputKeyboard buildingRotatorInput;
        [SerializeField] private Building building;
        
        public int Quadrant;
        public EENotifier RotateNotifier;

        protected override void EEAwake()
        {
            base.EEAwake();
            buildingRotatorInput.RotateEvent += Rotate;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            buildingRotatorInput.RotateEvent -= Rotate;
        }

        private void Rotate()
        {
            Quadrant++;
            Quadrant %= 4;
            SetQuadrant(Quadrant);
        }

        public void SetQuadrant(int quadrant)
        {
            Quadrant = quadrant;
            if (quadrant % 2 == 0) building.SetSize(building.StartingWidth, building.StartingHeight);
            else building.SetSize(building.StartingHeight, building.StartingWidth);
            RotateNotifier.Notify();
        }

        public int GetQuadrant() => Quadrant;
    }
}
