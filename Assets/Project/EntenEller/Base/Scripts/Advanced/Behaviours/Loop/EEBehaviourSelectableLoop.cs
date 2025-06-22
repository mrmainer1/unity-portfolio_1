using MEC;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop
{
    public abstract class EEBehaviourSelectableLoop : EEBehaviourLoop
    {
        [SerializeField] private Segment segment = Segment.Update;
        private ContainerLoopData containerLoopData;
        private bool isInit;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            foreach (var loop in Loops)
            {
                if (loop.Segment != segment) continue;
                containerLoopData = loop;
                break;
            }
            
            containerLoopData ??= new ContainerLoopData(segment);
            AddData(containerLoopData, EESelectableLoop);
        }

        protected virtual void EESelectableLoop() {}
    }
}
