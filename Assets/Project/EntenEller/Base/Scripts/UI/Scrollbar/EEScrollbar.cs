using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.UI.Scrollbar
{
    [ExecuteAfter(typeof(UnityEngine.UI.Scrollbar))]
    public class EEScrollbar : EEBehaviour
    {
        [MinMaxSlider(0, 1, true)]
        [SerializeField] private Vector2 sizeLimits = new Vector2(0, 1);
        
        protected void LateUpdate()
        {
            var size = GetSelf<UnityEngine.UI.Scrollbar>().size;
            if (size < sizeLimits.x) size = sizeLimits.x;
            else if (size > sizeLimits.y) size = sizeLimits.y;
            GetSelf<UnityEngine.UI.Scrollbar>().size = size;
        }
    }
}
