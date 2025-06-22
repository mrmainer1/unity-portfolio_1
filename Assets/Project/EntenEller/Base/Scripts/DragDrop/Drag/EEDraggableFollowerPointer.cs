using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.Inputs.Pointers;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.DragDrop.Drag
{
    public class EEDraggableFollowerPointer : EEDraggableFollower
    {
        protected override Vector3 GetPosition()
        {
            return EESingleton.Get<EEPointerManager>().PointersData.First().Position;
        }
    }
}
