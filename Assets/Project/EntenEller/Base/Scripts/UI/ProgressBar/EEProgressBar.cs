using System;
using System.Collections.Generic;
using System.Linq;
using MPUIKIT;
using Project.EntenEller.Base.Scripts.Advanced.Actions;
using Project.EntenEller.Base.Scripts.Advanced.Dictionaries;
using Project.EntenEller.Base.Scripts.Advanced.Editors;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Project.EntenEller.Base.Scripts.Advanced.Variables.Approach;
using Project.EntenEller.Base.Scripts.UI.Menu;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.UI.ProgressBar
{
    public class EEProgressBar : EEBehaviourEEMenu
    {
        public float Max = 1;
        public float Current => children[indexOfMainIndicator].FloatApproach.Current;
        [SerializeField] private EEDictionary<float, EEAction> actionsOnIntervals;
        [SerializeField] private int indexOfMainIndicator = 0;
        private List<float> usedIntervals = new List<float>();
        private float middlePoint;
        private int directionOld = 0;
        [SerializeField] private bool isFilled = true;
        [SerializeField] private bool isReversed;

#if DEBUG
            [ReadOnly] public EEEditorTimePoint TimePointCurrent = new();
            public float CurrentValueDebug;
#endif
        
        private EEFloatApproachHolder[] _children;
        private EEFloatApproachHolder[] children
        {
            get
            {
                if (_children != null) return _children;
                _children = GetAllChild<EEFloatApproachHolder>();
                images = _children.Select(a => a.GetComponent<MPImage>()).ToArray();
                rectTransforms = _children.Select(a => a.GetComponent<RectTransform>()).ToArray();
                rectTransform = GetComponent<RectTransform>();
                return _children;
            }
        }

        private MPImage[] images;
        private RectTransform[] rectTransforms;
        private RectTransform rectTransform;
        
        protected override void MenuLoop()
        {
            base.MenuLoop();

            var floatApproach = children[indexOfMainIndicator].FloatApproach;
            
            var direction = Math.Sign(floatApproach.Target - floatApproach.Current);
            if (direction != directionOld)
            {
                directionOld = direction;
                usedIntervals.Clear();
                middlePoint = floatApproach.Current;
            }
            
            foreach (var holder in children)
            {
                holder.FloatApproach.Proceed();
            }
            
            Fill();
            if (floatApproach.IsReached) return;
            
            foreach (var kv in actionsOnIntervals.Dictionary)
            {
                if (usedIntervals.Contains(kv.Key)) continue;
                if (kv.Key.IsBetween(middlePoint, floatApproach.Current))
                {
                    usedIntervals.Add(kv.Key);
                    kv.Value.Call();
                }
            }
        }

        public void SetMax(float max)
        {
            Max = max;
        }

        private void Fill()
        {
            for (var i = 0; i < children.Length; i++)
            {
                var holder = children[i];
                var r = holder.FloatApproach.Current / Max;
                if (isReversed) r = 1 - r;
                if (isFilled)
                {
                    if (r < EEConstants.MeasurementAccuracyHigh) r = EEConstants.MeasurementAccuracyHigh;
                    images[i].fillAmount = r;
                }
                else
                {
                    var size = rectTransform.rect.size;
                    size.x *= r;
                    if (size.x < EEConstants.MeasurementAccuracyHigh) size.x = EEConstants.MeasurementAccuracyHigh;
                    rectTransforms[i].sizeDelta = size;
                }
            }
        }

        public void SetTarget(float targetValue)
        {
            foreach (var holder in children)
            {
                holder.FloatApproach.SetTarget(targetValue);
            }
            Fill();
        }
        
        public void SetCurrent(float currentValue)
        {
            foreach (var holder in children)
            {
                holder.FloatApproach.SetCurrent(currentValue);
            }
            Fill();
#if DEBUG
            CurrentValueDebug = currentValue;
            TimePointCurrent.Refresh();
#endif
        }
    }
}