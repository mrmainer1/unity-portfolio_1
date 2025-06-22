using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Advanced.Triggers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Project.EntenEller.Base.Scripts.DragDrop.Drag
{
    public class EEDraggableDropZoneFinder : EEBehaviour
    {
        public EENotifier NoDraggablesToStickNotifier;
        
        protected override void EEEnable()
        {
            base.EEEnable();
            GetSelf<EEDraggablePullListener>().DragStopNotifier.Event += StopDrag;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            GetSelf<EEDraggablePullListener>().DragStopNotifier.Event -= StopDrag;
        }
        
        private void StopDrag()
        {
            if (GetNeighbor<EETrigger>().RightCollisions.Count != 0) return;
            NoDraggablesToStickNotifier.Notify();
        }
    }
}
