using System;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.UI.VideoPlayer
{
    public class EEVideoPlayer : EEBehaviour
    {
        private enum AspectRatio
        {
            Horizontal,
            Vertical
        }

        [SerializeField] private AspectRatio aspectRatio;
        [SerializeField] [ReadOnly] private Vector2 originVideoSize;
        
        public void SetAspectRatio()
        {
            var size = GetSelf<RectTransform>().rect.size;
            var aspect = originVideoSize.x / originVideoSize.y;
            switch (aspectRatio)
            {
                case AspectRatio.Horizontal:
                    size.x = size.y * aspect;
                    break;
                case AspectRatio.Vertical:
                    size.y = size.x / aspect;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            GetChild<RectTransform>().sizeDelta = size;
        }

        protected override void EEEnable()
        {
            base.EEEnable();
            GetSelf<UnityEngine.Video.VideoPlayer>().prepareCompleted += Ready;
        }
        
        protected override void EEDisable()
        {
            base.EEDisable();
            GetSelf<UnityEngine.Video.VideoPlayer>().prepareCompleted -= Ready;
        }

        private void Ready(UnityEngine.Video.VideoPlayer source)
        {
            originVideoSize = new Vector2(source.width, source.height);
            SetAspectRatio();
        }
    }
}
