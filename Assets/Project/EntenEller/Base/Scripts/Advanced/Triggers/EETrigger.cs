using System;
using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Advanced.Tags;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Triggers
{
    [ExecutionOrder(9999)]
    [RequireComponent(typeof(EETagHolder))]
    public abstract class EETrigger : EEBehaviour
    {
        public bool IsColliding => RightCollisions.Count != 0;
        public List<string> CollidingTags = new List<string>();
        
        [ReadOnly] public List<EETrigger> RightCollisions = new List<EETrigger>();
        [ReadOnly] public List<EETrigger> WrongCollisions = new List<EETrigger>();
        
        public EENotifier EnterRightNotifier;
        public EENotifier EnterRightFirstNotifier;
        public EENotifier ExitRightNotifier;
        public EENotifier ExitAllRightNotifier;
        [Space]
        public EENotifier EnterWrongNotifier;
        public EENotifier ExitWrongNotifier;
        [Space]
        public EENotifier EnterEventNotifier;
        public EENotifier ExitEventNotifier;
        
        public event Action<EETrigger> EnterRightEvent;
        public event Action<EETrigger> ExitRightEvent;
        public event Action<EETrigger> EnterWrongEvent;
        public event Action<EETrigger> ExitWrongEvent;
        public event Action<EETrigger> EnterEvent;
        public event Action<EETrigger> ExitEvent;

        public void Add(EETrigger trigger, bool isRight)
        {
            if (isRight)
            {
                if (RightCollisions.Count == 0) EnterRightFirstNotifier.Notify();
                RightCollisions.Add(trigger);
                EnterRightEvent.Call(trigger);
                EnterRightNotifier.Notify();
            }
            else
            {
                WrongCollisions.Add(trigger);
                EnterWrongEvent.Call(trigger);
                EnterWrongNotifier.Notify();
            }
            EnterEvent.Call(trigger);
            EnterEventNotifier.Notify();
        }

        public void Remove(EETrigger trigger,bool isRight)
        {
            if (isRight)
            {
                RightCollisions.Remove(trigger);
                ExitRightEvent.Call(trigger);
                ExitRightNotifier.Notify();
                if (RightCollisions.Count == 0) ExitAllRightNotifier.Notify();
            }
            else
            {
                WrongCollisions.Remove(trigger);
                ExitWrongEvent.Call(trigger);
                ExitWrongNotifier.Notify();
            }
            ExitEvent.Call(trigger);
            ExitEventNotifier.Notify();
        }
        
        public void ReplaceTag(string a, string b)
        {
            CollidingTags[CollidingTags.IndexOf(a)] = b;
        }
    }
}