using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.UI.ProgressBar;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Game.Health
{
    public class EEHealthBar : EEBehaviour
    {
        private EEHealth health;
        [SerializeField] private EEProgressBar progressBar;
        private bool wasEnabled;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            health = GetSelf<EEHealth>();
            health.HealthChangeNotifier.Event += OnHealthChanged;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            health.HealthChangeNotifier.Event -= OnHealthChanged;
        }

        protected override void EEEnable()
        {
            base.EEEnable();
            wasEnabled = true;
        }

        private void OnHealthChanged()
        {
            var hitpoints = health.HitPoints;
            var maxHitpoints = health.MaxHitpoints;
            if (wasEnabled)
            {
                wasEnabled = false;
                progressBar.SetCurrent(hitpoints);
            }
            progressBar.SetTarget(hitpoints);
            progressBar.SetMax(maxHitpoints);
        }
    }
}
