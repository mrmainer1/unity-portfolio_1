using Project.EntenEller.Base.Scripts.Advanced.Transforms.RectTransforms;
using Project.EntenEller.Base.Scripts.UI.Menu;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.UI.SizeController
{
    public class EERectTransformSizeDuplicator : EEBehaviourEEMenu
    {
        private RectTransform rectTransform;
        [SerializeField] private EERectTransformAxis axis;
        [SerializeField] private RectTransform target;
        [SerializeField] private float dx, dy;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            rectTransform = GetComponent<RectTransform>();
        }
        
        protected override void MenuLoop()
        {
            base.MenuLoop();
            if (target == null) return;
            var sizeOrigin = rectTransform.sizeDelta;
            var sizeTarget = target.sizeDelta;
            if (axis is EERectTransformAxis.Both or EERectTransformAxis.X) sizeOrigin.x = sizeTarget.x + dx * 2;
            if (axis is EERectTransformAxis.Both or EERectTransformAxis.Y) sizeOrigin.y = sizeTarget.y + dy * 2;
            rectTransform.sizeDelta = sizeOrigin;
        }
    }
}
