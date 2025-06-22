using Project.EntenEller.Base.Scripts.Advanced.Cameras;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Canvases
{
    public class EEMainCameraCanvasSetter : EEBehaviour
    {
        [SerializeField] private float planeDistance = 100f;
        private Canvas canvas;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            canvas = GetComponent<Canvas>();
        }

        protected override void EEEnable()
        {
            base.EEEnable();
            canvas.worldCamera = EECameraUtils.MainCamera;
            canvas.planeDistance = planeDistance;
        }
    }
}
