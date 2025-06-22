using Project.EntenEller.Base.Scripts.Advanced.Variables.Approach;

namespace Project.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public abstract class EETransformApproachPosition : EETransformApproach
    {
        public EEVector3Approach Position;
        
        protected override void EESelectableLoop()
        {
            base.EESelectableLoop();
            if (IsGlobal)
            {
                Position.Current = CachedTransform.position;
                if (Target) Position.Target = Target.position;
                Position.Proceed();
                GlobalMove();
            }
            else
            {
                Position.Current = CachedTransform.localPosition;
                if (Target) Position.Target = Target.localPosition;
                Position.Proceed();
                LocalMove();
            }
        }

        public void On()
        {
            Position.On();
        }

        public void Off()
        {
            Position.Off();
        }

        protected abstract void GlobalMove();
        protected abstract void LocalMove();
    }
}
