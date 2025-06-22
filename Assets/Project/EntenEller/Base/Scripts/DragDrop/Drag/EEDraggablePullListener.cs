using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Project.EntenEller.Base.Scripts.DragDrop.Drag
{
    public abstract class EEDraggablePullListener : EEBehaviour
    {
        public EENotifier DragStartNotifier;
        public EENotifier DragStopNotifier;

        protected void DragStart()
        {
            DragStartNotifier.Notify();
        }

        protected void DragEnd()
        {
            DragStopNotifier.Notify();
        }
    }
}
