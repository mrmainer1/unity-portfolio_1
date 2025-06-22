using System;
using System.Collections.Generic;
using MEC;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop
{
    public abstract class EEBehaviourLoop : EEBehaviour
    {
        protected static readonly HashSet<ContainerLoopData> Loops = new HashSet<ContainerLoopData>();
        private ContainerLoopData containerLoopData;
        [HideInInspector] public int IndexBehaviour;
        private Action currentAction;
        
        protected void AddData(ContainerLoopData data, Action action)
        {
            Loops.Add(data);
            containerLoopData = data;
            currentAction = action;
        }
        
        protected override void EEEnable()
        {
            base.EEEnable();
            IndexBehaviour = containerLoopData.Add(currentAction, this);
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            containerLoopData.Remove(IndexBehaviour);
        }
    }
    
    public class ContainerLoopData
    {
        private readonly LoopData[] behaviours = new LoopData[100000];
        private int amount;
        public Segment Segment;
        
        public ContainerLoopData(Segment segment)
        {
            Segment = segment;
            Timing.RunCoroutine(GlobalLoop(), segment);
        }

        public int Add(Action action, EEBehaviourLoop behaviourLoop)
        {
            behaviours[amount].Set(action, behaviourLoop);
            return amount++;
        }

        public void Remove(int index)
        {
            amount--;
            if (amount == 0) return;
            var last = behaviours[amount];
            last.BehaviourLoop.IndexBehaviour = index;
            behaviours[index] = last;
        }

        private IEnumerator<float> GlobalLoop()
        {
            while (true)
            {
                yield return Timing.WaitForOneFrame;
                for (var i = 0; i < amount; i++)
                {
                    behaviours[i].Action();
                }
            }
        }
    }
    
    public struct LoopData
    {
        public Action Action;
        public EEBehaviourLoop BehaviourLoop;

        public void Set(Action action, EEBehaviourLoop behaviourLoop)
        {
            Action = action;
            BehaviourLoop = behaviourLoop;
        }
    }
}