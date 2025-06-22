using System;
using System.Collections.Generic;
using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.Components;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.OdinInspector;

namespace Project.EntenEller.Base.Scripts.Advanced.Triggers
{
    public abstract class EETriggerColliderListener : EEBehaviour
    {
        public event Action<EETriggerColliderListener> RawEnterEvent;
        public event Action<EETriggerColliderListener> RawExitEvent;
        
        public EENotifier RawEnterNotifier;
        public EENotifier RawExitNotifier;
        
        [ReadOnly] public List<EETriggerColliderListener> RawCollisions = new List<EETriggerColliderListener>();
        
        private List<EETriggerColliderListener> cache = new List<EETriggerColliderListener>();

        protected override void EEDisable()
        {
            base.EEDisable();
            while (RawCollisions.Any())
            {
                var they = RawCollisions[0];
                RawCollisions.RemoveAt(0);
                they.Exit(this);
                Exit(they);
            }
        }

        protected void Enter(EETriggerColliderListener they)
        {
            if (they.IsNull()) return;
            cache.Add(they);
            RawCollisions.Add(they);
        }

        private void LateUpdate()
        {
            if (cache.Count == 0) return;
            if (!IsAwaken) return;
            for (var i = 0; i < cache.Count; i++)
            {
                var trigger = cache[i];
                RawEnterEvent.Call(trigger);
                RawEnterNotifier.Notify();
            }
            cache.Clear();
        }

        protected void Exit(EETriggerColliderListener they)
        {
            if (they.IsNull()) return;
            RawCollisions.Remove(they);
            RawExitEvent.Call(they);
            RawExitNotifier.Notify();
        }
    }
}
