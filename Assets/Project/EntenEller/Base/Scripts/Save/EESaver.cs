using System;
using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Editors;
using Project.EntenEller.Base.Scripts.Advanced.Randoms;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Functions;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Save
{
    public abstract class EESaver : EEBehaviour
    {
        protected static readonly List<EESaver> Savers = new List<EESaver>();
        protected static EESaverManager SaverManager;
        
        [SerializeField] private bool isInSlot = true;
        
        public string Key;
        public string FullKey => GetFullKey();
        [ReadOnly] public string Value;
        public string DefaultValue;
        
        #if UNITY_EDITOR
        public EEEditorTimePoint SaveTimePoint, LoadTimePoint;
        #endif
        
        public bool IsOn;
        
        public void On()
        {
            if (IsOn) return;
            IsOn = true;
            if (!SaverManager) SaverManager = EESingleton.Get<EESaverManager>();
            if (!Savers.Contains(this)) Savers.Add(this);
            SaverManager.SaveAllNotifier.Event += OnSaveAll;
            SaverManager.LoadAllNotifier.Event += OnLoadAll;
        }

        public void Off()
        {
            IsOn = false;
            EEQueueFunction.Remove(ComponentID);
            if (Savers.Contains(this)) Savers.Remove(this);
            SaverManager.SaveAllNotifier.Event -= OnSaveAll;
            SaverManager.LoadAllNotifier.Event -= OnLoadAll;
        }

        protected override void EEAwake()
        {
            base.EEAwake();
            On();
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            Off();
        }

        private void OnSaveAll()
        {
            LocalSave();
        }

        private void OnLoadAll()
        {
            LocalLoad();
        }

        public abstract void LocalSave();
        public abstract void LocalLoad();

        public virtual int GetQueue()
        {
            return 0;
        }
        
        [Button]
        public void GenerateRandomKey()
        {
            Key = gameObject.name + "_" + EERandomUtils.RandomTimeBasedString();
        }
        
        protected void Save(string value)
        {
#if UNITY_EDITOR
            SaveTimePoint.Refresh();
            if (Key.IsEmpty()) throw new Exception(gameObject.name);
#endif
            Value = value;
            SaverManager.SaveSingle(this);
        }
        
        protected void Load(Action<string> actionOnLoad = null)
        {
           // base.EEAwake();
#if UNITY_EDITOR
            LoadTimePoint.Refresh();
            if (Key.IsEmpty()) throw new Exception(gameObject.name);
#endif
            EEQueueFunction.Add(ComponentID, GetQueue(), Action);
            
            void Action()
            {
                SaverManager.LoadSingle(this, actionOnLoad);
            }
        }
        
        public void SetKey(string key)
        {
            Key = key;
        }
        
        public string GetFullKey()
        {
            if (isInSlot) return "(" + SaverManager.SaverSlot.SlotID + ")" + Key;
            return Key;
        }
    }
}
