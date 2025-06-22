using System;
using System.Collections.Generic;
using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.Dictionaries;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Affects
{
    [ExecuteInEditMode]
    [ExecutionOrder(9999)]
    public class EEAffect : EEBehaviourUpdate
    {
        [SerializeField] private bool hasLimits;
        [ShowIf("hasLimits")] [SerializeField] private float min, max = 10000;
        [ReadOnly] public float Value;
        [SerializeField] private List<EEAffectData> startingAffects;
        [ReadOnly] public EEDictionary<EEAffectType, List<EEAffectData>> Affects = new();
        
        public EENotifier ChangeNotifier;
        public event Action<float> ChangedValueEvent;
        private bool isRecount;

        public EEAffect()
        {
            Init();
        }

        protected override void EEAwake()
        {
            base.EEAwake();
            foreach (var affect in startingAffects)
            {
                Add(affect);
            }
            Init();
        }

        [Button]
        private void Init() 
        {
            var values = Enum.GetValues(typeof(EEAffectType)).Cast<EEAffectType>();;
            foreach(var key in values)
            {
                if (Affects.Dictionary.ContainsKey(key)) return;
                Affects.Add(key, new List<EEAffectData>());
            }
        }

        public void Add(EEAffectData affectData)
        {
            affectData.DestroyTime = Time.time + affectData.Duration;
            Affects.Dictionary[affectData.AffectType].Add(affectData);
            isRecount = true;
        }

        public void Remove(EEAffectData affectData)
        {
            Affects.Dictionary[affectData.AffectType].Remove(affectData);
            isRecount = true;
        }

        public void RemoveByTag(string tagToRemove)
        {
            
            isRecount = true;
        }
        
        protected override void EEUpdate()
        {
            base.EEUpdate();

            foreach (var kv in Affects.Dictionary)
            {
                var list = kv.Value;
                for (var i = 0; i < list.Count; i++)
                {
                    var affect = list[i];
                    if (affect.IsPermanent) continue;
                    if (affect.DestroyTime < Time.time) continue;
                    list.Remove(affect);
                    isRecount = true;
                }
            }

            if (!isRecount) return;
            isRecount = false;

            var value = 0f;
            foreach (var affectData in Affects.Dictionary[EEAffectType.Additive])
            {
                value += affectData.Value;
            }

            var cache = new Dictionary<int, float>();
            foreach (var affectData in Affects.Dictionary[EEAffectType.Highest])
            {
                if (!cache.ContainsKey(affectData.Layer)) cache.Add(affectData.Layer, 0f);
                if (Mathf.Abs(cache[affectData.Layer]) < Mathf.Abs(affectData.Value))
                {
                    cache[affectData.Layer] = affectData.Value;
                }
            }

            foreach (var kv in cache)
            {
                value += kv.Value;
            }

            var valuePercentage = 0f;
            foreach (var affectData in Affects.Dictionary[EEAffectType.Percentage])
            {
                valuePercentage += affectData.Value;
            }

            value += value * valuePercentage;
            
            if (hasLimits)
            {
                if (value > max) value = max;
                if (value < min) value = min;
            }
            
            foreach (var affectData in Affects.Dictionary[EEAffectType.OutOfLimitAdditive])
            {
                value += affectData.Value;
            }
            
            valuePercentage = 0f;
            foreach (var affectData in Affects.Dictionary[EEAffectType.OutOfLimitPercentage])
            {
                valuePercentage += affectData.Value;
            }
            value += value * valuePercentage;
            
                
            foreach (var affectData in Affects.Dictionary[EEAffectType.Constant])
            {
                value = affectData.Value;
            }    
                
            if (!Value.IsAlmostEqual(value))
            {
                Value = value;
                ChangedValueEvent.Call(Value);
            }
        }
    }
}