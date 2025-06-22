using System.Collections.Generic;
using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.VisualHacks
{
    public class EEWaitForStableFPS : EEBehaviourUpdate
    {
        private readonly List<float> deltaTimeValues = new List<float>();
        [SerializeField] private float threshold = 0.1f;
        public EENotifier StableNotifier;
        
        protected override void EEUpdate()
        {
            base.EEUpdate();
            deltaTimeValues.Add(Time.deltaTime);
            if (deltaTimeValues.Count < 30) return;
            if (deltaTimeValues.Max() <= threshold)
            {
                StableNotifier.Notify();
                enabled = false;
            }
            deltaTimeValues.RemoveAt(0);
        }
    }
}
