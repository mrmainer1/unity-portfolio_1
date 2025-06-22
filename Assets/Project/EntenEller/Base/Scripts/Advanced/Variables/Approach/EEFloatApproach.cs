using System;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Variables.Approach
{
    [Serializable]
    public class EEFloatApproach : EEVariableApproach
    {
        public float Target;
        public float Current;
        public bool IsForward;

        protected override void GetNewValue()
        {
            if (IsUnscaled)
            {
                Current = Approach.Style switch
                {
                    ApproachStyle.Speed => Mathf.MoveTowards(Current, Target, Approach.Speed * Time.unscaledDeltaTime),
                    ApproachStyle.Lerp => Mathf.Lerp(Current, Target, Approach.Lerp * Time.unscaledDeltaTime),
                    ApproachStyle.Instant => Target,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            else
            {
                Current = Approach.Style switch
                {
                    ApproachStyle.Speed => Mathf.MoveTowards(Current, Target, Approach.Speed * Time.deltaTime),
                    ApproachStyle.Lerp => Mathf.Lerp(Current, Target, Approach.Lerp * Time.deltaTime),
                    ApproachStyle.Instant => Target,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }
        
        public void SetCurrent(float current)
        {
            Current = current;
        }
        
        public void SetTarget(float target)
        {
            Target = target * OffsetFactor;
        }

        protected override bool CheckIfNear()
        {
            var isEqual = Current.IsAlmostEqual(Target, ApproximateError);
            if (isEqual)
            {
                Current = Target;
            }
            else
            {
                IsForward = Target > Current;
            }
            return isEqual;
        }
    }
}