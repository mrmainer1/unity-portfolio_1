using MEC;

namespace Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop
{
    public abstract class EEBehaviourLate : EEBehaviourLoop
    {
        private static ContainerLoopData containerLoopData;
        private static bool isInit;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            if (!isInit)
            {
                isInit = true;
                containerLoopData = new ContainerLoopData(Segment.LateUpdate);
            }
            AddData(containerLoopData, EELateUpdate);
        }
        
        protected virtual void EELateUpdate() {}
    }
}
