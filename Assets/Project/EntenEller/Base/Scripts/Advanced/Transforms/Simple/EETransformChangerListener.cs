using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.OdinInspector;

namespace Project.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class EETransformChangerListener : EEBehaviour
    {
        public EENotifier StopNotifier, StartNotifier;
        private bool isMoving, isRotating, isScaling;
        [ReadOnly] public bool IsChanging;
        
        protected override void EEEnable()
        {
            base.EEEnable();
            
            GetSelf<EETransformApproachPosition>().Position.OnNotifier.Event += OnPosition;
            GetSelf<EETransformApproachRotation>().Rotation.OnNotifier.Event += OnRotation;
            GetSelf<EETransformApproachScale>().Scale.OnNotifier.Event += OnScale;
            
            GetSelf<EETransformApproachPosition>().Position.OffNotifier.Event += OffPosition;
            GetSelf<EETransformApproachRotation>().Rotation.OffNotifier.Event += OffRotation;
            GetSelf<EETransformApproachScale>().Scale.OffNotifier.Event += OffScale;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            
            GetSelf<EETransformApproachPosition>().Position.OnNotifier.Event -= OnPosition;
            GetSelf<EETransformApproachRotation>().Rotation.OnNotifier.Event -= OnRotation;
            GetSelf<EETransformApproachScale>().Scale.OnNotifier.Event -= OnScale;
            
            GetSelf<EETransformApproachPosition>().Position.OffNotifier.Event -= OffPosition;
            GetSelf<EETransformApproachRotation>().Rotation.OffNotifier.Event -= OffRotation;
            GetSelf<EETransformApproachScale>().Scale.OffNotifier.Event -= OffScale;
        }

        private void OnPosition()
        {
            isMoving = true;
            TransformChange();
        }
        
        private void OnRotation()
        {
            isRotating = true;
            TransformChange();
        }

        private void OnScale()
        {
            isScaling = true;
            TransformChange();
        }
        
        private void OffPosition()
        {
            isMoving = false;
            TransformChange();
        }

        private void OffRotation()
        {
            isRotating = false;
            TransformChange();
        }
        
        private void OffScale()
        {
            isScaling = false;
            TransformChange();
        }

        private void TransformChange()
        {
            var isChanging = isMoving || isRotating || isScaling;
            if (IsChanging == isChanging) return;
            IsChanging = isChanging;
            if (IsChanging) StartNotifier.Notify();
            else StopNotifier.Notify();
        }
    }
}
