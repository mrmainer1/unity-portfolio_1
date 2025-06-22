using Project.EntenEller.Base.Scripts.Advanced.Behaviours;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Scenes.Performance.Base;

namespace Project.EntenEller.Scenes.Performance.EEBehaviourVSMonoBehaviour.Scripts
{
    public class TestEE : EEBehaviourUpdate
    {
        protected override void EEUpdate()
        {
            base.EEUpdate();
            TestPerformance.Easy();
        }
    }
}
