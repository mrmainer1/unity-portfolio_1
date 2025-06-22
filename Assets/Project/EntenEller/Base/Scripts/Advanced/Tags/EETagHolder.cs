using System.Collections.Generic;
using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Tags
{
    [ExecutionOrder(-9999)]
    [DisallowMultipleComponent]
    public class EETagHolder : EEBehaviour
    {
        public List<string> EETags = new();
        public int Amount => EETags.Count;
        public string FirstTag => EETags.FirstOrDefault();
        public string LastTag => EETags.Last();
        
        public EENotifier ChangeNotifier = new EENotifier();

        protected override void EEDestroy()
        {
            base.EEDestroy();
            EETags.ForEach(eeTag => EETagUtils.Remove(this, eeTag));
        }

        public void Add(string eeTag)
        {
            EETags.Add(eeTag);
            EETagUtils.Add(this, eeTag);
            Changed();
        }
    
        public void Remove(string eeTag)
        {
            EETags.Remove(eeTag);
            EETagUtils.Remove(this, eeTag);
            Changed();
        }
        
        public void Replace(string a, string b)
        {
            EETags[EETags.IndexOf(a)] = b;
            EETagUtils.Remove(this, a);
            EETagUtils.Add(this, b);
            Changed();
        }

        private void Changed()
        {
            ChangeNotifier.Notify();
        }
        
        public bool IsHavingTag(string eeTag)
        {
            return EETags.Contains(eeTag);
        }

        public bool IsHavingAnyTag(List<string> list)
        {
            if (list.Count == 0 || Amount == 0) return false;
            return list.Any(IsHavingTag);
        }
        
        #if UNITY_EDITOR

        [SerializeField] [ReadOnly] private List<EETagHolder> sameTags = new();
        
        [Button]
        public void FindSameTags()
        {
            sameTags = FindObjectsByType<EETagHolder>(FindObjectsInactive.Include, FindObjectsSortMode.None).Where(a => a.IsHavingAnyTag(EETags)).ToList();
            sameTags.Remove(this);
        }
        
        #endif
        
    }
}