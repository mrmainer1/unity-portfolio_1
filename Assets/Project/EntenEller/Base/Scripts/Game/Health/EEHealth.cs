using System;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Affects;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.EntenEller.Base.Scripts.Game.Health
{
    [ExecuteAfter(typeof(EEAffect))]
    public class EEHealth : EEBehaviourLate
    {
        public EENotifier HitNotifier, HealNotifier, HealthChangeNotifier, ReviveNotifier, DeathNotifier;
        public event Action<float> HitEvent, HealEvent, HealthChangeEvent;
        [ReadOnly] public float HitPoints;
        [ReadOnly] public float MaxHitpoints;
        [ReadOnly] public bool IsDead;
        [SerializeField] private EEAffect Damage;
        private float damageTaken;

        protected override void EEAwake()
        {
            base.EEAwake();
            GetSelf<EEAffect>().ChangeNotifier.Event += OnChanged;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            GetSelf<EEAffect>().ChangeNotifier.Event += OnChanged;
        }

        private void OnChanged()
        {
            Hit(0);
        }
        
        protected override void EEEnable()
        {
            base.EEEnable();
            Hit(0);
        }

        protected override void EELateUpdate()
        {
            base.EELateUpdate();
            if (Damage.Value.IsAlmostZero()) return;
            Hit(Damage.Value);
        }

        [Button]
        public void HitRandom()
        {
            Hit(Random.Range(0, 100));
        }
        
        public void Hit(float amount)
        {
            if (IsDead) return;
            damageTaken += amount;
            if (damageTaken < 0) damageTaken = 0;
            MaxHitpoints = GetSelf<EEAffect>().Value;
            HitPoints = MaxHitpoints - damageTaken;
            if (HitPoints < 0)
            {
                Kill();
                return;
            }
            if (amount < 0)
            {
                HealNotifier.Notify();
                HealEvent.Call(-amount);
            }
            if (amount > 0)
            {
                HitNotifier.Notify();
                HitEvent.Call(amount);
            }
            HealthChangeNotifier.Notify();
            HealthChangeEvent.Call(amount);
        }

        public void Kill()
        {
            IsDead = true;
            DeathNotifier.Notify();
        }

        public void Revive()
        {
            if (!IsDead) return;
            IsDead = false;
            FullHeal();
            ReviveNotifier.Notify();
        }

        public void FullHeal()
        {
            damageTaken = 0;
            HitPoints = MaxHitpoints;
            HealthChangeNotifier.Notify();
            HealthChangeEvent.Call(MaxHitpoints);
        }
    }
}
