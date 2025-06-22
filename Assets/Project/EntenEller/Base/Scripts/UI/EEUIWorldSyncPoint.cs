using Project.EntenEller.Base.Scripts.Advanced.Cameras;
using Project.EntenEller.Base.Scripts.Advanced.Tags;
using Project.EntenEller.Base.Scripts.UI.Menu;
using UnityEngine;
using UnityEngine.UI;

namespace Project.EntenEller.Base.Scripts.UI
{
    public class EEUIWorldSyncPoint : EEBehaviourEEMenu
    {
        public EEGameObjectFinder Target;
        private RectTransform rectTransform, rectTransformCanvas;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            GetSelf<RectTransform>().anchorMin = Vector2.zero;
            GetSelf<RectTransform>().anchorMax = Vector2.zero;
            rectTransform = GetComponent<RectTransform>();
            rectTransformCanvas = GetComponentInParent<CanvasScaler>().GetComponent<RectTransform>();
        }

        protected override void MenuLoop()
        {
            base.MenuLoop();

            var pos = EECameraUtils.MainCamera.WorldToScreenPoint(Target.GetSingle().transform.position);
            
            var qx = pos.x / Screen.width;
            var qy = pos.y / Screen.height;
            var x = rectTransformCanvas.sizeDelta.x * qx;
            var y = rectTransformCanvas.sizeDelta.y * qy;
            
            rectTransform.anchoredPosition = new Vector2(x, y);
        }
    }
}
