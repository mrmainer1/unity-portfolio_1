using System;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Variables.Approach
{
    [Serializable]
    public class EEVector3Approach : EEVariableApproach
    {
        public Vector3 Target;
        public Vector3 Current;
        private Vector3 smoothDamp;
        
        protected override void GetNewValue()
        {
            if (IsUnscaled)
            {
                Current = Approach.Style switch
                {
                    ApproachStyle.Speed => Vector3.MoveTowards(Current, Target, Approach.Speed * Time.unscaledDeltaTime),
                    ApproachStyle.Lerp => Vector3.Lerp(Current, Target, Approach.Lerp * Time.unscaledDeltaTime),
                    ApproachStyle.SmoothDamp => Vector3.SmoothDamp(Current, Target, ref smoothDamp, Approach.SmoothDamp * Time.unscaledDeltaTime),
                    ApproachStyle.Instant => Target,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            else
            {
                Current = Approach.Style switch
                {
                    ApproachStyle.Speed => Vector3.MoveTowards(Current, Target, Approach.Speed * Time.deltaTime),
                    ApproachStyle.Lerp => Vector3.Lerp(Current, Target, Approach.Lerp * Time.deltaTime),
                    ApproachStyle.SmoothDamp => Vector3.SmoothDamp(Current, Target, ref smoothDamp, Approach.SmoothDamp * Time.unscaledDeltaTime),
                    ApproachStyle.Instant => Target,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }
        
        public void SetTarget(Vector3 target)
        {
            Target = target * OffsetFactor;
        }
        
        protected override bool CheckIfNear()
        {
            return Current.IsAlmostEqual(Target, ApproximateError);
        }
    }
}