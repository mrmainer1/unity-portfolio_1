using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Actions
{
    public class EEActionStartOver : EEBehaviour
    {
        public EEAction Action;
        [SerializeField] private float minTime = 0.1f;
        private float banTimer;
        
        public void Call()
        {
            if (Time.time < banTimer) return;
            banTimer = Time.time + minTime;
            Action.Call();
        }
    }
}
