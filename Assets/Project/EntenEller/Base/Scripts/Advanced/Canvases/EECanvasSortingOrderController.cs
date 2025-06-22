using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Canvases
{
    public class EECanvasSortingOrderController : EEBehaviour
    {
        public void On()
        {
            GetSelf<Canvas>().sortingOrder = EESingleton.Get<EECanvasSortingOrderManager>().GetNext();
        }
    }
}
