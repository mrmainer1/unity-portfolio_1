using Project.EntenEller.Base.Scripts.Advanced.Behaviours;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.Components;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Advanced.Transforms.Simple;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.WaypointSystem
{
    public class EEWaypointRunner : EEBehaviourUpdate
    {
        public EEWaypointSystem WaypointSystem;
        public int Index = 0;
        public int TargetIndex = -1;
        public int Direction = 1;
        public EENotifier PointReachNotifier;
        public EENotifier StartRunNotifier;
        public EENotifier StopRunNotifier;
        public EENotifier EndRunNotifier;
        public bool IsOn = true;
        public bool IsLoop;

        public void SetWaypointSystem(EEWaypointSystem waypointSystem)
        {
            Index = 0;
            On();
            WaypointSystem = waypointSystem;
        }
        
        protected override void EEUpdate()
        {
            base.EEUpdate();
            if (WaypointSystem.IsNull()) return;
            if (!IsOn) return;
            var index = Index;
            var pos = WaypointSystem.GetNext(GetSelf<Transform>().position, ref Index, ref Direction, out var isEnd, TargetIndex, IsLoop);
            if (isEnd && IsLoop == false)
            {
                Off();
                EndRunNotifier.Notify();
                return;
            }
            if (index != Index) PointReachNotifier.Notify();
            GetSelf<EETransformApproachPosition>().Position.SetTarget(pos);
        }

        public void On()
        {
            IsOn = true;
            StartRunNotifier.Notify();
        }

        public void Off()
        {
            IsOn = false;
            StopRunNotifier.Notify();
            GetSelf<EETransformApproachPosition>().Position.Off();
        }
    }
}
