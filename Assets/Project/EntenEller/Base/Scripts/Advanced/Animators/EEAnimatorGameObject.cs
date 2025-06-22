namespace Project.EntenEller.Base.Scripts.Advanced.Animators
{
    public class EEAnimatorGameObject : EEAnimatorBase
    {
        protected override void EEUpdate()
        {
            base.EEUpdate();
            PlayAnimation();
        }
    }
}