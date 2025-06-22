using System.Collections.Generic;
using System.Linq;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Timers;
using Project.EntenEller.Base.Scripts.UI.Menu;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.EntenEller.Base.Scripts.Advanced.Canvases
{
    public class EECanvas : EEBehaviour
    {
        private static readonly List<EECanvas> canvases = new List<EECanvas>();

        protected override void EEEnable()
        {
            canvases.Add(this);
            GetSelf<EEMenuView>().StartShowNotifier.Event += On;
            GetSelf<EEMenuView>().FinishHideNotifier.Event += Off;
            EETime.StartTimer(new EETime.EETimerData
            {
                Action = () =>
                {
                    if (GetSelf<EEMenu>().IsActive) On();
                    else Off();
                },
                FinalTime = 0.1f,
                ComponentID = ComponentID
            });
        }

        protected override void EEDisable()
        {
            canvases.Remove(this);
            GetSelf<EEMenuView>().StartShowNotifier.Event -= On;
            GetSelf<EEMenuView>().FinishHideNotifier.Event -= Off;
            EETime.StopAllTimersForComponentID(ComponentID);
        }

        private void On()
        {
            SetState(true);
        }

        private void Off()
        {
            SetState(false);
        }

        private void SetState(bool isOn)
        {
            GetSelf<Canvas>().enabled = isOn;
            if (GetSelf<CanvasScaler>()) GetSelf<CanvasScaler>().enabled = isOn;
            if (GetSelf<GraphicRaycaster>()) GetSelf<GraphicRaycaster>().enabled = isOn;
        }

        public void SetZOrder(int order)
        {
            GetSelf<Canvas>().sortingOrder = order;
        }
        
        public void SetZOrderHigher()
        {
            GetSelf<Canvas>().sortingOrder += 1;
        }
        
        public void SetZOrderLower()
        {
            GetSelf<Canvas>().sortingOrder -= 1;
        }
        
        public void DisableRaycast()
        {
            GetSelf<GraphicRaycaster>().enabled = false;
        }
        
        public void EnableRaycast()
        {
            GetSelf<GraphicRaycaster>().enabled = true;
        }

        public static bool CheckHitInAnyCanvas(Vector2 screenPosition)
        {
            foreach (var canvas in canvases)
            {
                if (canvas.CheckHitInCanvas(screenPosition)) return true;
            }
            return false;
        }

        public bool CheckHitInCanvas(Vector2 screenPosition)
        {
            var results = new List<RaycastResult>();
            var pointerData = new PointerEventData(EventSystem.current)
            {
                position = screenPosition
            };
            if (!enabled) return false;
            if (!GetSelf<GraphicRaycaster>()) return false; 
            GetSelf<GraphicRaycaster>().Raycast(pointerData, results);
            return results.Any();
        }
    }
}