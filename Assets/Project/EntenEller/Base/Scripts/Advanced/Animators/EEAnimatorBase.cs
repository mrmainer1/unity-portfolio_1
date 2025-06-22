using System;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.GameObjects;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Project.EntenEller.Base.Scripts.Advanced.Variables.Approach;
using Project.EntenEller.Base.Scripts.Timers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Animators
{
    public abstract class EEAnimatorBase : EEBehaviourUpdate
    {
        public string CurrentAnimationName;
        public EEFloatApproach TimeApproach;
        public Approach ApproachStyleForward, ApproachStyleBackward;
        
        public EENotifier StartNotifier;
        public EENotifier FinishNotifier;
        public event Action<float> UpdateTimeEvent;
        public event Action RestartLoopEvent;
        
        [ReadOnly] public Animator Animator;
        private EEGameObject obj;

        [SerializeField] private bool isLoop;
        public float DelayForward, DelayBackward;
        public bool IsOver;
        public bool IsActive = true;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            Animator = GetComponentInChildren<Animator>(true);
            Animator.speed = 0;
            obj = this.GetEEGameObject();
        }

        protected override void EEEnable()
        {
            base.EEEnable();
            SetState(IsActive);
            TimeApproach.ReachNotifier.Event += Reached;
        }
        
        protected override void EEDisable()
        {
            base.EEDisable();
            TimeApproach.ReachNotifier.Event -= Reached;
        }
        
        private void Reached()
        {
            Animator.Play(CurrentAnimationName, -1, TimeApproach.Target);
            FinishNotifier.Notify();
        }
        
        public void SetTime(float time)
        {
            TimeApproach.Current = time;
            Animator.Play(CurrentAnimationName, -1, TimeApproach.Current);
        }
        
        public void PlayForward(string animationName)
        {
            Play(animationName, 0.999f);
        }

        public void PlayBackward(string animationName)
        {
            Play(animationName, 0f);
        }
        
        public void Play(string animationName, float target)
        {
            EETime.StopAllTimersForComponentID(ComponentID);
            EETime.StartTimer(new EETime.EETimerData
            {
                ComponentID = ComponentID,
                Action = () =>
                {
                    TimeApproach.Approach = target > TimeApproach.Target ? ApproachStyleForward : ApproachStyleBackward;
                    SetState(true);
                    CurrentAnimationName = animationName;
                    TimeApproach.SetTarget(target);
                    StartNotifier.Notify();
                    IsOver = false;
                },
                FinalTime = target > TimeApproach.Target ? DelayForward : DelayBackward
            });
        }
        
        protected void PlayAnimation()
        {
            if (Time.deltaTime > 0.2f) return;
            Animator.Play(CurrentAnimationName, -1, TimeApproach.Current);
            if (!obj.IsActive) return;
            if (!IsActive) return;
            TimeApproach.Proceed();
            IsOver = TimeApproach.IsReached;
            if (!IsOver)
            {
                UpdateTimeEvent.Call(TimeApproach.Current);
            }
            else
            {
                if (isLoop)
                {
                    if (TimeApproach.Target.IsAlmostEqual(1))
                    {
                        RestartLoopEvent.Call();
                        TimeApproach.SetCurrent(0);
                    }
                }
            }
        }

        public void Stop()
        {
            TimeApproach.SetCurrent(0);
            Animator.Play(CurrentAnimationName, -1, TimeApproach.Current);
            SetState(false);
        }

        public void Pause()
        {
            SetState(false);
        }

        private void SetState(bool isActive)
        {
            IsActive = isActive;
        }
        
        public void SetTrueBool(string nameBool)
        {
            Animator.SetBool(nameBool, true);
        }
        
        public void SetFalseBool(string nameBool)
        {
            Animator.SetBool(nameBool, false);
        }

        public void SetSpeed(float speed)
        {
            TimeApproach.Approach.SetSpeed(speed);
        }

        public void ChangeForwardDelay(float delay)
        {
            DelayForward = delay;
        }

        public void ChangeBackwardDelay(float delay)
        {
            DelayBackward = delay;
        }

    }
}
