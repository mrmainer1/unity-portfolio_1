using Project.EntenEller.Base.Scripts.Advanced.Transforms.Simple;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Transforms.RectTransforms
{
    public class EERectTransform : EETransform
    {
        private RectTransform rectTransform;

        protected override void EEAwake()
        {
            base.EEAwake();
            rectTransform = GetComponent<RectTransform>();
        }

        public void SetAnchoredPosition(Vector2 newPosition) => rectTransform.anchoredPosition = newPosition;
        public void SetAnchoredPosition(float x, float y) => rectTransform.anchoredPosition = new Vector2(x, y);
        public void SetAnchoredPositionX(float x) => rectTransform.anchoredPosition = new Vector2(x, rectTransform.anchoredPosition.y);
        public void SetAnchoredPositionY(float y) => rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, y);
        public void SetSizeDelta(Vector2 newSize) => rectTransform.sizeDelta = newSize;
        public void SetSizeDelta(float width, float height) => rectTransform.sizeDelta = new Vector2(width, height);
        public void SetWidth(float width) => rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
        public void SetHeight(float height) => rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);

        public void SetPivot(Vector2 newPivot) => rectTransform.pivot = newPivot;
        public void SetPivot(float x, float y) => rectTransform.pivot = new Vector2(x, y);
        public void SetPivotX(float x) => rectTransform.pivot = new Vector2(x, rectTransform.pivot.y);
        public void SetPivotY(float y) => rectTransform.pivot = new Vector2(rectTransform.pivot.x, y);

        public void SetAnchorMin(Vector2 newAnchorMin) => rectTransform.anchorMin = newAnchorMin;
        public void SetAnchorMinX(float x) => rectTransform.anchorMin = new Vector2(x, rectTransform.anchorMin.y);
        public void SetAnchorMinY(float y) => rectTransform.anchorMin = new Vector2(rectTransform.anchorMin.x, y);

        public void SetAnchorMax(Vector2 newAnchorMax) => rectTransform.anchorMax = newAnchorMax;
        public void SetAnchorMaxX(float x) => rectTransform.anchorMax = new Vector2(x, rectTransform.anchorMax.y);
        public void SetAnchorMaxY(float y) => rectTransform.anchorMax = new Vector2(rectTransform.anchorMax.x, y);
    }
}
