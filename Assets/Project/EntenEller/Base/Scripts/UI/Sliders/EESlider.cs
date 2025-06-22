using System;
using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Actions;
using Project.EntenEller.Base.Scripts.Advanced.Dictionaries;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Project.EntenEller.Base.Scripts.Advanced.Variables.Approach;
using Project.EntenEller.Base.Scripts.Save;
using Project.EntenEller.Base.Scripts.UI.Menu;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Project.EntenEller.Base.Scripts.UI.Sliders
{
    public class EESlider : EEBehaviourEEMenu
    {
        public EEFloatApproach FloatApproach;
        [SerializeField] private EEDictionary<float, EEAction> actionsOnIntervals;
        [SerializeField] [ReadOnly] private List<float> usedIntervals = new List<float>();
        private float middlePoint;
        private int directionOld = 0;
        private Slider slider;
        public EENotifier ChangeNotifier;

        protected override void EEAwake()
        {
            base.EEAwake();
            slider = GetComponent<Slider>();
            slider.onValueChanged.AddListener(OnValueChange);
        }
        
        private void OnLoad(string value)
        {
            FloatApproach.Target = FloatApproach.Current = value.ParseFloat();
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            slider.onValueChanged.RemoveListener(OnValueChange);
        }

        private void OnValueChange(float value)
        {
            ChangeNotifier.Notify();
        }

        protected override void MenuStartShow()
        {
            base.MenuStartShow();
            slider.value = FloatApproach.Current;
        }

        protected override void MenuLoop()
        {
            base.MenuLoop();
            
            var direction = Math.Sign(FloatApproach.Target - FloatApproach.Current);
            if (direction != directionOld)
            {
                directionOld = direction;
                usedIntervals.Clear();
                middlePoint = FloatApproach.Current;
            }
            
            FloatApproach.Proceed();
            if (FloatApproach.IsReached) return;
            
            foreach (var kv in actionsOnIntervals.Dictionary)
            {
                if (usedIntervals.Contains(kv.Key)) continue;
                if (kv.Key.IsBetween(middlePoint, FloatApproach.Current))
                {
                    usedIntervals.Add(kv.Key);
                    kv.Value.Call();
                }
            }
            
            slider.value = FloatApproach.Current;
        }

        public void Add(float value)
        {
            SetTarget(FloatApproach.Target + value);
        }

        public void SetTarget(float targetValue)
        {
            FloatApproach.Target = targetValue;
        }
    }
}
