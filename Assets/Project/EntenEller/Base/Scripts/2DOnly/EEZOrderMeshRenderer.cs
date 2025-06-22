using UnityEngine;

namespace Project.EntenEller.Base.Scripts._2DOnly
{
    public class EEZOrderMeshRenderer : EEZOrderBase
    {
        private MeshRenderer meshRenderer;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            meshRenderer = GetComponent<MeshRenderer>();
        }

        protected override void SetZ()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying && meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
#endif
            meshRenderer.sortingOrder = Z;
        }
    }
}