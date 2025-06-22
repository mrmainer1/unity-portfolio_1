using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Cache
{
    public class EEGlobalCacheQuestPoint : EEBehaviour
    {
        public string Key;
        public int Value;
        
        public void Set()
        {
            EEGlobalCache.Set(Key, Value);
        }

        public void Add()
        {
            var v = EEGlobalCache.Get(Key) ?? 0;
            EEGlobalCache.Set(Key, (int) v + Value);
        }

        public void SetKey(string key)
        {
            Key = key;
        }

        public void SetValue(int value)
        {
            Value = value;
        }
    }
}
