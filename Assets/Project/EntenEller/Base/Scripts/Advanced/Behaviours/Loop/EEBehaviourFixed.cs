using MEC;

namespace Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop
{
    public class EEBehaviourFixed : EEBehaviourLoop
    {
        private static ContainerLoopData containerLoopData;
        private static bool isInit;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            if (!isInit)
            {
                isInit = true;
                containerLoopData = new ContainerLoopData(Segment.FixedUpdate);
            }
            AddData(containerLoopData, EEFixedUpdate);
        }

        protected virtual void EEFixedUpdate() {}
    }
}
