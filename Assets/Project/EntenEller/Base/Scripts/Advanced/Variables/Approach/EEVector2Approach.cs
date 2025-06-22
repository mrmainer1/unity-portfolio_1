using System;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Variables.Approach
{
    [Serializable]
    public class EEVector2Approach : EEVariableApproach
    {
        public Vector2 Target;
        public Vector2 Current;
        
        protected override void GetNewValue()
        {
            if (IsUnscaled)
            {
                Current = Approach.Style switch
                {
                    ApproachStyle.Speed => Vector2.MoveTowards(Current, Target, Approach.Speed * Time.unscaledDeltaTime),
                    ApproachStyle.Lerp => Vector2.Lerp(Current, Target, Approach.Lerp * Time.unscaledDeltaTime),
                    ApproachStyle.Instant => Current,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            else
            {
                Current = Approach.Style switch
                {
                    ApproachStyle.Speed => Vector2.MoveTowards(Current, Target, Approach.Speed * Time.deltaTime),
                    ApproachStyle.Lerp => Vector2.Lerp(Current, Target, Approach.Lerp * Time.deltaTime),
                    ApproachStyle.Instant => Target,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }
        
        public void SetTarget(Vector2 target)
        {
            Target = target * OffsetFactor;
        }

        protected override bool CheckIfNear()
        {
            return Current.IsAlmostEqual(Target, ApproximateError);
        }
    }
}
