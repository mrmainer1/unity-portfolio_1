using MEC;

namespace Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop
{
    public abstract class EEBehaviourUpdate : EEBehaviourLoop
    {
        private static ContainerLoopData containerLoopData;
        private static bool isInit;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            if (!isInit)
            {
                isInit = true;
                containerLoopData = new ContainerLoopData(Segment.Update);
            }
            AddData(containerLoopData, EEUpdate);
        }

        protected virtual void EEUpdate() {}
    }
}