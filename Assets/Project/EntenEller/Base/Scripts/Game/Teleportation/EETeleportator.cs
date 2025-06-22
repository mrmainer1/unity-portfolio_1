using Project.EntenEller.Base.Scripts.Advanced.GameObjects;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.GameOnly.Teleportation
{
    public class EETeleportator : EEBehaviour
    {
        public Vector3 Position;

        public static void Call(EEBehaviour target, Vector3 position)
        {
            target.GetParent<EEGameObjectRoot>().transform.position = position;
        }
        
        public void Call(EEBehaviour target)
        {
            Call(target, Position);
        }
    }
}
