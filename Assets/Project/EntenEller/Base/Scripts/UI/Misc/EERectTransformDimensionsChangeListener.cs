using System;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using UnityEngine.EventSystems;

namespace Project.EntenEller.Base.Scripts.UI.Misc
{
    public class EERectTransformDimensionsChangeListener : UIBehaviour
    {
        public event Action ChangedEvent;
        
        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();
            ChangedEvent.Call();
        }
    }
}
