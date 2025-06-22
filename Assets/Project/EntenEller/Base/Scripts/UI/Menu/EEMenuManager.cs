using System;
using System.Collections.Generic;
using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Project.EntenEller.Base.Scripts.Advanced.Tags;

namespace Project.EntenEller.Base.Scripts.UI.Menu
{
    [ExecutionOrder(3000)]
    public class EEMenuManager : EEBehaviourLate
    {
        public static event Action<string> GotNewNameEvent;
        [ReadOnly] public List<EEMenuResource> History = new List<EEMenuResource>();
        private Dictionary<EEMenu, bool> menus = new Dictionary<EEMenu, bool>();

        public void Recreate(EEMenuResource menuResource)
        {
            HideCurrent(menuResource);
            Add(menuResource);
        }
        
        public void Add(EEMenuResource menuResource)
        {
            GotNewNameEvent.Call(menuResource.Key);
            if (menuResource.IsRecordHistory)
            {
                if (History.LastOrDefault() == menuResource) return;
                History.Add(menuResource);
            }
            SetState(menuResource, true);
        }

        public void Remove(EEMenuResource menuResource)
        {
            if (menuResource.IsRecordHistory && History.Any())
            {
                if (History.Last() != menuResource) return;
                History.Remove(menuResource);
            }
            SetState(menuResource, false);
        }
        
        public void Back()
        {
            if (History.Count <= 1) return;
            History.Remove(History.LastOrDefault());
            Rebuild();
        }
        
        public void CleanUntil(EEMenuResource menuResource)
        {
            for (var i = History.Count - 1; i >= 0; i--)
            {
                var menu = History[i];
                if (menu == menuResource) break;
                Remove(menu);
            }
            Rebuild();
        }
        
        public void CleanAll()
        {
            History.Clear();
            HideCurrent();
        }

        private static void HideCurrent(EEMenuResource menuResource = null)
        {
            var allMenus = EEMenu.Menus.Where(a => !a.IgnoreMenuSystem);
            if (menuResource != null)
            {
                allMenus = allMenus.Where(a => !menuResource.MenuEETags.Contains(a.GetSelf<EETagHolder>().FirstTag)).ToArray();
            }
            allMenus.ForEach(b => b.SetState(false));
        }
        
        private void Rebuild()
        {
            HideCurrent();
            foreach (var menuData in History)
            {
                if (menuData.MenuMode == MenuMode.Add) SetState(menuData, true);
            }
            var last = History.Last(a => a.MenuMode == MenuMode.Recreate);
            GotNewNameEvent.Call(last.Key);
            SetState(last, true);
        }

        protected override void EELateUpdate()
        {
            base.EELateUpdate();
            foreach (var kv in menus)
            {
                kv.Key.SetState(kv.Value);
            }
            menus.Clear();
        }

        private void SetState(EEMenuResource menuResource, bool isOn)
        {
            for (var i = 0; i < menuResource.MenuEETags.Count; i++)
            {
                var eeTag = menuResource.MenuEETags[i];
                var menu = EETagUtils.FindEETagInScenes(eeTag).GetSelf<EEMenu>();
                if (!menus.ContainsKey(menu)) menus.Add(menu, false);
                menus[menu] = isOn;
            }
        }
    }
}
