using System;
using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Save;
using Project.EntenEller.Base.Scripts.UI.Text;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.UI.Switcher
{
    public class EESwitcher : EEBehaviour
    {
        [SerializeField] private List<string> list = new List<string>();
        public EENotifier ChangeNotifier;
        public event Action<string> ChangedEventData;
        public event Action<int> ChangedEventIndex;
        public int Index;

        private void ActionLoad(string value)
        {
            Index = int.Parse(value);
            Change();
        }

        public void Next()
        {
            Index++;
            Change();
        }

        public void Previous()
        {
            Index--;
            Change();
        }

        private void Change()
        {
           var data = list.GetLooped(ref Index);
           GetSelf<EEText>().SetData(data);
           ChangeNotifier.Notify();
           ChangedEventData.Call(data);
           ChangedEventIndex.Call(Index);
        }
    }
}
