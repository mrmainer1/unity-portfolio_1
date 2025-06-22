using System.Collections.Generic;
using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Advanced.Transforms;
using Project.EntenEller.Base.Scripts.Advanced.Transforms.Simple;
using Project.EntenEller.Base.Scripts.Advanced.Triggers;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.DragDrop.Drag;

namespace Project.EntenEller.Base.Scripts.DragDrop.Drop
{
    public class EEDropZone : EEBehaviour
    {
        public EENotifier AddDragNotifier, RemoveDragNotifier;
        public bool IsHavingAnyDrags;
        private List<EETrigger> lastCollisions = new List<EETrigger>();
        
        protected override void EEEnable()
        {
            base.EEEnable();
            EEDraggable.StopDragAnyEvent += Stop;
        }
        
        protected override void EEDisable()
        {
            base.EEDisable();
            EEDraggable.StopDragAnyEvent -= Stop;
        }

        private void Stop()
        {
            var collisions = GetNeighbor<EETrigger>().RightCollisions;
            var children = transform.GetFirstRowOfChildren();
            if (collisions.Count > children.Count)
            {
                for (var i = 0; i < collisions.Count; i++)
                {
                    var trigger = collisions[i];
                    if (i == 0)
                    {
                        trigger.GetNeighbor<EEDraggableRetreater>().Back();
                        continue;
                    }
                    if (i >= children.Count) return;
                    var child = children[i - 1];
                    trigger.GetParent<EETransformApproachPRS>().SetTarget(child);
                    Add();
                }
                return;
            }
            
            var isSame = lastCollisions.IsFullyEqual(collisions);
            lastCollisions = collisions.ToList();
            
            if (collisions.Count == 0 && IsHavingAnyDrags)
            {
                Remove();
                return;
            }
            
            for (var i = 0; i < collisions.Count; i++)
            {
                var trigger = collisions[i];
                var child = children[i];
                trigger.GetParent<EETransformApproachPRS>().SetTarget(child);
                if (!isSame) Add();
            }
        }

        public void Add()
        {
            IsHavingAnyDrags = true;
            AddDragNotifier.Notify();
        }

        public void Remove()
        {
            IsHavingAnyDrags = false;
            RemoveDragNotifier.Notify();
        }
    }
}
