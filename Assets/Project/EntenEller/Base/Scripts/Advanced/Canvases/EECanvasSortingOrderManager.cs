using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Canvases
{
    public class EECanvasSortingOrderManager : EEBehaviour
    {
        [SerializeField] private int zOrder = 100;
        private const int max = 32767;
        
        public int GetNext()
        {
            zOrder++;
            return zOrder;
        }
    }
}
