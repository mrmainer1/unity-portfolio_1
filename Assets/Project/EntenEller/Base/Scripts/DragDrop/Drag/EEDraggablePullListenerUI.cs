using Project.EntenEller.Base.Scripts.Advanced.Inputs.Pointers;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.DragDrop.Drag
{
    public class EEDraggablePullListenerUI : EEDraggablePullListener
    {
        protected override void EEEnable()
        {
            base.EEEnable();
            GetNeighbor<EEPointerUI>().DownNotifier.Event += PointerDown;
            GetNeighbor<EEPointerUI>().UpNotifier.Event += PointerUp;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            GetNeighbor<EEPointerUI>().DownNotifier.Event -= PointerDown;
            GetNeighbor<EEPointerUI>().UpNotifier.Event -= PointerUp;
        }
        
        protected void PointerUp()
        {
            if (GetSelf<EEDraggable>().IsDragging) DragEnd();
        }
        
        private void PointerDown()
        {
            if (Application.isMobilePlatform && Input.touchCount != 1) return;
            DragStart();
        }
    }
}
