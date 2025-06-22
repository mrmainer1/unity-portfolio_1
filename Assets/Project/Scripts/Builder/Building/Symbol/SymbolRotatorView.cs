using Project.EntenEller.Base.Scripts.Advanced.Tags;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.Scripts.Builder.Building.Symbol
{
    public class SymbolRotatorView : EEBehaviour
    {
        [SerializeField] private BuildingRotator buildingRotator;
        protected override void EEAwake()
        {
            base.EEAwake();
            buildingRotator.RotateNotifier.Event += OnRotate;
        }
        protected override void EEDestroy()
        {
            base.EEDestroy();
            buildingRotator.RotateNotifier.Event -= OnRotate;
        }
    
        private void OnRotate()
        {
            var menu = EETagUtils.FindEETagInChildren(this,"node-input").transform;
            menu.localEulerAngles = new Vector3(0, buildingRotator.Quadrant switch { 0 => 0, 1 => -90, 2 => 180, 3 => 90, _ => 0 }, 0);
        }
    }
}
