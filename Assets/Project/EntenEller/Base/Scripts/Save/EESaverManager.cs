using System;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.Utilities;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Save
{
    public class EESaverManager : EEBehaviour
    {
        [HideInInspector] public EESaverSource SaverSource;
        [HideInInspector] public EESaverSlot SaverSlot;
        public EENotifier LoadAllNotifier, SaveAllNotifier;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            FindObjectsByType<EESaver>(FindObjectsSortMode.None).ForEach(a => a.On());
            SaverSource = GetComponent<EESaverSource>();
            SaverSlot = GetComponent<EESaverSlot>();
        }
        
        public void SaveSingle(EESaver saver)
        {
            SaverSource.Save(saver);
        }

        public void LoadSingle(EESaver saver, Action<string> actionOnLoad)
        {
            SaverSource.Load(saver, actionOnLoad);
        }

        public void SaveAll()
        {
            SaveAllNotifier.Notify();
        }

        public void LoadAll(string slotID)
        {
            SaverSlot.SetID(slotID);
            LoadAllNotifier.Notify();
        }
    }
}
