using System;
using Project.EntenEller.Base.Scripts.Advanced.Randoms;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.EntenEller.Base.Scripts.Advanced.Variables
{
    [Serializable]
    public class EEFloatRandomOrDefined
    {
        [SerializeField] private bool isRandom = false;
        
        [ShowIf("isRandom")] [SerializeField] private bool isRandomEverytime = false;
        
        [ShowIf("isRandom")] public float Min = 0;
        [ShowIf("isRandom")] public float Max = 0;
        [HideIf("isRandom")] [SerializeField] private float _Value = 0;
        
        private bool isInit;
        
        public float Value
        {
            get
            {
                if (!isRandom) return _Value;
                if (isInit) return _Value;
                if (!isRandomEverytime)
                {
                    isInit = true;
                }
                _Value = EERandomUtils.RandomFloat(Min, Max);
                return _Value;
            }
            private set => _Value = value;
        }

        public void SetValue(float value)
        {
            isRandom = false;
            _Value = value;
        }
    }
}
