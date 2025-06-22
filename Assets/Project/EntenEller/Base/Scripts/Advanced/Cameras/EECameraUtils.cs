using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Cameras
{
    public static class EECameraUtils
    {
        private static Camera _MainCamera;
        public static Camera MainCamera
        {
            get
            {
                if (_MainCamera != null) return _MainCamera;
                _MainCamera = Camera.main;
                return _MainCamera;
            }
        }
    }
}
