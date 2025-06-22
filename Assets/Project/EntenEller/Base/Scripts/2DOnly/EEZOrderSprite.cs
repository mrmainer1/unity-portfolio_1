using UnityEngine;

namespace Project.EntenEller.Base.Scripts._2DOnly
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    public class EEZOrderSprite : EEZOrderBase
    {
        private SpriteRenderer spriteRenderer;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            spriteRenderer = GetSelf<SpriteRenderer>();
        }

        protected override void SetZ()
        {
            spriteRenderer.sortingOrder = Z;
        }
    }
}
