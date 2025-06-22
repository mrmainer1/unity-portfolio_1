using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Save;
using Project.EntenEller.Base.Scripts.Timers;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Sound
{
    public class EESoundVolumeGlobalController : EEBehaviour
    {
        [SerializeField] private string type;
        public float Volume;

        private void OnLoad(string value)
        {
            Volume = value.ParseFloat();
            Set(Volume);
        }

        public void Set(float volume)
        {
            Volume = volume;
            EESound.SetGlobalVolume(type, Volume);
        }
    }
}
