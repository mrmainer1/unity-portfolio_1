using System;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Variables.Approach
{
    public abstract class EEVariableApproach
    {
        public bool IsOn = true;
        public bool IsUnscaled = false;
        
        [ReadOnly] public bool IsReached = true;
        
        [SerializeField] protected float ApproximateError = EEConstants.MeasurementAccuracyHigh;
        [SerializeField] protected float OffsetFactor = 1;
        public Approach Approach = new Approach();
        
        public EENotifier OnNotifier = new EENotifier();
        public EENotifier OffNotifier = new EENotifier();
        public EENotifier ReachNotifier = new EENotifier();
        
        public void Proceed()
        {
            if (!IsOn) return;
            if (CheckIfNear())
            {
                if (IsReached) return;
                IsReached = true;
                ReachNotifier.Notify();
                return;
            }
            GetNewValue();
            IsReached = false;
        }

        protected abstract bool CheckIfNear();
        
        protected abstract void GetNewValue();

        public void On()
        {
            IsReached = false;
            IsOn = true;
            OnNotifier.Notify();
        }
        
        public void Off()
        {
            IsOn = false;
            OffNotifier.Notify();
        }

        public void SetApproximateError(float approximateError)
        {
            ApproximateError = approximateError;
        }
    }
    
    public enum ApproachStyle
    {
        Lerp,
        Speed,
        Instant,
        SmoothDamp
    }
    
    [Serializable]
    public class Approach
    {
        public ApproachStyle Style = ApproachStyle.Speed;
        [ShowIf("Style", ApproachStyle.Lerp)] public float Lerp = 20f;
        [ShowIf("Style", ApproachStyle.Speed)] public float Speed = 5f;
        [ShowIf("Style", ApproachStyle.SmoothDamp)] public float SmoothDamp = 20f;
        
        public void SetSpeed(float speed)
        {
            Speed = speed;
        }

        public void SetLerp(float lerp)
        {
            Lerp = lerp;
        }
        
        public void SetSmoothDamp(float smoothDamp)
        {
            SmoothDamp = smoothDamp;
        }
    }
}
