using System;
using Project.EntenEller.Base.Scripts.UI.Menu;
using UnityEngine;
using UnityEngine.UI;

namespace Project.EntenEller.Base.Scripts.Advanced.Layouts
{
    [ExecuteInEditMode]
    public class EELayoutSize : EEBehaviourEEMenu
    {
        [SerializeField] private float normalSize, percentSize;
        private RectTransform parentRectTransform, selfRectTransform;
        private LayoutGroup parentLayoutGroup;
        private bool isHorizontal;

        protected override void EEAwake()
        {
            base.EEAwake();
            Init();
        }

        private void Init()
        {
            parentRectTransform = transform.parent.GetComponent<RectTransform>();
            selfRectTransform = GetComponent<RectTransform>();
            parentLayoutGroup = parentRectTransform.GetComponent<LayoutGroup>();
            
            var type = parentLayoutGroup.GetType();
            if (type == typeof(HorizontalLayoutGroup)) isHorizontal = true;
            if (type == typeof(GridLayoutGroup)) throw new Exception("Grid Layout group is not supported!");
        }

        protected override void MenuLoop()
        {
            base.MenuLoop();
            SetSize();
        }

        private void SetSize()
        {
            var parentSize = parentRectTransform.sizeDelta;
            var selfSize = selfRectTransform.sizeDelta;
            
            if (isHorizontal)
            {
                selfSize.x = parentSize.x * percentSize + normalSize;
            }
            else
            {
                selfSize.y = parentSize.y * percentSize + normalSize;
            }
            
            selfRectTransform.sizeDelta = selfSize;
        }
        
#if UNITY_EDITOR
        
        private void Update()
        {
            Init();
            SetSize();
        }

#endif
    }
}
