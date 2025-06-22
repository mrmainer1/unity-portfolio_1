using System;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Project.EntenEller.Base.Scripts.Conditions
{
    [Serializable]
    public abstract class EECondition : EEBehaviour
    {
        public abstract bool IsFulfill();
    }
}