using System;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Variables.Approach
{
    [Serializable]
    public class EEQuaternionApproach : EEVariableApproach
    { 
        public Quaternion Target;
        public Quaternion Current;
        
        protected override void GetNewValue()
        {
            if (IsUnscaled)
            {
                Current = Approach.Style switch
                {
                    ApproachStyle.Speed => Quaternion.RotateTowards(Current, Target, Approach.Speed * Time.unscaledDeltaTime),
                    ApproachStyle.Lerp => Quaternion.Lerp(Current, Target, Approach.Lerp * Time.unscaledDeltaTime),
                    ApproachStyle.Instant => Current,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            else
            {
                Current = Approach.Style switch
                {
                    ApproachStyle.Speed => Quaternion.RotateTowards(Current, Target, Approach.Speed * Time.deltaTime),
                    ApproachStyle.Lerp => Quaternion.Lerp(Current, Target, Approach.Lerp * Time.deltaTime),
                    ApproachStyle.Instant => Target,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        public void SetTarget(Quaternion target)
        {
            Target = Quaternion.Slerp(Quaternion.identity, target, OffsetFactor);
        }

        protected override bool CheckIfNear()
        {
            return Quaternion.Angle(Target, Current).IsAlmostZero(ApproximateError);
        }
    }
}
