using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Cameras
{
    public class EECameraFitter : EEBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private float scale;
        [SerializeField] private float orthographicSize;
        public EENotifier RescaleNotifier;

        public void Rescale()
        {
            var height = 2f * orthographicSize;
            var width = height * cam.aspect;
            var size = new Vector2(scale, scale);
            size.x *= width;
            size.y *= height;
            GetSelf<RectTransform>().sizeDelta = size;
            RescaleNotifier.Notify();
        }
    }
}
