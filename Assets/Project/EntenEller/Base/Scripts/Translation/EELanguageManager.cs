using System;
using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Dictionaries;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Unity.Collections;

namespace Project.EntenEller.Base.Scripts.Translation
{
    public class EELanguageManager : EEBehaviour
    {
        [ReadOnly] public EEDictionary<string, List<string>> Data = new EEDictionary<string, List<string>>();
        [ReadOnly] public int Language;
        public event Action<int> SwitchedEvent;
        public EENotifier SwitchNotifier = new EENotifier();

        public void Switch(int language)
        {
            Language = language;
            SwitchedEvent.Call(Language);
            SwitchNotifier.Notify();
        }

        public void AddData(string key, List<string> value)
        {
            Data.Add(key, value);
        }

        public string GetTranslation(string key)
        {
            if (key == null) return "";
            if (key == "") return key;
            var result = Data.Dictionary.TryGetValue(key.Trim(), out var list);
            if (!result)
            {
                return key;
            }
            return list.Count <= Language ? "NO LANGUAGE!" : list[Language];
        }
    }
}
