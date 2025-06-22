using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Variables.Approach;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Timers;
using Project.EntenEller.Base.Scripts.UI.Menu;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Timing = MEC.Timing;

namespace Project.EntenEller.Base.Scripts.UI.SizeController
{
    public class EERectTransformSizeController : EEBehaviourEEMenu
    {
        private RectTransform rectTransform;
        [SerializeField] private RectTransform.Axis axis = RectTransform.Axis.Horizontal;
        [SerializeField] private bool isSmoothResizing;
        [ShowIf("isSmoothResizing")][SerializeField] private EEFloatApproach approach;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            rectTransform = GetComponent<RectTransform>();
        }
        
        public void Recount()
        {
            var size = LayoutUtility.GetPreferredSize(rectTransform, (int) axis);
            if (isSmoothResizing)
            {
                approach.SetTarget(size);
                Timing.KillCoroutines(ComponentID);
                Timing.RunCoroutine(SmoothResizing(), ComponentID);
            }
            else
            {
                rectTransform.SetSizeWithCurrentAnchors(axis, size);
                if (GetParent<ContentSizeFitter>()) GetParent<ContentSizeFitter>().enabled = false;
                EETime.StartTimer(new EETime.EETimerData
                {
                    Action = () =>
                    {
                        if (GetParent<ContentSizeFitter>()) GetParent<ContentSizeFitter>().enabled = true;
                    },
                    FinalTime = 0.01f
                });
            }
        }

        private IEnumerator<float> SmoothResizing()
        {
            while (true)
            {
                approach.Proceed();
                rectTransform.SetSizeWithCurrentAnchors(axis, approach.Current);
                yield return Timing.WaitForOneFrame;
            }
        }
    }
}
