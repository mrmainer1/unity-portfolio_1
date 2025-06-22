using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Dictionaries
{
    [Serializable]
    public class EEDictionary<T1, T2>
    {
        [SerializeField] private List<Data> list = new List<Data>();
        private Dictionary<T1, T2> _dictionary;
        
        public Dictionary<T1, T2> Dictionary
        {
            get
            {
                if (_dictionary != null) return _dictionary;
                _dictionary = new Dictionary<T1, T2>();
                foreach (var data in list)
                {
                    _dictionary.Add(data.Key, data.Value);
                }
                return _dictionary;
            }
        }

        public void ClearAll()
        {
            Dictionary.Clear();
#if UNITY_EDITOR
            list.Clear();
#endif
        }

        public void Add(T1 key, T2 value)
        {
            Dictionary.Add(key, value);
#if UNITY_EDITOR
            list.Add(new Data
            {
                Key = key,
                Value = value
            });
#endif
        }
        
        public void Remove(T1 key)
        {
            Dictionary.Remove(key);
#if UNITY_EDITOR
            list.RemoveAll(a => a.Key.Equals(key));
#endif
        }

        public void SetValue(T1 key, T2 value)
        {
            Dictionary[key] = value;
#if UNITY_EDITOR
            list.First(a => a.Key.Equals(key)).Value = value;
#endif
        }

        public List<Data> GetList()
        {
            return list;
        }

        [Serializable]
        public class Data
        {
            public T1 Key;
            public T2 Value;
        }
    }
}
