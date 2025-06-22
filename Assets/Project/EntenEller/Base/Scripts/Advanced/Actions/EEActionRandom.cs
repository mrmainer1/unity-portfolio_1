using System;
using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.Dictionaries;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.EntenEller.Base.Scripts.Advanced.Actions
{
    public class EEActionRandom : EEBehaviour
    {
        [SerializeField] private EEDictionary<EEAction, float> superActions;

        protected override void EEAwake()
        {
            base.EEAwake();
            if (superActions.Dictionary.Count == 0) throw new Exception("Actions are empty, nothing to random!");
            if (superActions.Dictionary.All(a => a.Value == 0)) throw new Exception("All randoms are zero, it's gonna be an endless loop!");
        }

        public void Call()
        {
            if (!enabled) return;
            while (true)
            {
                var kv = superActions.Dictionary.GetRandom();
                if (kv.Value > Random.value) continue;
                kv.Key.Call();
                return;
            }
        }
    }
}
