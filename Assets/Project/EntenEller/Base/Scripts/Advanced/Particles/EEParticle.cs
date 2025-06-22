using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.Utilities;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Particles
{
    public class EEParticle : EEBehaviour
    {
        public bool IsOn;

        protected override void EEAwake()
        {
            base.EEAwake();
            if (IsOn) On();
            else Off();
        }

        public void On()
        {
            IsOn = true;
            Play();
        }

        public void Off()
        {
            IsOn = false;
            Stop();
        }

        private void Play()
        {
            if (GetSelf<ParticleSystem>()) GetSelf<ParticleSystem>().Play();
            GetAllChild<ParticleSystem>().ForEach(a => a.Play());
        }

        public void Stop()
        {
            if (GetSelf<ParticleSystem>()) GetSelf<ParticleSystem>().Stop();
            GetAllChild<ParticleSystem>().ForEach(a => a.Stop());
        }
        
    }
}
