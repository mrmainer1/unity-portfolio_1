using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Affects
{
    [Serializable]
    public class EEAffectData
    {
        public List<string> Tags;
        public EEAffectType AffectType;
        public float Value;
        public int Layer;
        public bool IsPermanent;
        [HideIf("IsPermanent")] public float Duration;
        [HideInInspector] public float DestroyTime;
    }
}
