using Project.EntenEller.Base.Scripts.Advanced.Cameras;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Raycasters
{
    public class EERaycasterScreen : EERaycaster
    {
        [SerializeField] private Camera currentCamera;

        protected override void EEAwake()
        {
            base.EEAwake();
            if (!currentCamera) currentCamera = EECameraUtils.MainCamera;
        }

        public override int CastRay()
        {
            return UnityEngine.Physics.RaycastNonAlloc(currentCamera.ScreenPointToRay(Input.mousePosition), Hits, Distance, LayerMask);
        }
    }
}