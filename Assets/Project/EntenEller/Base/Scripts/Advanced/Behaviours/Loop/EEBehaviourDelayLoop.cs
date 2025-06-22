using System.Collections.Generic;
using MEC;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop
{
    public abstract class EEBehaviourDelayLoop : EEBehaviour
    {
        [SerializeField] private float delay = 0.1f;
        [SerializeField] private Segment segment;

        protected override void EEEnable()
        {
            base.EEEnable();
            Timing.RunCoroutine(Loop(), segment, ToString() + ComponentID);
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            Timing.KillCoroutines(ComponentID, ToString() + ComponentID);
        }

        private IEnumerator<float> Loop()
        {
            while (true)
            {
                yield return Timing.WaitForSeconds(delay);
                DelayedLoop();
            }
        }

        protected abstract void DelayedLoop();
    }
}
