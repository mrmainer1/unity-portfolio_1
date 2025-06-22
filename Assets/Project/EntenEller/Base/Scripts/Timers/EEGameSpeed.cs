using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Timers
{
    public class EEGameSpeed : EEBehaviour
    {
        public float Speed;
        public bool IsPaused;
        
        public void Pause()
        {
            IsPaused = true;
            Refresh();
        }

        public void Unpause()
        {
            IsPaused = false;
            Refresh();
        }

        public void Set(float speed)
        {
            Speed = speed;
            Refresh();
        }

        private void Refresh()
        {
            Time.timeScale = IsPaused ? 0f : Speed;
        }
    }
}
