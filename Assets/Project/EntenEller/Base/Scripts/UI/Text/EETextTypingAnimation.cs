using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Advanced.Variables.Approach;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.UI.Text
{
    public class EETextTypingAnimation : EEText
    {
        private int currentStopIndex;
        private string current = "", target = "";
        public EEFloatApproach TimeApproach = new EEFloatApproach();
        public Approach ApproachStyleForward, ApproachStyleBackward;
        public EENotifier FinishShowNotifier, FinishHideNotifier;
        [ReadOnly] public bool IsDone = true;
        
        protected override void Change()
        {
            target = GetTranslated();
            
            var length = Mathf.Min(target.Length, current.Length);
            currentStopIndex = length;
            
            for (var i = 0; i < length; i++)
            {
                if (current[i] == target[i]) continue;
                currentStopIndex = i;
                break;
            }

            TimeApproach.Approach = ApproachStyleBackward;
            TimeApproach.Current = current.Length;
            TimeApproach.SetTarget(currentStopIndex);
            TimeApproach.Proceed();

            IsDone = false;
        }

        protected override void MenuLoop()
        {
            base.MenuLoop();
            if (IsDone) return;
            if (TimeApproach.IsReached)
            {
                if (current != target)
                {
                    TimeApproach.Approach = ApproachStyleForward;
                    TimeApproach.SetTarget(target.Length);
                    SetText();
                }
                else if (!IsDone)
                {
                    IsDone = true;
                    if (TimeApproach.IsForward)
                    {
                        FinishShowNotifier.Notify();
                    }
                    else
                    {
                        FinishHideNotifier.Notify();
                    }
                }
            }
            else
            {
                SetText();
            }
            
            TimeApproach.Proceed();

            void SetText()
            {
                var index = Mathf.RoundToInt((int) TimeApproach.Current);
                current = TimeApproach.IsForward ? target.Substring(0, index) : current.Substring(0, index);
                Set(current);
            }
        }
    }
}
