using Project.EntenEller.Base.Scripts.Advanced.GameObjects;
using Project.EntenEller.Base.Scripts.Advanced.Spawns;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Patterns.Pool
{
    public class EEPoolObject : EEGameObject
    {
        public EEPoolObject Origin;
        public int FrameEnabled;
        public int J;
        public float DisableTime;
        public float DelayTimeBeforeNextActive = 0.01f;

        protected override void EEEnable()
        {
            base.EEEnable();
            FrameEnabled = Time.frameCount;
        }

        protected override void EEDisable()
        {
            DisableTime = Time.time;
            (Spawner as EESpawnerEEPoolObject).DespawnPoolObject(this);
            base.EEDisable();
        }
    }
}