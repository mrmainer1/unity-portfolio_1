using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Actions;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.Dictionaries;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Animators
{
    public class EEAnimatorActions : EEBehaviourUpdate
    {
        [SerializeField] private EEAnimatorBase animator;
        [SerializeField] private EEDictionary<int, EEAction> actionsOnFrames;
        private readonly List<int> usedFrames = new List<int>();
        public int Frame;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            animator.RestartLoopEvent += OnRestartLoop;
            animator.StartNotifier.Event += OnStarted;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            animator.RestartLoopEvent -= OnRestartLoop;
            animator.StartNotifier.Event -= OnStarted;
        }

        private void OnRestartLoop()
        {
            foreach (var kv in actionsOnFrames.Dictionary)
            {
                if (usedFrames.Contains(kv.Key)) continue;
                kv.Value.Call();
            }
            OnStarted();
        }

        private void OnStarted()
        {
            usedFrames.Clear();
        }

        protected override void EEUpdate()
        {
            base.EEUpdate();
            if (!animator.IsActive) return;
            Frame = EEAnimatorUtils.GetFrame(animator.Animator, 0, 0, animator.TimeApproach.Current);
            foreach (var kv in actionsOnFrames.Dictionary)
            {
                if (usedFrames.Contains(kv.Key)) continue;
                if (kv.Key <= Frame)
                {
                    usedFrames.Add(kv.Key);
                    kv.Value.Call();
                }
            }
        }
    }
}
