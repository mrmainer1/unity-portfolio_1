using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.GameObjects;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Advanced.Tags;
using Project.EntenEller.Base.Scripts.UI.Menu;
using Sirenix.Utilities;
using UnityEngine;

namespace Project.Scripts.UI
{
   public class HideGameUIManager : EEBehaviourUpdate
   {
      [SerializeField] private List<EEMenu> menus;
      public EENotifier HideNotifier, ShowNotifier;
      private bool isHide;

      private void Update()
      {
         if(!isHide) return;
         if(!Input.GetKeyDown(KeyCode.Escape)) return;
         Show();
      }

      public void Hide()
      {
         menus.ForEach(n => n.SetState(false));
         var inputField = EETagUtils.TryFindEETagsInScenes("input-field-car");
         inputField?.ForEach(n => n.GetEEGameObject().Off());
         isHide = true;
         HideNotifier.Notify();
      }

      public void Show()
      {
         menus.ForEach(n => n.SetState(true));

         var inputField = EETagUtils.TryFindEETagsInScenes("input-field-car");
         inputField?.ForEach(n => n.gameObject.SetActive(true));
         isHide = false;
         ShowNotifier.Notify();
      }
   }
}
