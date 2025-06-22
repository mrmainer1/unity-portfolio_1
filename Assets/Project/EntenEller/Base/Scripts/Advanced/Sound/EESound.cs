using System;
using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.Variables.Approach;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Sound
{
    public class EESound : EEBehaviourUpdate
    {
        private static readonly Dictionary<string, VolumeData> volumes = new Dictionary<string, VolumeData>();
        private static readonly Dictionary<string, List<EESound>> sounds = new Dictionary<string, List<EESound>>();
            
        [SerializeField] private string type;
        [SerializeField] private AudioClip clip;
        [SerializeField] private float baseVolume = 1;
        
        private AudioSource audioSource;
        [SerializeField] private EEFloatApproach volumeApproach;

#if UNITY_EDITOR
        [ReadOnly] [SerializeField] private int frameOfPlay = -1;
#endif
        
        protected override void EEAwake()
        {
            base.EEAwake();
            if (!sounds.ContainsKey(type)) sounds.Add(type, new List<EESound>());
            sounds[type].Add(this);
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = clip;
            SetVolume(audioSource.volume, true);
        }
        
        protected override void EEUpdate()
        {
            base.EEUpdate();
            volumeApproach.Proceed();
            audioSource.volume = volumeApproach.Current * baseVolume;
        }

        public void SetLocalVolume(float local)
        {
            SetVolume(local, false);
        }

        private void SetVolume(float local, bool isInstant)
        {
            if (!volumes.ContainsKey(type)) volumes.Add(type, new VolumeData(local, 0));
            var vd = volumes[type];
            vd.Local = local;
            var r = vd.Global * vd.Local;
            if (isInstant) SetTargetInstantly(r);
            else SetTarget(r);
        }
        
        public static void SetGlobalVolume(string type, float global)
        {
            if (!volumes.ContainsKey(type)) volumes.Add(type, new VolumeData(1, global));
            var vd = volumes[type];
            vd.Global = global;
            if (sounds.ContainsKey(type)) sounds[type].ForEach(a => a.SetTargetInstantly(vd.Global * vd.Local));
        }
        
        private void SetTarget(float target)
        {
            volumeApproach.SetTarget(target);
        }
        
        private void SetTargetInstantly(float target)
        {
            audioSource.volume = target;
            SetTarget(target);
            volumeApproach.Current = target;
        }
        
        public void Play()
        {
            #if UNITY_EDITOR
            frameOfPlay = Time.frameCount;
            #endif
            audioSource.Play();
        }

        public void Stop()
        {
            audioSource.Stop();
        }
        
        [Serializable]
        private class VolumeData
        {
            public float Local;
            public float Global;

            public VolumeData(float local, float global)
            {
                Local = local;
                Global = global;
            }
        }
    }
}
