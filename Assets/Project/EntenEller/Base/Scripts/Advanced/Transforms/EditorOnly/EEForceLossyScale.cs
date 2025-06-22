using Project.EntenEller.Base.Scripts.Advanced.Components;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Transforms.EditorOnly
{
    [ExecuteInEditMode]
    public class EEForceLossyScale : EEBehaviour
    {
        [SerializeField] private Vector3 scale = new Vector3(1, 1, 1);
        [SerializeField] private bool isEditModeOnly;
        
        private void Update()
        {
            if (Application.isPlaying && isEditModeOnly) return;
            
            var factor = scale;
            var ts = GetSelf<Transform>();
            
            while(true)
            {
                var tParent = ts.parent;
                if (tParent.IsNull()) break;
                factor.x /= tParent.localScale.x;
                factor.y /= tParent.localScale.y;
                factor.z /= tParent.localScale.z;
                ts = tParent;
            }

            GetSelf<Transform>().localScale = factor;
        }
    }
}