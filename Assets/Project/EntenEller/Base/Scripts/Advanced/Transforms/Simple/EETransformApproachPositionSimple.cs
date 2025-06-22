namespace Project.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class EETransformApproachPositionSimple : EETransformApproachPosition
    {
        protected override void GlobalMove()
        {
            CachedTransform.position = Position.Current;
        }

        protected override void LocalMove()
        {
            CachedTransform.localPosition = Position.Current;
        }
    }
}