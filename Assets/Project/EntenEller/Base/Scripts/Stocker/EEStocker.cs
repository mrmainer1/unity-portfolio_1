using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Stocker
{
    public class EEStocker : EEBehaviour
    {
        public List<string> Cache = new List<string>();
        [SerializeField] private bool isAllowDuplicates = false;
        [SerializeField] private int targetAmount;
        public EENotifier AddNotifier, RemoveNotifier, AddFirstNotifier, RemoveAllNotifier, ReachedTargetNotifier;
    
        public void Add(string data)
        {
            if (!isAllowDuplicates)
            {
                if (Cache.Contains(data)) return;
            }
            if (Cache.Count == 0) AddFirstNotifier.Notify();
            AddNotifier.Notify();
            Cache.Add(data);
            if (Cache.Count == targetAmount) ReachedTargetNotifier.Notify();
        }

        public bool IsHavingAny()
        {
            return Cache.Count > 0;
        }

        public void Remove(string data)
        {
            for (var i = 0; i < Cache.Count; i++)
            {
                if (Cache[i] != data) continue;
                Cache.RemoveAt(i);
                if (Cache.Count == 0) RemoveAllNotifier.Notify();
                RemoveNotifier.Notify();
                return;
            }
        }
    }
}
