using Project.EntenEller.Base.Scripts.Advanced.Canvases;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    [ExecuteInEditMode]
    public class EERectTransformBetweenOther : MonoBehaviour
    {
        [SerializeField] private RectTransform bottomTarget, topTarget;
        [SerializeField] private RectTransform panel;
        public Rect screenRectBottom, screenRectTop;
        
        private void Update()
        {
            screenRectBottom = bottomTarget.GetScreenRect();
            screenRectTop = topTarget.GetScreenRect();

            var pos = panel.position;
            
            var topPosition = new Vector2(screenRectTop.x, screenRectTop.y);
            pos.y = topPosition.y;
            panel.position = pos;
            var topRectPosition = panel.anchoredPosition;
            
            var bottomPosition = new Vector2(screenRectBottom.x, screenRectBottom.y);
            pos.y = bottomPosition.y;
            panel.position = pos;
            var bottomRectPosition = panel.anchoredPosition;

            var h = topRectPosition.y - bottomRectPosition.y;

            var size = panel.sizeDelta;
            size.y = h;
            panel.sizeDelta = size;
        }
    }
}
