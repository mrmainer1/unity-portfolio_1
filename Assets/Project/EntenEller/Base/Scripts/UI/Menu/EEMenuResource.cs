using System;
using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Debugs;
using Project.EntenEller.Base.Scripts.Advanced.ScriptableObjects;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.UI.Menu
{
    [CreateAssetMenu(fileName = "EEMenuData", menuName = "EntenEller/Menu/EEMenuData", order = 1)]
    [Serializable]
    public class EEMenuResource : EEResource
    {
        public List<string> MenuEETags = new List<string>();

        public MenuMode MenuMode = MenuMode.Add;
        public bool IsRecordHistory = true;
        public bool IsSavePoint = false;
        
        public bool HasName = false;
        [ShowIf("HasName", false)]
        public string Key; 
        
        public string Id => GetInstanceID().ToString();

        public void On()
        {
            switch (MenuMode)
            {
                case MenuMode.Add:
                    EEDebug.Log(EEDebugTag.Menu, this + " ADD");
                    EESingleton.Get<EEMenuManager>().Add(this);
                    break;
                case MenuMode.Recreate:
                    EEDebug.Log(EEDebugTag.Menu, this + " RECREATE");
                    EESingleton.Get<EEMenuManager>().Recreate(this);
                    break;
            }
        }
        
        public void Off()
        {
            EEDebug.Log(EEDebugTag.Menu, this + " REMOVE");
            EESingleton.Get<EEMenuManager>().Remove(this);
        }
        
        public void CleanUntil()
        {
            EESingleton.Get<EEMenuManager>().CleanUntil(this);
        }

        public void CleanAll()
        {
            EESingleton.Get<EEMenuManager>().CleanAll();
        }

        public void Back()
        {
            EESingleton.Get<EEMenuManager>().Back();
        }
    }
    
    public enum MenuMode 
    {
        Recreate,
        Add
    }
}