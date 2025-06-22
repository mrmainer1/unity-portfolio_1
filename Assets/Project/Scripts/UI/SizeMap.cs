using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using Project.Scripts.Map;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UINewMap
{
    public class SizeMap : EEBehaviour
    {
        [SerializeField] private Slider sliderHeight;
        [SerializeField] private Slider sliderWidth;

        public EENotifier SizeChangedNotifier;

        protected override void EEAwake()
        {
            base.EEAwake();
            RememberSize();
        }

        public void CalculateHeight()
        {
            sliderHeight.value = Calculate(sliderHeight.value);
            RememberSize();
            SizeChangedNotifier.Notify();
        }

        public void CalculateWidth()
        {
            sliderWidth.value = Calculate(sliderWidth.value);
            RememberSize();
            SizeChangedNotifier.Notify();
        }
    
        private float Calculate(float value) => Mathf.Round(value / 2) * 2;
        private void RememberSize() => EESingleton.Get<MapResizer>().RememberSize((int) sliderWidth.value, ((int) sliderHeight.value));
    }
}

